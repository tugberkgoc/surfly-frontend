using System;
using System.Collections.Generic;
using Surfly.Services;
using Surfly.Helpers;
using Xamarin.Forms;
using Surfly.Models;
using Xamarin.Forms.Internals;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Linq;
using Xamarin.Essentials;

namespace Surfly.Views
{
    public partial class SearchPage : ContentPage
    {
        TidalService _tidalService;
        WeatherService _weatherService;

        string howManyDays;

        public SearchPage()
        {
            InitializeComponent();
            _tidalService = new TidalService();
            _weatherService = new WeatherService();
            howManyDays = "0";
        }

        async void OnTextChangedAsync(object sender, EventArgs e)
        {
            SearchBar searchBar = (SearchBar)sender;
            StationsData stationsData = await _tidalService.GetStationsDataAsync(GenerateRequestUriForStations(Constants.UKTidalEndpoind, searchBar.Text));

            if (stationsData != null)
            {
                BindingContext = stationsData.Stations;
            }
            else
            {
                BindingContext = null;
            }

        }

        async void OnItemSelected(object sender, EventArgs e)
        {
            ListView listView = (ListView)sender;
            StationData stationData = (StationData)listView.SelectedItem;

            if (listView.SelectedItem != null && howManyDays != "0" && stationData != null)
            {
                // TODO: Show user waiting
                bool isAdded = false;
                int tempDayCounter = 0;
                int howMany3HoursinGivenDay = Int32.Parse(howManyDays) * 8;

                WeatherData weatherData = await _weatherService.GetWeatherDataAsync(GenerateRequestUri(Constants.OpenWeatherMapEndpoint, stationData.Geometry.Coordinates[1], stationData.Geometry.Coordinates[0], howMany3HoursinGivenDay.ToString()));

                TidalEventsData[] tidalEventsData = await _tidalService.GetTidalEventsDataAsync(GenerateRequestUriForTidalEventsData(Constants.UKTidalEndpoind, stationData.Properties.Id, howManyDays));

                if (weatherData != null && tidalEventsData != null)
                {
                    string currentDate = DateTime.Now.ToString().Substring(3, 2);

                    List<TidalAndWeather> tidalAndWeathers = new List<TidalAndWeather>();

                    for (int i = 0; i < weatherData.Count; i++)
                    {
                        if (currentDate == weatherData.WeatherList[i].DtTxt.Substring(8, 2) && isAdded != false)
                        {
                            var obj = tidalAndWeathers.FirstOrDefault(x => x.Date.Substring(8, 2) == currentDate);
                            if (obj != null)
                            {
                                if (obj.MinTemperature > weatherData.WeatherList[i].Main.MinTemperature && weatherData.WeatherList[i].Main.MinTemperature < obj.Temperature) obj.MinTemperature = weatherData.WeatherList[i].Main.MinTemperature;
                                if (obj.MaxTemperature < weatherData.WeatherList[i].Main.MaxTemperature && weatherData.WeatherList[i].Main.MaxTemperature > obj.Temperature) obj.MaxTemperature = weatherData.WeatherList[i].Main.MaxTemperature;
                            }
                        }
                        else
                        {
                            if (tempDayCounter + 1 > Int32.Parse(howManyDays) * 4) break;

                            if (tidalEventsData.ElementAtOrDefault(tempDayCounter + 3) != null && tidalEventsData[tempDayCounter + 3].DateTime.Substring(8, 2) == currentDate)
                            {

                                tidalAndWeathers.Add(new TidalAndWeather()
                                {
                                    City = stationData.Properties.Name,
                                    Icon = weatherData.WeatherList[i].Weather[0].Icon,
                                    Date = weatherData.WeatherList[i].DtTxt.Substring(0, 10),
                                    Description = weatherData.WeatherList[i].Weather[0].Description,
                                    Temperature = weatherData.WeatherList[i].Main.Temperature,
                                    MinTemperature = weatherData.WeatherList[i].Main.MinTemperature,
                                    MaxTemperature = weatherData.WeatherList[i].Main.MaxTemperature,
                                    WindSpeed = weatherData.WeatherList[i].Wind.WindSpeed,
                                    Humidity = weatherData.WeatherList[i].Main.Humidity,
                                    FirstWater = tidalEventsData[tempDayCounter].getTypeAndHeight(),
                                    FirstWaterHeight = tidalEventsData[tempDayCounter].Height.ToString("0.##"),
                                    SecondWater = tidalEventsData[tempDayCounter + 1].getTypeAndHeight(),
                                    SecondWaterHeight = tidalEventsData[tempDayCounter + 1].Height.ToString("0.##"),
                                    ThirdWater = tidalEventsData[tempDayCounter + 2].getTypeAndHeight(),
                                    ThirdWaterHeight = tidalEventsData[tempDayCounter + 2].Height.ToString("0.##"),
                                    FourthWater = tidalEventsData[tempDayCounter + 3].getTypeAndHeight(),
                                    FourthWaterHeight = tidalEventsData[tempDayCounter + 3].Height.ToString("0.##")
                                });
                                tempDayCounter = tempDayCounter + 4;

                            }
                            else if (tidalEventsData.ElementAtOrDefault(tempDayCounter + 2) != null && tidalEventsData[tempDayCounter + 2].DateTime.Substring(8, 2) == currentDate)
                            {

                                tidalAndWeathers.Add(new TidalAndWeather()
                                {
                                    City = stationData.Properties.Name,
                                    Icon = weatherData.WeatherList[i].Weather[0].Icon,
                                    Date = weatherData.WeatherList[i].DtTxt.Substring(0, 10),
                                    Description = weatherData.WeatherList[i].Weather[0].Description,
                                    Temperature = weatherData.WeatherList[i].Main.Temperature,
                                    MinTemperature = weatherData.WeatherList[i].Main.MinTemperature,
                                    MaxTemperature = weatherData.WeatherList[i].Main.MaxTemperature,
                                    WindSpeed = weatherData.WeatherList[i].Wind.WindSpeed,
                                    Humidity = weatherData.WeatherList[i].Main.Humidity,
                                    FirstWater = tidalEventsData[tempDayCounter].getTypeAndHeight(),
                                    FirstWaterHeight = tidalEventsData[tempDayCounter].Height.ToString("0.##"),
                                    SecondWater = tidalEventsData[tempDayCounter + 1].getTypeAndHeight(),
                                    SecondWaterHeight = tidalEventsData[tempDayCounter + 1].Height.ToString("0.##"),
                                    ThirdWater = tidalEventsData[tempDayCounter + 2].getTypeAndHeight(),
                                    ThirdWaterHeight = tidalEventsData[tempDayCounter + 2].Height.ToString("0.##"),
                                });
                                tempDayCounter = tempDayCounter + 3;

                            }
                            else if (tidalEventsData.ElementAtOrDefault(tempDayCounter + 1) != null && tidalEventsData[tempDayCounter + 1].DateTime.Substring(8, 2) == currentDate)
                            {

                                tidalAndWeathers.Add(new TidalAndWeather()
                                {
                                    City = stationData.Properties.Name,
                                    Icon = weatherData.WeatherList[i].Weather[0].Icon,
                                    Date = weatherData.WeatherList[i].DtTxt.Substring(0, 10),
                                    Description = weatherData.WeatherList[i].Weather[0].Description,
                                    Temperature = weatherData.WeatherList[i].Main.Temperature,
                                    MinTemperature = weatherData.WeatherList[i].Main.MinTemperature,
                                    MaxTemperature = weatherData.WeatherList[i].Main.MaxTemperature,
                                    WindSpeed = weatherData.WeatherList[i].Wind.WindSpeed,
                                    Humidity = weatherData.WeatherList[i].Main.Humidity,
                                    FirstWater = tidalEventsData[tempDayCounter].getTypeAndHeight(),
                                    FirstWaterHeight = tidalEventsData[tempDayCounter].Height.ToString("0.##"),
                                    SecondWater = tidalEventsData[tempDayCounter + 1].getTypeAndHeight(),
                                    SecondWaterHeight = tidalEventsData[tempDayCounter + 1].Height.ToString("0.##"),
                                });
                                tempDayCounter = tempDayCounter + 2;

                            }
                            else if (tidalEventsData.ElementAtOrDefault(tempDayCounter) != null && tidalEventsData[tempDayCounter].DateTime.Substring(8, 2) == currentDate)
                            {

                                tidalAndWeathers.Add(new TidalAndWeather()
                                {
                                    City = stationData.Properties.Name,
                                    Icon = weatherData.WeatherList[i].Weather[0].Icon,
                                    Date = weatherData.WeatherList[i].DtTxt.Substring(0, 10),
                                    Description = weatherData.WeatherList[i].Weather[0].Description,
                                    Temperature = weatherData.WeatherList[i].Main.Temperature,
                                    MinTemperature = weatherData.WeatherList[i].Main.MinTemperature,
                                    MaxTemperature = weatherData.WeatherList[i].Main.MaxTemperature,
                                    WindSpeed = weatherData.WeatherList[i].Wind.WindSpeed,
                                    Humidity = weatherData.WeatherList[i].Main.Humidity,
                                    FirstWater = tidalEventsData[tempDayCounter].getTypeAndHeight(),
                                    FirstWaterHeight = tidalEventsData[tempDayCounter].Height.ToString("0.##"),
                                });
                                tempDayCounter = tempDayCounter + 1;

                            }

                            if (isAdded != false)
                            {
                                int tempDate = Int32.Parse(currentDate) + 1;
                                if (tempDate.ToString().Length == 1) currentDate = $"0{tempDate.ToString()}";
                                else currentDate = tempDate.ToString();
                            }
                            isAdded = true;
                        }
                    }

                    await Navigation.PushAsync(new TidalDetailPage(tidalAndWeathers));
                }
                else
                {
                    await DisplayAlert("Alert", "There is no tidal data for given location.", "OK");
                }
            }
            else if (howManyDays == "0")
            {
                await DisplayAlert("Alert", "You must choose how many days you want to see!", "OK");
            }
        }

