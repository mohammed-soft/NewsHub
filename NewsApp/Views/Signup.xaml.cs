using CommunityToolkit.Maui.Alerts;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using NewsApp.Models;

namespace NewsApp.Views;

public partial class Signup : ContentPage
{
    private readonly FirebaseAuthClient _authClient;
    private readonly FirebaseClient _databaseClient;
    public Signup(FirebaseAuthClient authClient, FirebaseClient databaseClient)
    {
        _authClient = authClient;
        _databaseClient = databaseClient;
        InitializeComponent();
    }
    private async void OnSignUpClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(NameEntry.Text))
        {
            await DisplayAlert("Error", "please enter your name!", "ok");
            return;
        }

        if (string.IsNullOrWhiteSpace(emailEntry.Text))
        {
            await DisplayAlert("Error", "please enter your email!", "ok");
            return;
        }

        if (string.IsNullOrWhiteSpace(passwordEntry.Text))
        {
            await DisplayAlert("Error", "please enter your password!", "ok");
            return;
        }

        try
        {
            var authResult = await _authClient.CreateUserWithEmailAndPasswordAsync(emailEntry.Text, passwordEntry.Text, NameEntry.Text);
            if (string.IsNullOrEmpty(authResult?.User?.Info?.Uid))
            {
                await DisplayAlert("Error", "failed to signup!", "Ok");
                return;
            }

            await _databaseClient.Child("Users")
                .Child(authResult.User.Info.Uid)
                .PutAsync(new Models.UserData()
                {
                    Name = NameEntry.Text
                });

            var toast = Toast.Make("signup succeeded", CommunityToolkit.Maui.Core.ToastDuration.Short);

            await toast.Show(default);
            await Shell.Current.GoToAsync("Signin");

        }
        catch (FirebaseAuthException fex)
        {
            await DisplayAlert("Error", fex.Message, "Ok");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "Ok");
        }
    }

}