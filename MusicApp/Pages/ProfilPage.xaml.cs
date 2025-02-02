namespace MusicApp.Pages;

public partial class ProfilPage : ContentPage
{
	public ProfilPage()
	{
		InitializeComponent();
        LblUserName.Text = Preferences.Get("username", string.Empty);

    }
    // logout
    private void Button_Clicked(object sender, EventArgs e)
    {
        SecureStorage.RemoveAll();
        Application.Current.MainPage = new NavigationPage(new LoginPage());
    }
    private async void OnAdminPoliciesTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AdminPoliciesPage());
    }
    private void TapOrders_Tapped(object sender, TappedEventArgs e)
    {
        Navigation.PushAsync(new OrderPage());
    }
    private void MyInformationTapped(object sender, TappedEventArgs e)
    {
        Navigation.PushAsync(new StoreInfoPage());
    }
}