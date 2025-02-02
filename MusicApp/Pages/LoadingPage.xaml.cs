namespace MusicApp.Pages;

public partial class LoadingPage : ContentPage
{
	public LoadingPage()
	{
		InitializeComponent();
	}
    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        try
        {
             await Task.Delay(200);

            if (await isAuthenticated())
            {
                await Shell.Current.GoToAsync("///home-tab");   
            }
            else
            {
                await Shell.Current.GoToAsync("///login-page");  
            }
        }
        catch (Exception ex)
        {
             await DisplayAlert("Error", ex.Message, "OK");
        }

        base.OnNavigatedTo(args);
    }


    async Task<bool> isAuthenticated()
    {
        await Task.Delay(2000);  
        var hasAuth = await SecureStorage.GetAsync("hasAuth");  
        return !(hasAuth == null);
    }
    protected override bool OnBackButtonPressed()
    {
        Application.Current.Quit();
        return true;
    }
}