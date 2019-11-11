using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Surfly.Helpers;
using Surfly.Models;

namespace Surfly.Services
{
    public class TidalService
    {

        HttpClient _client;

        public TidalService()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Constants.UKTidalAPIKey);
        }

        public async Task<StationData> GetStationDataAsync(string uri)
        {
            StationData stationData = null;
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    stationData = JsonConvert.DeserializeObject<StationData>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
            }

            return stationData;
        }

        public async Task<StationsData> GetStationsDataAsync(string uri)
        {
            StationsData stationsData = null;
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    stationsData = JsonConvert.DeserializeObject<StationsData>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
            }

            return stationsData;
        }

        //public async Task<TidalEventsData[]> GetTidalEventsDataAsync(string uri)
        //{
        //    TidalEventsData[] tidalEventsData = null;
        //    try
        //    {
        //        //_client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Subscription key", "Primary-654b");
        //        HttpResponseMessage response = await _client.GetAsync(uri);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            string content = await response.Content.ReadAsStringAsync();
        //            Debug.WriteLine("Tidal Events are: \t");
        //            Debug.WriteLine(content);
        //            tidalEventsData = JsonConvert.DeserializeObject<TidalEventsData[]>(content);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine("\tERROR {0}", ex.Message);
        //    }

        //    return tidalEventsData;
        //}
    }
}
