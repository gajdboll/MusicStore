using MusicApp.Services;

namespace MusicApp.Pages;

public partial class SignUpPage : ContentPage
{
	public SignUpPage()
	{
		InitializeComponent();
	}
    private async void BtnSignup_Clicked(object sender, EventArgs e)
    {
        var response = await ApiService.RegisterUser(EntName.Text, EntLastName.Text, EntEmail.Text, EntPhone.Text, EntPassword.Text,
            EntStreet.Text, EntPostal.Text, EntCity.Text, EntState.Text, EntCountry.Text);

        if (response)
        {
             await DisplayAlert("", "Your account has been created", "OK");
            await Navigation.PushAsync(new LoginPage());
        }
        else
        {
            await DisplayAlert("", "Oops something went wrong", "Cancel");
        }
    }

    private async void OnSignInTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LoginPage());
    }
}