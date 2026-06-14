using NewsAPI;
using NewsAPI.Models;
using NewsApp.Config;
using NewsApp.Models;
using NewsApp.Utils;
using System.Collections.ObjectModel;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace NewsApp.Views;

public partial class Explore : ContentPage
{
    private readonly NewsApiClient _newsApiClient;

    private ObservableCollection<Source> _allSources;

    public Explore(NewsApiClient newsApiClient)
    {
        InitializeComponent();
        _newsApiClient = newsApiClient;
        LoadData();
    }

    protected override void OnAppearing()
    {
        LoadData();
        base.OnAppearing();
    }

    private async void LoadData()
    {
        var sourcesResponse = await _newsApiClient.GetSourcesAsync();
        if (sourcesResponse is not null)
        {
            _allSources = new ObservableCollection<Source>(sourcesResponse);
            SourcesCollectionView.ItemsSource = _allSources;
        }
        else
        {
            await DisplayAlert("Error", "Failed to load sources.", "OK");
        }
        isLoading.IsVisible = false;
    }

    private void Search_TextChanged(object sender, TextChangedEventArgs e)
    {
        var searchTerm = e.NewTextValue ?? "";
        var filtered = _allSources?
            .Where(source => source.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))?
            .ToList();

        SourcesCollectionView.ItemsSource = filtered;
    }

    private async void OnExploreClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var selectedSource = button?.CommandParameter as Source;

        if (selectedSource != null)
        {
            // Navigate to the articles page with the selected source
            var parameters = new Dictionary<string, object>
            {
                { "Source", selectedSource }
            };
            await Shell.Current.GoToAsync(nameof(ExploreSource), true, parameters);
            //await DisplayAlert("Source Clicked", selectedSource.Name, "OK");
        }
    }



}

internal static class NewsAPISources
{
    public static async Task<List<Source>?> GetSourcesAsync(this NewsApiClient newsApiClient)
    {
        List<Source>? sources = JsonSerializer.Deserialize<List<Source>>(NewsAPIConfig.Sources, JsonSerializerOptions.Web);

        if (sources is not null)
            return sources;

        try
        {
            var httpClient = new HttpClient();


            httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("NewsApp", "1.0"));

            var response = await httpClient.GetAsync($"https://newsapi.org/v2/sources?apiKey={NewsAPIConfig.ApiKey}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<NewsAPISourcesResult>();
                sources = result?.Sources;
                return sources;
            }
            else
            {
                // Log response content for debugging
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error: {errorContent}");
            }
        }
        catch (Exception ex)
        {
            //no need to throw as function is returning null in case an error!
        }

        return null;
    }
}
