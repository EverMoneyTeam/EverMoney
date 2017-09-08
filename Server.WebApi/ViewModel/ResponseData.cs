namespace Server.WebApi.Controllers
{
    public enum ResponseDataOption
    {
        Ok,
        BadRequest,
        NullOfParameters,
        InvalidUserInfomation,
        CanNotAddTokenToDatabase,
        RefreshTokenHasExpired,
        CanNotExpireTokenOrANewToken,
        CanNotRefreshToken
    }

    public class ResponseData
    {
        public ResponseData(ResponseDataOption responseDataOption)
        {
            SetCodeAndMessage(responseDataOption);
        }

        public ResponseData(ResponseDataOption responseDataOption, object data)
        {
            SetCodeAndMessage(responseDataOption);
            Data = data;
        }

        public string Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        private void SetCodeAndMessage(ResponseDataOption responseDataOption)
        {
            switch (responseDataOption)
            {
                case ResponseDataOption.BadRequest:
                    Code = "904";
                    Message = "bad request";
                    break;
                case ResponseDataOption.NullOfParameters:
                    Code = "901";
                    Message = "null of parameters";
                    break;
                case ResponseDataOption.InvalidUserInfomation:
                    Code = "902";
                    Message = "invalid user infomation";
                    break;
                case ResponseDataOption.Ok:
                    Code = "999";
                    Message = "Ok";
                    break;
                case ResponseDataOption.CanNotAddTokenToDatabase:
                    Code = "909";
                    Message = "can not add token to database";
                    break;
                case ResponseDataOption.RefreshTokenHasExpired:
                    Code = "906";
                    Message = "refresh token has expired";
                    break;
                case ResponseDataOption.CanNotExpireTokenOrANewToken:
                    Code = "910";
                    Message = "can not expire token or a new token";
                    break;
                case ResponseDataOption.CanNotRefreshToken:
                    Code = "95";
                    Message = "can not refresh token";
                    break;
                default:
                    break;
            }
        }
    }
}