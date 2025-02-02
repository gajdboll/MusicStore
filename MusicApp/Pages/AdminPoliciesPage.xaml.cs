using MusicApp.Models;
using MusicApp.Services;

namespace MusicApp.Pages;

public partial class AdminPoliciesPage : ContentPage
{
    public AdminPoliciesPage()
    {
        InitializeComponent();
        GetPolicies();
    }

    private async void GetPolicies()
    {
        var policies = await ApiService.GetAdminPoliciesAsync();
        CvPolicies.ItemsSource = policies; 
    }
}