using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Client.Desktop.Helper;
using MaterialDesignThemes.Wpf;

namespace Client.Desktop.Utils
{
    public static class DialogHostExtension
    {
        public static async Task<object> ShowError(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.Forbidden || response.StatusCode == HttpStatusCode.NotAcceptable || response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return await DialogHost.Show("Unauthorized. Please, login");
            }

            var responseBody = await ResponseParser.ExceptionParse(response);
            return await DialogHost.Show(responseBody.StatusCode + Environment.NewLine + responseBody.Message);
        }
        
        public static async Task<object> ShowError(Exception exception)
        {
            string message = exception.Message;
            if (exception.InnerException != null)
                message += Environment.NewLine + exception.InnerException;

            return await DialogHost.Show(message);
        }

        public static async Task<object> ShowInfo(string infoMessage)
        {
            return await DialogHost.Show(infoMessage);
        }
    }
}
