using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Server.WebApi.Helper
{
    public static class AuthTokenHelper
    {
        public static string GetAccountId(this HttpContext context)
        {
            return new JwtSecurityToken(context.Request.Headers["Authorization"].ToString().Split(' ')[1]).Subject;
        }
    }
}
