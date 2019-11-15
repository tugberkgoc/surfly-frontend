using System;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace Surfly.Models
{
    public class TidalAndWeather
    {
        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("City")]
        public string City { get; set; }

        [JsonProperty("Icon")]
        public string Icon { get; set; }

        [JsonProperty("Date")]
        public string Date { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("Temperature")]
        public double Temperature { get; set; }

        [JsonProperty("MinTemperature")]
        public double MinTemperature { get; set; }

        [JsonProperty("MaxTemperature")]
        public double MaxTemperature { get; set; }

        [JsonProperty("WindSpeed")]
        public double WindSpeed { get; set; }

        [JsonProperty("Humidity")]
        public double Humidity { get; set; }

        [JsonProperty("FirstWater")]
        public string FirstWater { get; set; }

        [JsonProperty("SecondWater")]
        public string SecondWater { get; set; }

        [JsonProperty("ThirdWater")]
        public string ThirdWater { get; set; }

        [JsonProperty("FourthWater")]
        public string FourthWater { get; set; }

        [JsonProperty("FirstWaterHeight")]
        public string FirstWaterHeight { get; set; }

        [JsonProperty("SecondWaterHeight")]
        public string SecondWaterHeight { get; set; }

        [JsonProperty("ThirdWaterHeight")]
        public string ThirdWaterHeight { get; set; }

        [JsonProperty("FourthWaterHeight")]
        public string FourthWaterHeight { get; set; }

        public TidalAndWeather()
        {
        }
    }
}
