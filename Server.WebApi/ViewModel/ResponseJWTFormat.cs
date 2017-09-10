using System;

namespace Server.WebApi.ViewModel
{
    public class ResponseJwtFormat
    {
        public string AccessToken { get; set; }
        public DateTime ExpiresIn { get; set; }
        public string RefreshToken { get; set; } 
    }
}
