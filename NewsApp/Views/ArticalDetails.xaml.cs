using NewsAPI.Models;
using Microsoft.Maui.Controls;
using NewsApp.Utils;
using NewsApp.Models;
using Firebase.Database;
using Firebase.Database.Query; //  √ﬂœ √‰ﬂ ÷«Ì› using Â–« ·Ê «Õ Ã Â

namespace NewsApp;

public partial class ArticalDetails : ContentPage, IQueryAttributable
{
    private Article SelectedArticle;
    private SavedArticles SavedArticales;
    private const string savedArticels = "savedArticels";
    private readonly FirebaseClient _databaseClient;
    private UserData _userData;

    public ArticalDetails(FirebaseClient databaseClient)
    {
        InitializeComponent();
        _databaseClient = databaseClient;
    }

    protected async override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        _userData = (await SecureStorage.Default.GetAsync("user"))?.ToObject<UserData>();
        base.OnNavigatedTo(args);
    }


    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        string articales = Preferences.Default.Get(savedArticels, "");
        SavedArticales = articales?.ToObject<SavedArticles>(new SavedArticles());
        Article article = query.Values.ElementAtOrDefault(0) as Article;
        if (article != null)
        {
            SelectedArticle = article;

            AuthorName.Text = article.Author ?? "Unknown Author";
            ArticleTitle.Text = article.Title;
            if (!string.IsNullOrEmpty(article.UrlToImage))
            {
                ArticleImage.Source = ImageSource.FromUri(new Uri(article.UrlToImage));
            }
            ArticleDetails.Text = article.Description ?? "No Details Available";
        }

        if (SavedArticales is not null
            && SavedArticales.Articles?.Count > 0
            && SavedArticales.Articles.Any(x => x.Url.Equals(SelectedArticle.Url)))
        {
            unsaved.IsVisible = false;
            saved.IsVisible = true;
        }
    }

    private async void ReadMoreButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (SelectedArticle != null && !string.IsNullOrEmpty(SelectedArticle.Url))
            {
                await Browser.OpenAsync(SelectedArticle.Url, BrowserLaunchMode.External);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    private async void unsaved_Clicked(object sender, EventArgs e)
    {
        if (_userData is not null)
        {
            SavedArticales.UserId = _userData.Id;
            SavedArticales?.Articles?.Insert(0, SelectedArticle);
            Preferences.Default.Set(savedArticels, SavedArticales.ToJson());
            var firebaseObject = new UserData()
            {
                Id = _userData.Id,
                SavedArticles = SavedArticales.Articles
            };
            await _databaseClient.Child("Users")
                .Child(_userData.Id)
                .PutAsync(firebaseObject.ToJson());
            unsaved.IsVisible = false;
            saved.IsVisible = true;
        }
        else
        {
            await DisplayAlert("Warning", "please signin to use this feature!", "ok");
        }
    }
    private async void saved_Clicked(object sender, EventArgs e)
    {
        if (_userData is not null)
        {
            SavedArticales.UserId = _userData.Id;
            SavedArticales?.Articles?.RemoveAll(x => x.Url.Equals(SelectedArticle.Url));
            Preferences.Default.Set(savedArticels, SavedArticales.ToJson());
            var firebaseObject = new UserData()
            {
                Id = _userData.Id,
                SavedArticles = SavedArticales.Articles
            };
            await _databaseClient.Child("Users")
                .Child(_userData.Id)
                .PutAsync(firebaseObject.ToJson());
            unsaved.IsVisible = true;
            saved.IsVisible = false;
        }
        else
        {
            await DisplayAlert("Warning", "please signin to use this feature!", "ok");
        }
    }
}
