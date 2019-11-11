using System;
using System.Collections.Generic;
using Surfly.Services;
using Surfly.Helpers;
using Xamarin.Forms;
using Surfly.Models;
using Xamarin.Forms.Internals;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Surfly.Views
{
    public partial class SearchPage : ContentPage
    {
        TidalService _tidalService;

        public SearchPage()
        {
            InitializeComponent();
            _tidalService = new TidalService();
        }

        async void OnTextChangedAsync(object sender, EventArgs e)
        {
            SearchBar searchBar = (SearchBar)sender;
            StationsData stationsData = await _tidalService.GetStationsDataAsync(GenerateRequestUriForStations(Constants.UKTidalEndpoind, searchBar.Text));
            
            if (stationsData != null)
            {
                BindingContext = stationsData.Stations;
            } else
            {
                BindingContext = null;
            }
            
        }

        string GenerateRequestUriForStations(string endpoint, string searchBarText)
        {
            string requestUri = endpoint;
            string location = searchBarText; // need to get from user
            requestUri += $"/Stations?name={location}";
            return requestUri;
        }

        async void OnItemSelected(object sender, EventArgs e)
        {
            ListView listView = (ListView)sender;

            if (listView.SelectedItem != null)
            {
                StationData stationData = (StationData)listView.SelectedItem;
                await Navigation.PushModalAsync(new TidalDetailPage(stationData.Properties.Id));
            }
        }
    }
}
