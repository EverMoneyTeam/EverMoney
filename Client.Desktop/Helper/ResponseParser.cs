using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Client.Desktop.Helper
{
    public class ResponseException
    {
        public int Code { get; set; }
        public string Message { get; set; }
        [JsonIgnore]
        public HttpStatusCode StatusCode => (HttpStatusCode) Code;
    }

    public static class ResponseParser
    {
        public static async Task<ResponseException> ExceptionParse(HttpResponseMessage response)
        {
            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ResponseException>(responseBody);
        }
    }
}
