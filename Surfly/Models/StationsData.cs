using Newtonsoft.Json;

namespace Surfly.Models
{
    public class StationsData
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("features")] // It represents a station
        public StationData[] Stations { get; set; }
    }
}
