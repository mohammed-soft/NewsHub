using NewsAPI;
using NewsAPI.Constants;
using NewsAPI.Models;
using System.Collections.Generic;

namespace NewsApp.Views
{
    public partial class SearchResult : ContentPage, IQueryAttributable
    {
        
        private int _pageNumber = 1;
        private int _pageSize = 10;
        
        private readonly NewsApiClient _newsApiClient;

        public SearchResult(NewsApiClient newsApiClient)
        {
            InitializeComponent();
            _newsApiClient = newsApiClient;

        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (SearchResultList?.ItemsSource?.Cast<object>()?.Count() > 0)
                return;
            if (query.ContainsKey("SearchText"))
            {
                var searchText = query["SearchText"] as string;
                await Search(searchText);
            }
            isLoading.IsVisible = false;
        }

        private async Task Search(string searchText)
        {
            try
            {
                var articles = await _newsApiClient.GetEverythingAsync(new EverythingRequest
                {
                    Q = searchText,
                    SortBy = SortBys.Popularity
                });

                if (articles.Status == Statuses.Ok && articles.TotalResults > 0)
                {
                    SearchResultList.ItemsSource = articles.Articles;
                }
                else
                {
                    await DisplayAlert("No Results", "No articles found for the given search term.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
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

}
