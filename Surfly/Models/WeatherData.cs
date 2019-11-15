using Newtonsoft.Json;

namespace Surfly.Models
{
    public class WeatherData
    {
        [JsonProperty("cod")]
        public string Cod { get; set; }

        [JsonProperty("message")]
        public double Message { get; set; }

        [JsonProperty("list")]
        public WeatherList[] WeatherList { get; set; }

        [JsonProperty("cnt")]
        public int Count { get; set; }

        [JsonProperty("city")]
        public City City { get; set; }

    }

    public class WeatherList
    {
        [JsonProperty("dt")]
        public double Dt { get; set; }

        [JsonProperty("main")]
        public Main Main { get; set; }

        [JsonProperty("weather")]
        public Weather[] Weather { get; set; }

        [JsonProperty("clouds")]
        public Clouds Clouds { get; set; }

        [JsonProperty("wind")]
        public Wind Wind { get; set; }

        [JsonProperty("sys")]
        public Sys sys { get; set; }

        [JsonProperty("dt_txt")]
        public string DtTxt { get; set; }
    }

    public class Main
    {
        [JsonProperty("temp")]
        public double Temperature { get; set; }

        [JsonProperty("temp_min")]
        public double MinTemperature { get; set; }

        [JsonProperty("temp_max")]
        public double MaxTemperature { get; set; }

        [JsonProperty("pressure")]
        public double Pressure { get; set; }

        [JsonProperty("sea_level")]
        public double SeaLevel { get; set; }

        [JsonProperty("grnd_level")]
        public double GrndLevel { get; set; }

        [JsonProperty("humidity")]
        public double Humidity { get; set; }

        [JsonProperty("temp_kf")]
        public double TempKf { get; set; }
    }

    public class Weather
    {
        [JsonProperty("id")]
        public double Id { get; set; }

        [JsonProperty("main")]
        public string Main { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }
    }

    public class Clouds
    {
        [JsonProperty("all")]
        public double All { get; set; }
    }

    public class Wind
    {
        [JsonProperty("speed")]
        public double WindSpeed { get; set; }

        [JsonProperty("deg")]
        public double Deg { get; set; }
    }

    public class Sys
    {
        [JsonProperty("pod")]
        public string Pod { get; set; }
    }

    public class City
    {
        [JsonProperty("id")]
        public double id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("coord")]
        public Coord Coord { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("population")]
        public double Population { get; set; }

        [JsonProperty("timezone")]
        public double Timezone { get; set; }

        [JsonProperty("sunrise")]
        public double Sunrise { get; set; }

        [JsonProperty("sunset")]
        public double Sunset { get; set; }
    }

    public class Coord
    {
        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("lon")]
        public double Lon { get; set; }
    }
}
