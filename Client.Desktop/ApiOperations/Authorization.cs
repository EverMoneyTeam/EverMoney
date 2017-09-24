using Client.Desktop.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Client.Desktop.ApiOperations
{
    static class Authorization
    {
        static HttpClient client = new HttpClient();

        static Authorization()
        {
            client.BaseAddress = new Uri("https://localhost/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static async Task<bool> CreateAccountAsync(string login, string password)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("token/registration", new { login = login, password = password });
            return response.IsSuccessStatusCode;
        }


        public static async Task<ResponseJWTFormat> GetTokenAsync(string login, string password)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("token/login", new { login = login, password = password });

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<ResponseJWTFormat>();
            }
            return null;
        }

        //public ResponseJWTFormat AuthenticateUser(string login, string password)
        //{
        //    string endpoint = baseUrl + "/token/login";
        //    string method = "POST";
        //    string json = JsonConvert.SerializeObject(new
        //    {
        //        login = login,
        //        password = password
        //    });



        //    WebClient wc = new WebClient();
        //    wc.Headers["Content-Type"] = "application/json";
        //    try
        //    {
        //        string response = wc.UploadString(endpoint, method, json);
        //        return JsonConvert.DeserializeObject<ResponseJWTFormat>(response);
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}

        //public ResponseJWTFormat RegisterAccount(string login, string password)
        //{
        //    string endpoint = baseUrl + "/token/registration";
        //    string method = "POST";
        //    string json = JsonConvert.SerializeObject(new
        //    {
        //        login = login,
        //        password = password
        //    });

        //    WebClient wc = new WebClient();
        //    wc.Headers["Content-Type"] = "application/json";
        //    try
        //    {
        //        string response = wc.UploadString(endpoint, method, json);
        //        return response;
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}
    }
}
