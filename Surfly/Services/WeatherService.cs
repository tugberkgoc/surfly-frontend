using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Surfly.Models;

namespace Surfly.Services
{
    public class WeatherService
    {
        HttpClient _client;

        public WeatherService()
        {
            _client = new HttpClient();
        }

        public async Task<WeatherData> GetWeatherDataAsync(string uri)
        {
            WeatherData weatherData = null; // Convert to WeatherData
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    weatherData = JsonConvert.DeserializeObject<WeatherData>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
            }

            return weatherData;
        }

    }
}
