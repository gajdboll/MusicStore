using MusicApp.Pages;

namespace MusicApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override async void OnStart()
        {
            base.OnStart();

             await Shell.Current.GoToAsync("login-page");
        }
    }
}