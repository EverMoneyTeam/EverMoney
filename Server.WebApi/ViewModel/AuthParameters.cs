using System;

namespace Server.WebApi.Controllers
{
    public class AuthParameters
    {
        public string AccountId { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string GrantType { get; set; }

        public string RefreshToken { get; set; }
    }
}