using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Surfly.Models;

namespace Surfly.Services
{
    public class TidalAndWeatherService
    {
        HttpClient _client;

        public TidalAndWeatherService()
        {
            _client = new HttpClient();
        }

        public async Task<List<TidalAndWeather>> GetSavedTidalAndWeather(string uri)
        {
            List<TidalAndWeather> tidalAndWeather = null;
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    tidalAndWeather = JsonConvert.DeserializeObject<List<TidalAndWeather>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
            }

            return tidalAndWeather;
        }

        public async Task<ResponseData> PostSaveTidalAndWeather(string uri, List<TidalAndWeather> tidalAndWeather)
        {
            ResponseData responseData = null;
            var json = JsonConvert.SerializeObject(tidalAndWeather);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await _client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    responseData = JsonConvert.DeserializeObject<ResponseData>(result);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
            }

            return responseData;
        }

        public async Task<ResponseData> DeleteTidalAndWeatherData(string uri, TidalAndWeather postContent)
        {
            ResponseData responseData = null;
            var json = JsonConvert.SerializeObject(postContent);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await _client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    responseData = JsonConvert.DeserializeObject<ResponseData>(result);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
            }

            return responseData;
        }
    }
}
