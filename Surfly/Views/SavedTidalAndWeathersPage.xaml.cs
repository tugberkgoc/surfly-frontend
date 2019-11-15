using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Surfly.Helpers;
using Surfly.Models;
using Surfly.Services;
using Xamarin.Forms;

namespace Surfly.Views
{
    public partial class SavedTidalAndWeathersPage : ContentPage
    {
        TidalAndWeatherService _tidalAndWeatherService;
        List<TidalAndWeather> empty { get; set; }

        public SavedTidalAndWeathersPage()
        {
            InitializeComponent();
            empty = new List<TidalAndWeather>();
            tidalAndWeatherListView.ItemsSource = empty;
            _tidalAndWeatherService = new TidalAndWeatherService();
            UpdatePage();
        

        async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            App.IsUserLoggedIn = false;
            App.Username = null;
            Navigation.InsertPageBefore(new LoginPage(), Navigation.NavigationStack.First());
            await Navigation.PopToRootAsync();
        }

        async void OnDelete(object sender, EventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            TidalAndWeather newTidalAndWeather = (Surfly.Models.TidalAndWeather)menuItem.CommandParameter;

            ResponseData responseData = await _tidalAndWeatherService.DeleteTidalAndWeatherData(GenerateDeleteTidalAndWeather(Constants.BackendAPIEndpoint), newTidalAndWeather);
            if (responseData.Status == "success")
            {
                UpdatePage();
            }
        }

        void GetSavedData(object sender, EventArgs e)
        {
            UpdatePage();
        }

        async void UpdatePage()
        {
            List<TidalAndWeather> tidalAndWeathers = await _tidalAndWeatherService.GetSavedTidalAndWeather(GenerateGetTidalAndWeather(Constants.BackendAPIEndpoint));
            tidalAndWeatherListView.ItemsSource = tidalAndWeathers;
        }

        string GenerateGetTidalAndWeather(string endpoint)
        {
            string requestUri = endpoint;
            requestUri += $"/station?Username={App.Username}";
            return requestUri;
        }

        string GenerateDeleteTidalAndWeather(string endpoint)
        {
            string requestUri = endpoint;
            requestUri += $"/station/delete?Username={App.Username}";
            return requestUri;
        }

    }
}
