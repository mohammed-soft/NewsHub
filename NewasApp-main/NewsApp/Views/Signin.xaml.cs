using CommunityToolkit.Maui.Alerts;
using Firebase.Auth;
using Firebase.Auth.Requests;
using Firebase.Database;
using Firebase.Database.Query;
using NewsApp.Models;
using NewsApp.Utils;


namespace NewsApp.Views;

public partial class Signin : ContentPage
{
    private readonly FirebaseAuthClient _authClient;
    private readonly FirebaseClient _databaseClient;

    public Signin(FirebaseAuthClient authClient, FirebaseClient databaseClient)
    {
        _authClient = authClient;
        _databaseClient = databaseClient;
        InitializeComponent();
    }

    private async void Signin_Clicked(object sender, EventArgs e) 
    {
        try
        {
            var authresult = await _authClient.SignInWithEmailAndPasswordAsync(emailEntry.Text, PasswordEntry.Text);
            UserData userData = await _databaseClient.Child("Users")
                .Child(authresult.User.Uid)
                .OnceSingleAsync<UserData>();
            UserData userInfo = new UserData()
            {
                Id = authresult.User.Uid,
                Name = userData.Name,
            };
            await SecureStorage.Default.SetAsync("user", userInfo.ToJson());
            var toast = Toast.Make("Sign in succeeded", CommunityToolkit.Maui.Core.ToastDuration.Short);

            await toast.Show(default);
            await Shell.Current.Navigation.PopToRootAsync(false);
            await Shell.Current.GoToAsync("//MainPage");


        }
        catch (FirebaseAuthException fex)
        {
            await DisplayAlert("Authentication Error", "Invalid email/password!\nTry again please", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
    private async void OnForgotPasswordTapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("ResetPassword");
    }



}
