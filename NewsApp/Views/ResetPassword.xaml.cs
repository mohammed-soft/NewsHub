using Firebase.Auth;

namespace NewsApp.Views;

public partial class ResetPassword : ContentPage
{
    private readonly FirebaseAuthClient _firebaseAuth;

    public ResetPassword(FirebaseAuthClient firebaseAuth)
    {
        InitializeComponent();
        _firebaseAuth = firebaseAuth;
    }

    private async void resetButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            await _firebaseAuth.ResetEmailPasswordAsync(emailEntry.Text);

            // Handle success
            await DisplayAlert("Success", "Password reset email sent.", "OK");
            await Shell.Current.GoToAsync("..");
        }
        catch (FirebaseAuthException ex)
        {
            // Handle failure
            await DisplayAlert("Error", ex.Message, "OK");
        }
        catch (Exception ex)
        {
            // Handle other exceptions
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
}