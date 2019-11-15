using System;
using System.Collections.Generic;
using System.Linq;
using Surfly.Helpers;
using Surfly.Models;
using Surfly.Services;
using Xamarin.Forms;

namespace Surfly.Views
{
    public partial class TidalDetailPage : ContentPage
    {

        List<TidalAndWeather> _tidalAndWeather;
        TidalAndWeatherService _tidalAndWeatherService;

        public TidalDetailPage(List<TidalAndWeather> tidalAndWeather)
        {
            InitializeComponent();
            tidalAndWeatherListView.ItemsSource = tidalAndWeather;
            _tidalAndWeather = tidalAndWeather;
            _tidalAndWeatherService = new TidalAndWeatherService();
        }

        async void OnDismissButtonClicked(object sender, EventArgs args)
        {
            // TODO: Check here, im not quite sure about it
            await Navigation.PopAsync();
        }

        async void OnSaveButtonClicked(object sender, EventArgs args)
        {
            ResponseData responseData = await _tidalAndWeatherService.PostSaveTidalAndWeather(GeneratePostSaveTidalAndWeather(Constants.BackendAPIEndpoint), _tidalAndWeather);
            if (responseData.Status == "success")
            {
                await Navigation.PopAsync();
            } else
            {
                await DisplayAlert("Alert", "Stations were not saved because of Internet Connection.", "OK");
            }
        }

        string GeneratePostSaveTidalAndWeather(string endpoint)
        {
            string requestUri = endpoint;
            requestUri += $"/station?Username={App.Username}";
            return requestUri;
        }
    }
}
