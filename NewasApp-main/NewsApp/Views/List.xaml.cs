using Firebase.Database;
using Firebase.Database.Query;
using NewsAPI.Models;
using NewsApp.Models;
using NewsApp.Utils;

namespace NewsApp.Views;

public partial class List : ContentPage
{

    private const string savedArticlesKey = "savedArticels";
    private readonly FirebaseClient _databaseClient;

    public List(FirebaseClient databaseClient)
    {
        InitializeComponent();
        _databaseClient = databaseClient;
    }

    protected async override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        try
        {
            var userData = (await SecureStorage.Default.GetAsync("user"))?.ToObject<UserData>();
            if (userData is not null)
            {
                string articales = Preferences.Default.Get(savedArticlesKey, "");
                var savedArticles = articales.ToObject<SavedArticles>();
                if (savedArticles is not null && savedArticles.UserId.Equals(userData.Id) && savedArticles?.Articles is not null)
                    SavedArticales.ItemsSource = savedArticles.Articles;
                else
                {
                    //try get it from firebase
                    UserData userDataResult = await _databaseClient.Child("Users")
                        .Child(userData.Id)
                        .OnceSingleAsync<UserData>();

                    SavedArticles userArticles = new SavedArticles()
                    {
                        UserId = userData.Id,
                        Articles = userDataResult.SavedArticles
                    };
                    if (userArticles.Articles?.Count > 0)
                    {
                        Preferences.Default.Set(savedArticlesKey, userArticles.ToJson());
                        SavedArticales.ItemsSource = userArticles.Articles;
                    }
                }
                return;
            }

            //show signin and signup buttons 
            SavedArticales.ItemsSource = new List<Article>();
        }
        catch (Exception ex)
        {
            await DisplayAlert("error", ex.Message, "ok");
        }

        base.OnNavigatedTo(args);
    }

    private void SavedArticles_RemainingItemsThresholdReached(object sender, EventArgs e)
    {

    }


    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        var selected = (e.Parameter as Article);
        if (selected is null)
            return;
        try
        {
            var par = new Dictionary<string, object>() { { "Article", selected } };
            // await Browser.OpenAsync(((Article)selected).Url, BrowserLaunchMode.External);
            await Shell.Current.GoToAsync(nameof(ArticalDetails), true, par);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
}