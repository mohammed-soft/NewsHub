using CommunityToolkit.Maui;
using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NewsAPI;
using NewsApp.Config;

namespace NewsApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            
            // Load configuration
            var config = LoadConfiguration();
            
            // Set config values
            FirebaseConfig.ApiKey = config["Firebase:ApiKey"] ?? "";
            FirebaseConfig.AuthDomain = config["Firebase:AuthDomain"] ?? "";
            FirebaseConfig.DatabaseUrl = config["Firebase:DatabaseUrl"] ?? "";
            NewsAPIConfig.ApiKey = config["NewsAPI:ApiKey"] ?? "";
            NewsAPIConfig.ApiKey2 = config["NewsAPI:ApiKey2"] ?? "";
            
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit(options =>
                {
                    options.SetShouldEnableSnackbarOnWindows(true);
                })
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton(new FirebaseAuthClient(new FirebaseAuthConfig()
            {
                ApiKey = FirebaseConfig.ApiKey,
                AuthDomain = FirebaseConfig.AuthDomain,
                Providers = [new EmailProvider()]
            }));

            builder.Services.AddSingleton(new FirebaseClient(FirebaseConfig.DatabaseUrl));

            builder.Services.AddSingleton(new NewsApiClient(NewsAPIConfig.ApiKey2));

            return builder.Build();
        }
        
        private static IConfiguration LoadConfiguration()
        {
            var configBuilder = new ConfigurationBuilder();
            
            // Try to load from the app data directory first (for production)
            var appDataPath = FileSystem.Current.AppDataDirectory;
            if (File.Exists(Path.Combine(appDataPath, "appsettings.json")))
            {
                configBuilder.SetBasePath(appDataPath)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            }
            else
            {
                // For development, load from the project directory
                var projectPath = AppDomain.CurrentDomain.BaseDirectory;
                configBuilder.SetBasePath(projectPath)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);
            }
            
            return configBuilder.Build();
        }
        
        private static void SetupRoutes()
        {
            Routing.RegisterRoute(nameof(ArticalDetails), typeof(ArticalDetails));

        }
    }
}
