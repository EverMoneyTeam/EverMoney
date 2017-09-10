using Client.Desktop.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Client.Desktop.ApiOperations
{
    class Authorization
    {
        private string baseUrl;

        public Authorization()
        {
            this.baseUrl = "http://localhost:5000/api";
        }

        public ResponseJWTFormat AuthenticateUser(string accountId, string login, string password, string grantType, string refreshToken)
        {
            string endpoint = this.baseUrl + "/token/auth";
            string method = "POST";
            string json = JsonConvert.SerializeObject(new
            {
                accountId,
                login,
                password,
                grantType,
                refreshToken
            });

            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";
            try
            {
                string response = wc.UploadString(endpoint, method, json);
                return JsonConvert.DeserializeObject<ResponseJWTFormat>(response);
            }
            catch (Exception)
            {
                return null;
            }


        }

        //    public User GetUserDetails(User user)
        //    {
        //        string endpoint = this.baseUrl + "/users/" + user.Id;
        //        string access_token = user.access_token;

        //        WebClient wc = new WebClient();
        //        wc.Headers["Content-Type"] = "application/json";
        //        wc.Headers["Authorization"] = access_token;
        //        try
        //        {
        //            string response = wc.DownloadString(endpoint);
        //            user = JsonConvert.DeserializeObject<User>(response);
        //            user.access_token = access_token;
        //            return user;
        //        }
        //        catch (Exception)
        //        {
        //            return null;
        //        }
        //    }

        public ResponseJWTFormat RegisterAccount(string login, string password)
        {
            string endpoint = this.baseUrl + "/token/registration";
            string method = "POST";
            string json = JsonConvert.SerializeObject(new
            {
                login = login,
                password = password
            });

            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";
            try
            {
                string response = wc.UploadString(endpoint, method, json);
                return JsonConvert.DeserializeObject<ResponseJWTFormat>(response);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
