using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Server.WebApi.ConfigOption;
using Server.DataAccess.Model;
using Server.DataAccess.Repository;

namespace Server.WebApi.Controllers
{
    [Route("api/token")]
    public class TokenController : Controller
    {
        //some config in the appsettings.json  
        private Audience _settings;
        //repository to handler the sqlite database  
        private ITokenRepository _tokenRepository;

        private IAccountRepository _accountRepository;

        public TokenController(IOptions<Audience> settings, ITokenRepository tokenRepository, IAccountRepository accountRepository)
        {
            this._settings = settings.Value;
            this._tokenRepository = tokenRepository;
            this._accountRepository = accountRepository;
        }

        [HttpPost("auth")]
        public IActionResult Auth([FromBody]Parameters parameters)
        {
            if (parameters == null)
            {
                return Json(new ResponseData(ResponseDataOption.NullOfParameters));
            }

            if (parameters.GrantType == "password")
            {
                return Json(DoPassword(parameters));
            }
            else if (parameters.GrantType == "refresh_token")
            {
                return Json(DoRefreshToken(parameters));
            }
            else
            {
                return Json(new ResponseData(ResponseDataOption.BadRequest));
            }
        }

        //scenario 1 get the access-token by username and password  
        private ResponseData DoPassword(Parameters parameters)
        {
            //validate the client_id/client_secret/username/passwo  
            var accountId = _accountRepository.GetAccountId(parameters.Login, parameters.Password);

            if (accountId == null)
            {
                return new ResponseData(ResponseDataOption.InvalidUserInfomation);
            }

            var refresh_token = Guid.NewGuid().ToString().Replace("-", "");

            var token = new Token
            {
                AccountId = accountId,
                RefreshToken = refresh_token,
                IsStop = 0
            };

            //store the refresh_token   
            if (_tokenRepository.AddToken(token))
            {
                return new ResponseData(ResponseDataOption.Ok, GetJwt(accountId.ToString(), refresh_token));
            }
            else
            {
                return new ResponseData(ResponseDataOption.CanNotAddTokenToDatabase);
            }
        }

        //scenario 2 get the access_token by refresh_token  
        private ResponseData DoRefreshToken(Parameters parameters)
        {
            var token = _tokenRepository.GetToken(parameters.RefreshToken, parameters.AccountId);

            if (token == null)
            {
                return new ResponseData(ResponseDataOption.CanNotRefreshToken);
            }

            if (token.IsStop == 1)
            {
                return new ResponseData(ResponseDataOption.RefreshTokenHasExpired);
            }
            
            var refresh_token = Guid.NewGuid().ToString().Replace("-", "");

            token.IsStop = 1;
            //expire the old refresh_token and add a new refresh_token  
            var updateFlag = _tokenRepository.ExpireToken(token);

            var addFlag = _tokenRepository.AddToken(new Token
            {
                AccountId = new Guid(parameters.AccountId),
                RefreshToken = refresh_token,
                IsStop = 0
            });

            if (updateFlag && addFlag)
            {
                return new ResponseData(ResponseDataOption.Ok, GetJwt(parameters.AccountId, refresh_token));
            }
            else
            {
                return new ResponseData(ResponseDataOption.CanNotExpireTokenOrANewToken);
            }
        }

        //get the jwt token   
        private string GetJwt(string client_id, string refresh_token)
        {
            var now = DateTime.UtcNow;

            var claims = new Claim[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, client_id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(), ClaimValueTypes.Integer64)
            };

            var symmetricKeyAsBase64 = _settings.Secret;
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);

            var jwt = new JwtSecurityToken(
                issuer: _settings.Iss,
                audience: _settings.Aud,
                claims: claims,
                notBefore: now,
                expires: now.Add(TimeSpan.FromDays(1)),
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                expires_in = (int)TimeSpan.FromMinutes(2).TotalSeconds,
                refresh_token = refresh_token,
            };

            return JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented });
        }
    }
}
