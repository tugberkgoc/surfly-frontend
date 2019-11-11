using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Surfly.Views
{
    public partial class TidalDetailPage : ContentPage
    {
        public int myId { get; } = 0;

        public TidalDetailPage(int id)
        {
            myId = id;
            InitializeComponent();
            BindingContext = this;
        }

        async void OnDismissButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PopModalAsync();
        }

        async void OnSaveButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PopModalAsync();
        }
    }
}
