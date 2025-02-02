using MusicApp.Services;

namespace MusicApp.Pages;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}
    private async void BtnSignIn_Clicked(object sender, EventArgs e)
    {
        var response = await ApiService.LoginUser(EntEmail.Text, EntPassword.Text);

        if (response)
        {
            await SecureStorage.SetAsync("hasAuth", "true");

            Preferences.Set("userid", string.Empty);

            await DisplayAlert("", "Your are logged in", "OK");
            Application.Current.MainPage = new AppShell();
    }
        else
        {
        await DisplayAlert("Login failed", "Uusername or password if invalid", "Try again");
    }



}
    private async void OnSignUpTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SignUpPage());
    }
}