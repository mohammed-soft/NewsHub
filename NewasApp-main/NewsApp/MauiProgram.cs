using CommunityToolkit.Maui;
using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Database;
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
        private static void SetupRoutes()
        {
            Routing.RegisterRoute(nameof(ArticalDetails), typeof(ArticalDetails));

        }
    }
}
