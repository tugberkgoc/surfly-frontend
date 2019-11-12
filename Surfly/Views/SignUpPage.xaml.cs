using System;
using System.Linq;
using Xamarin.Forms;
using Surfly.Models;
using Surfly.Services;
using Surfly.Helpers;
using System.Threading.Tasks;

namespace Surfly.Views
{
    public partial class SignUpPage : ContentPage
    {

        SignUpService _signUpService;

        public SignUpPage()
        {
            InitializeComponent();
            _signUpService = new SignUpService();
        }

        async void OnSignUpButtonClicked(object sender, EventArgs e)
        {
            var user = new User()
            {
                Username = usernameEntry.Text,
                Password = passwordEntry.Text,
                Email = emailEntry.Text
            };

            if((!string.IsNullOrWhiteSpace(user.Username) && !string.IsNullOrWhiteSpace(user.Password) && !string.IsNullOrWhiteSpace(user.Email) && user.Email.Contains("@")))
            {
                messageLabel.Text = "Processing...";

                ResponseData responseData = await _signUpService.SignUpUserAsync($"{Constants.BackendAPIEndpoint}/auth/signup", user);

                if (responseData != null)
                {
                    if (responseData.Status == "success")
                    {
                        var rootPage = Navigation.NavigationStack.FirstOrDefault();
                        if (rootPage != null)
                        {
                            App.IsUserLoggedIn = true;
                            App.Username = user.Username;
                            Navigation.InsertPageBefore(new MainTabbedPage(), Navigation.NavigationStack.First());
                            await Navigation.PopToRootAsync();
                        }
                    }
                    else
                    {
                        messageLabel.Text = "Sign up failed";
                    }
                } else
                {
                    messageLabel.Text = "Check Internet Connection!";
                    passwordEntry.Text = string.Empty;
                } 
            } else
            {
                messageLabel.Text = "Please fill the required fields.";
            }
        }
    }
}
