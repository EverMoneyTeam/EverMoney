namespace Server.WebApi.AppSetting
{
    public class JwtConfigs
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
