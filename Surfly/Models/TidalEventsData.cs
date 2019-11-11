using Newtonsoft.Json;

namespace Surfly.Models
{
    public class TidalEventsData
    {
        [JsonProperty("EventType")]
        public string EventType { get; set; }

        [JsonProperty("DateTime")]
        public string DateTime { get; set; }

        [JsonProperty("IsApproximateTime")]
        public bool IsApproximateTime { get; set; }

        [JsonProperty("Height")]
        public double Height { get; set; }

        [JsonProperty("IsApproximateHeight")]
        public bool IsApproximateHeight { get; set; }

        [JsonProperty("Filtered")]
        public bool Filtered { get; set; }
    }
}
