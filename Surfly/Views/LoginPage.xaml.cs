using System;
using Xamarin.Forms;
using Surfly.Models;
using Surfly.Services;
using Surfly.Helpers;

namespace Surfly.Views
{
    public partial class LoginPage : ContentPage
    {

        LoginService _loginService;

        public LoginPage()
        {
            InitializeComponent();
            _loginService = new LoginService();
        }

        async void OnSignUpButtonClicked(object sender, EventArgs e)
        {
            usernameEntry.Text = string.Empty;
            passwordEntry.Text = string.Empty;
            await Navigation.PushAsync(new SignUpPage());
        }

        async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            var user = new User
            {
                Username = usernameEntry.Text,
                Password = passwordEntry.Text
            };

            if ((!string.IsNullOrWhiteSpace(user.Username) && !string.IsNullOrWhiteSpace(user.Password)))
            {
                messageLabel.Text = "Processing...";

                ResponseData responseData = await _loginService.LoginUserAsync($"{Constants.BackendAPIEndpoint}/auth/login", user);

                if (responseData != null)
                {
                    if (responseData.Status == "success")
                    {
                        App.IsUserLoggedIn = true;
                        App.Username = user.Username;
                        Navigation.InsertPageBefore(new MainTabbedPage(), this);
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        messageLabel.Text = "Login failed";
                        passwordEntry.Text = string.Empty;
                    }
                } else
                {
                    messageLabel.Text = "Check Internet Connection!";
                    passwordEntry.Text = string.Empty;
                }
            }
            else
            {
                messageLabel.Text = "Please fill the required fields.";
            }
        }
    }
}