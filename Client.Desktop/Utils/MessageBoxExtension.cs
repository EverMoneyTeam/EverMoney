using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Client.Desktop.Helper;
using MaterialDesignThemes.Wpf;

namespace Client.Desktop.Utils
{
    public static class MessageBoxExtension
    {
        public static void ShowError(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.Forbidden || response.StatusCode == HttpStatusCode.NotAcceptable || response.StatusCode == HttpStatusCode.Unauthorized)
            {
                MessageBox.Show("Unauthorized. Please, login", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var responseBody = ResponseParser.ExceptionParse(response).Result;
            MessageBox.Show(responseBody.StatusCode + "\n" + responseBody.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void ShowError(Exception exception)
        {
            string message = exception.Message;
            if (exception.InnerException != null)
                message += "\n" + exception.InnerException;

            MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void ShowError(string errorMessage)
        {
            MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void ShowInfo(string infoMessage)
        {
            MessageBox.Show(infoMessage, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
