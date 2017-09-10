using Newtonsoft.Json;

namespace Server.WebApi.ExceptionHandler.ValidateModel
{
    public class ValidationError
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Field { get; }

        public string Message { get; }

        public ValidationError(string field, string message)
        {
            Field = string.IsNullOrEmpty(field) ? null : field;
            Message = message;
        }
    }
}
