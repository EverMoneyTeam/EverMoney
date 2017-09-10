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

        public static bool CheckSignature(ResponseJWTFormat response)
        {
            string jwt = response.AccessToken;
            string[] parts = jwt.Split(".".ToCharArray());
            var header = parts[0];
            var payload = parts[1];
            var signature = parts[2];//Base64UrlEncoded signature from the token

            byte[] bytesToSign = getBytes(string.Join(".", header, payload));

            byte[] secret = getBytes("12345678901234567890");

            var alg = new HMACSHA256(secret);
            var hash = alg.ComputeHash(bytesToSign);

            var computedSignature = Base64UrlEncode(hash);

            return signature == computedSignature;
        }

        private static byte[] getBytes(string value)
        {
            return Encoding.UTF8.GetBytes(value);
        }

        // from JWT spec
        private static string Base64UrlEncode(byte[] input)
        {
            var output = Convert.ToBase64String(input);
            output = output.Split('=')[0]; // Remove any trailing '='s
            output = output.Replace('+', '-'); // 62nd char of encoding
            output = output.Replace('/', '_'); // 63rd char of encoding
            return output;
        }
    }
}
