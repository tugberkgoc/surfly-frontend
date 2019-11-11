using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Surfly.Models;

namespace Surfly.Services
{
    public class SignUpService
    {
        HttpClient _client;

        public SignUpService()
        {
            _client = new HttpClient();
        }

        public async Task<ResponseData> SignUpUserAsync(string uri, User user)
        {
            ResponseData responseData = null;
            var json = JsonConvert.SerializeObject(user);
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
