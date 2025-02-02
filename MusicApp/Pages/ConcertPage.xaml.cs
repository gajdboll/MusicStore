
using MusicApp.Models;
using MusicApp.Services;
 

namespace MusicApp.Pages;

public partial class ConcertPage : ContentPage
{
    private int _concertId;
    public ConcertPage(int id)
    {
        InitializeComponent();
        _concertId = id;
        GetConcerts(id);
     }
    private async void GetConcerts(int id)
    {
        // 2 that endpoint is run when first category like Electric etc is selected - List of Categories returned from db
        var selection = await ApiService.GetConcerts(id);
        CvConcerts.ItemsSource = new List<Concert> { selection };
    }
    private void OnEditClicked(object sender, EventArgs e)
    {
         if (sender is Button button && button.BindingContext is Concert concert)
        {
             Navigation.PushAsync(new EditConcert(concert.Id));
        }
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Confirm Delete",
                                         "Are you sure you want to delete this concert?",
                                         "Yes", "No");

        if (answer)
        {
            try
            {
                 bool success = await ApiService.DeleteConcert(_concertId);

                if (success)
                {
                    await DisplayAlert("Success", "Concert has been deleted.", "OK");

                    Navigation.PushAsync(new HomePage());
                }
            }
            catch (Exception ex)
            {
                 await DisplayAlert("Error", "Failed to delete the concert: " + ex.Message, "OK");
            }
        }
        else
        {
             await DisplayAlert("Cancelled", "Concert deletion was cancelled.", "OK");
        }
    }



}