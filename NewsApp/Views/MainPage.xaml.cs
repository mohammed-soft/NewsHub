using NewsAPI;
using NewsAPI.Constants;
using NewsAPI.Models;
using NewsApp.Views;
using System.Collections.Generic;

namespace NewsApp
{
    public partial class MainPage : ContentPage
    {
        private int _pageNumber = 1;
        private int _pageSize = 10;
        
        private readonly NewsApiClient _newsApiClient;

        public MainPage(NewsApiClient newsApiClient)
        {
            InitializeComponent();
            _newsApiClient = newsApiClient;


            Date.Text = DateTime.Now.ToShortDateString();

        }

        protected async override void OnNavigatedTo(NavigatedToEventArgs args)
        {
            //return; // for testing
            if (Trending?.Height != -1)
                return;
            Trending.ItemsSource = new List<Article>();
            isLoading.IsVisible = true;
            if (LatestNews.ItemsSource is null)
                await LoadNews(1);
            await LoadTopHeadLines();
            isLoading.IsVisible = false;
        }

        private async Task<List<Article>?> LoadNews(int pageNumber, string? query = null)
        {
            try
            {
                var items = new List<Article>();
                var latest = await _newsApiClient.GetEverythingAsync(new EverythingRequest()
                {
                    Page = pageNumber,
                    SortBy = query is null ? SortBys.PublishedAt : SortBys.Relevancy,
                    PageSize = _pageSize,
                    Q = query ?? "Saudi"
                });
                if (latest is not null && latest?.Articles?.Count > 0)
                {
                    if (query is null)
                    {
                        if (LatestNews.ItemsSource is not null)
                            items.AddRange((IEnumerable<Article>)LatestNews.ItemsSource);
                        items.AddRange(latest.Articles);
                        LatestNews.ItemsSource = items;
                    }
                    return items;
                }

                if (!string.IsNullOrEmpty(latest?.Error?.Message))
                    await DisplayAlert("Error", latest.Error.Message, "OK");

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
            return null;
        }

        private async Task LoadTopHeadLines(Categories? category = null)
        {
            try
            {
                var request = new TopHeadlinesRequest()
                {
                    Page = 1,
                    PageSize = 10,
                    Language = Languages.EN
                };

                if (category is not null)
                {
                    request.Category = category;
                }

                var trending = await _newsApiClient.GetTopHeadlinesAsync(request);
                if (trending is not null && trending?.Articles?.Count > 0)
                {
                    Trending.ItemsSource = trending.Articles.Where(x => !string.IsNullOrWhiteSpace(x.Url));
                }

                //if (!string.IsNullOrEmpty(trending?.Error?.Message))
                //    await DisplayAlert("Error", trending.Error.Message, "OK");

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async void Category_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            isLoading.IsVisible = true;
            if (e.CurrentSelection?.Count < 1)
                return;
            try
            {
                var categoryExist = Enum.TryParse((string)e.CurrentSelection.FirstOrDefault(), out Categories category);
                await LoadTopHeadLines(categoryExist ? category : null);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
            isLoading.IsVisible = false;
        }

        private async void Article_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection?.Count < 1)
                return;
            try
            {
                await Browser.OpenAsync(((Article)e.CurrentSelection.FirstOrDefault()).Url, BrowserLaunchMode.External);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async void Trending_CurrentItemChanged(object sender, CurrentItemChangedEventArgs e)
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


        private async void LatestNews_RemainingItemsThresholdReached(object sender, EventArgs e)
        {
            loadMoreBtn.IsEnabled = false;
            await LoadNews(++_pageNumber);
            loadMoreBtn.IsEnabled = true;
        }

        private async void Search_Clicked(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(searchEntry.Text))
            {
                await DisplayAlert("Error", "Please enter a search term.", "OK");
                return;
            }
            var par = new Dictionary<string, object>() { { "SearchText", searchEntry.Text } };
            await Shell.Current.GoToAsync(nameof(SearchResult), true, par);
        }
    }

}
