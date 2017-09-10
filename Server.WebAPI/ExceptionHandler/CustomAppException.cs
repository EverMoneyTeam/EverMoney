using System;

namespace Server.WebApi.ExceptionHandler
{
    public class CustomAppException : Exception
    {
        public CustomAppException()
        { }

        public CustomAppException(string message)
            : base(message)
        { }

        public CustomAppException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
