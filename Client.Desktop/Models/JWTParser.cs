using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;

namespace Client.Desktop.Models
{
    public static class JWTParser
    {
        public static string ReturnAccountId(ResponseJWTFormat jwt)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(jwt.AccessToken) as JwtSecurityToken;
            var sub = jsonToken.Claims.First(claim => claim.Type == "sub").Value;
            return sub;
        }

        public static string ReturnAccountId(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(accessToken) as JwtSecurityToken;
            var sub = jsonToken.Claims.First(claim => claim.Type == "sub").Value;
            return sub;
        }
    }
}