        void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var howManyDaysPicker = (Picker)sender;
            int selectedIndex = howManyDaysPicker.SelectedIndex;

            if (selectedIndex != -1)
            {
                howManyDays = (string)howManyDaysPicker.ItemsSource[selectedIndex];
            }
        }

        async void useMyLocationClicked(object sender, EventArgs e)
        {
            if (howManyDays == "0")
            {
                await DisplayAlert("Alert", "You must choose how many days you want to see!", "OK");
            }
            else
            {
                try
                {
                    var location = await Geolocation.GetLastKnownLocationAsync();

                    if (location != null)
                    {
                        Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                        await DisplayAlert("Alert", $"Location: Latitude: {location.Latitude}, Longitude: {location.Longitude}", "OK");
                    } else
                    {
                        await DisplayAlert("Alert", $"Please give access to your location.", "OK");
                    }
                }
                catch (FeatureNotSupportedException fnsEx)
                {
                    // Handle not supported on device exception
                }
                catch (FeatureNotEnabledException fneEx)
                {
                    // Handle not enabled on device exception
                }
                catch (PermissionException pEx)
                {
                    // Handle permission exception
                }
                catch (Exception ex)
                {
                    // Unable to get location
                }
            }
        }

        string GenerateRequestUri(string endpoint, double lat, double lon, string cnt)
        {
            string requestUri = endpoint;
            requestUri += $"?lat={lat}";
            requestUri += $"&lon={lon}";
            requestUri += "&units=metric";
            requestUri += $"&cnt={cnt}";
            requestUri += $"&APPID={Constants.OpenWeatherMapAPIKey}";
            return requestUri;
        }

        string GenerateRequestUriForStations(string endpoint, string searchBarText)
        {
            string requestUri = endpoint;
            string location = searchBarText;
            requestUri += $"/Stations?name={location}";
            return requestUri;
        }

        string GenerateRequestUriForTidalEventsData(string endpoint, string id, string days)
        {
            string requestUri = endpoint;
            requestUri += $"/Stations/{id}/TidalEvents?duration={days}";
            return requestUri;
        }
    }
}
