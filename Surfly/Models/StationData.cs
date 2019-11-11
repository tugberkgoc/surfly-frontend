using Newtonsoft.Json;

namespace Surfly.Models
{
    public class StationData
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("geometry")]
        public Geometry Geometry { get; set; }

        [JsonProperty("properties")]
        public Properties Properties { get; set; }

        public override string ToString()
        {
            return $"{Properties.Name} - {Properties.Country}";
        }
    }

    public class Geometry
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("coordinates")]
        public double[] Coordinates { get; set; }
    }

    public class Properties
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Country")]
        public string Country { get; set; }

        [JsonProperty("ContinuousHeightsAvailable")]
        public bool ContinuousHeightsAvailable { get; set; }

        [JsonProperty("Footnote")]
        public string Footnote { get; set; } // null not sure about type
    }

}
