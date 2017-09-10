using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Rest;
using SimpleExceptionHandling;

namespace Server.WebApi.ExceptionHandler
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<CustomExceptionFilter> _logger;

        public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            var statusCode = 0;
            string message = null;

            var result =
                Handling.Prepare()
                    .On<ValidationException>(ex =>
                    {
                        _logger.LogWarning(ex, "ValidationException has been thrown");

                        statusCode = 422;
                        message = "The provided entity has invalid data";
                    })
                    .On<NotImplementedException>(ex =>
                    {
                        _logger.LogCritical(ex, "NotImplementedException has been thrown");

                        statusCode = (int)HttpStatusCode.NotImplemented;
                        message = "The action is not implemented";
                    })
                    .On<TimeoutException>(ex =>
                    {
                        _logger.LogError(ex, "TimeoutException has been thrown");

                        statusCode = (int)HttpStatusCode.GatewayTimeout;
                        message = "Connection failed to some internal service";
                    })
                    .On<CustomAppException>(ex =>
                    {
                        _logger.LogError(ex, "CustomAppException has been thrown");

                        statusCode = (int)HttpStatusCode.InternalServerError;
                        message = ex.Message;
                    })
                    .Catch(context.Exception, throwIfNotHandled: false);

            if (!result.Handled)
            {
                _logger.LogError(context.Exception, "Exception unknown has been thrown");

                statusCode = (int)HttpStatusCode.InternalServerError;
                message = "Connection failed to some internal service";
            }

            context.HttpContext.Response.StatusCode = statusCode;
            context.Result = new JsonResult(new
            {
                Code = statusCode,
                Message = message
            });
        }
    }
}
