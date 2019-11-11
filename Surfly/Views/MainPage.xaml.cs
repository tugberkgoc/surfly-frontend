using System;
using Xamarin.Forms;
using Surfly.Models;
using Surfly.Services;
using Surfly.Helpers;

namespace Surfly.Views
{
    public partial class MainPage : ContentPage
    {

        WeatherService _weatherService;

        public MainPage()
        {
            InitializeComponent();
            _weatherService = new WeatherService();
        }

        async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            App.IsUserLoggedIn = false;
            Navigation.InsertPageBefore(new LoginPage(), this);
            await Navigation.PopAsync();
        }

        async void OnButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(cityEntry.Text))
            {

                WeatherData weatherData = await _weatherService.GetWeatherDataAsync(GenerateRequestUri(Constants.OpenWeatherMapEndpoint));
                BindingContext = weatherData;

            }
        }

        string GenerateRequestUri(string endpoint)
        {
            string requestUri = endpoint;
            requestUri += $"?q={cityEntry.Text}";
            requestUri += "&units=imperial"; // or units=metric
            requestUri += $"&APPID={Constants.OpenWeatherMapAPIKey}";
            return requestUri;
        }
    }
}
