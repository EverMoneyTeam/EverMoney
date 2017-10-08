using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Client.Desktop.Utils
{
    public enum ApiRequestEnum
    {
        Login,
        Register,
        RefreshToken, 
        Logout,
        Health
    }

    public static class ApiAuthService
    {
        public static async Task<HttpResponseMessage> PostAsync(ApiRequestEnum apiRequestEnum, object body, params Tuple<string, string>[] parameters)
        {
            return await GetClient().PostAsJsonAsync(GetRequestUri(apiRequestEnum, parameters), body);
        }

        public static async Task<HttpResponseMessage> GetAsync(ApiRequestEnum apiRequestEnum, params Tuple<string, string>[] parameters)
        {
            return await GetClient().GetAsync(GetRequestUri(apiRequestEnum, parameters));
        }

        private static HttpClient GetClient()
        {
            var httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:5001/api/") };
           httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Properties.Login.Default.JwtToken);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return httpClient;
        }

        private static string GetRequestUri(ApiRequestEnum apiRequestEnum, Tuple<string, string>[] parameters)
        {
            string uri;

            // Set base URI
            switch (apiRequestEnum)
            {
                case ApiRequestEnum.Login:
                    uri = "token/login";
                    break;
                case ApiRequestEnum.Register:
                    uri = "token/registration";
                    break;
                case ApiRequestEnum.RefreshToken:
                    uri = "token/refreshToken";
                    break;
                case ApiRequestEnum.Logout:
                    uri = "token/logout";
                    break;
                case ApiRequestEnum.Health:
                    uri = "health/authorized";
                    break;
                default:
                    return string.Empty;
            }

            // Add parameters to URI
            if (parameters != null && parameters.Any())
            {
                uri += "?";
                for (int i = 0; i < parameters.Length; i++)
                {
                    uri += parameters[i].Item1 + "=" + parameters[i].Item2;
                    if (i != parameters.Length - 1)
                    {
                        uri += "&";
                    }
                }
            }

            return uri;
        }
    }
}
