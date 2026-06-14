using NewsAPI;
using NewsAPI.Models;

namespace NewsApp.Views;

public partial class ExploreSource : ContentPage, IQueryAttributable
{
    private Source _source;
    private readonly NewsApiClient _newsApiClient;

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("Source"))
        {
            _source = (Source)query["Source"];
            LoadData();
        }
    }

    

    public ExploreSource(NewsApiClient newsApiClient)
	{
		InitializeComponent();
        _newsApiClient = newsApiClient;
    }

    private async void LoadData()
    {
        if (_source != null)
        {
            var articlesResponse = await _newsApiClient.GetTopHeadlinesAsync(new TopHeadlinesRequest
            {
                Sources = new List<string> { _source.Id },
                PageSize = 20
            });

            if (articlesResponse is not null && articlesResponse.Articles?.Count > 0)
            {
                LatestNews.ItemsSource = articlesResponse.Articles;
            }
            else
            {
                await DisplayAlert("Error", "Failed to load articles.", "OK");
            }
        }

        isLoading.IsVisible = false;
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