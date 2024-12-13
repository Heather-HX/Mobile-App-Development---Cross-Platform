using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LibraryApp
{
    public partial class MainPage : ContentPage
    {
        private readonly Dictionary<string, string> _users = new Dictionary<string, string>
        {
            { "peter", "1234" },
            { "mary", "0000" }
        };

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            var username = UsernameEntry.Text;
            var password = PasswordEntry.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Error", "Please enter gboth username and password.", "OK");
                return;
            }

            if (_users.ContainsKey(username) && _users[username] == password)
            {
                await Navigation.PushAsync(new BooksListPage(username));
            }
            else
            {
                await DisplayAlert("Error", "Invalid username or password.", "OK");
            }
        }
    }
}
