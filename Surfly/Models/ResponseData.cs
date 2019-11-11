using Newtonsoft.Json;

namespace Surfly.Models
{
    public class ResponseData
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
