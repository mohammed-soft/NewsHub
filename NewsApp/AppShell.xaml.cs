using NewsApp.Views;

namespace NewsApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Application.Current!.UserAppTheme = AppTheme.Light;
            Routing.RegisterRoute(nameof(SearchResult), typeof(SearchResult));
            Routing.RegisterRoute(nameof(ArticalDetails), typeof(ArticalDetails));
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute("Signup", typeof(Signup));
            Routing.RegisterRoute("Signin", typeof(Signin));
            Routing.RegisterRoute("Profile", typeof(Profile));
            Routing.RegisterRoute("ResetPassword", typeof(ResetPassword));
            Routing.RegisterRoute("ExploreSource", typeof(ExploreSource));




        }
    }
}
