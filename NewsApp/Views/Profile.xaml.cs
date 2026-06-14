using Firebase.Auth;
using NewsApp.Models;
using NewsApp.Utils;

namespace NewsApp.Views;

public partial class Profile : ContentPage
{
	public Profile()
	{
		InitializeComponent();
	}

    protected async override void OnAppearing()
    {
        var userData = (await SecureStorage.Default.GetAsync("user"))?.ToObject<UserData>();
        if (userData is not null)
        {
            // show signout button
            welcomeLabel.Text = $"Welcome {userData.Name}";
            UserSignedin.IsVisible = true;
            NoUser.IsVisible = false;
        }
        else
        {
            //show signin and signup buttons 
            UserSignedin.IsVisible = false;
            NoUser.IsVisible = true;
        }
        base.OnAppearing();
    }

    private async void OnSignupClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("Signup");
    }

    private async void OnSigninClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("Signin");
    }

    private async void OnSignoutClicked(object sender, EventArgs e)
    {
        Preferences.Default.Remove("savedArticels");
        SecureStorage.Default.Remove("user");
        await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
    }
}