using CommunityToolkit.Maui;
using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NewsAPI;
using NewsApp.Config;
using NewsApp.Views;

namespace NewsApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            // 1. تحميل الإعدادات بالطريقة الصحيحة والمباشرة
            var config = LoadConfiguration();

            // 2. قراءة القيم بأمان (تجنب القيمة null لتفادي توقف التطبيق)
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

            // 3. تهيئة الخدمات باستخدام القيم المسحوبة من الإعدادات
            builder.Services.AddSingleton(new FirebaseAuthClient(new FirebaseAuthConfig()
            {
                ApiKey = FirebaseConfig.ApiKey,
                AuthDomain = FirebaseConfig.AuthDomain,
                Providers = [new EmailProvider()]
            }));

            builder.Services.AddSingleton(new FirebaseClient(FirebaseConfig.DatabaseUrl));

            builder.Services.AddSingleton(new NewsApiClient(NewsAPIConfig.ApiKey2));

            // 4. تسجيل الصفحات في حاوية الحقن لتمكين Constructor Injection
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<Explore>();
            builder.Services.AddTransient<ExploreSource>();
            builder.Services.AddTransient<SearchResult>();
            builder.Services.AddTransient<List>();
            builder.Services.AddTransient<ArticalDetails>();
            builder.Services.AddTransient<Signin>();
            builder.Services.AddTransient<Signup>();
            builder.Services.AddTransient<ResetPassword>();
            builder.Services.AddTransient<Profile>();

            return builder.Build();
        }

        private static IConfiguration LoadConfiguration()
        {
            // الاعتماد على المسار الأساسي للمشروع
            // تأكد من تفعيل (Copy if newer) لملفات الـ JSON لكي يعمل هذا الكود
            var basePath = AppDomain.CurrentDomain.BaseDirectory;

            return new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
                .Build();
        }
    }
}