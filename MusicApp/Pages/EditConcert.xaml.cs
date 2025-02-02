using MusicApp.Models;
using MusicApp.Services;
using System;
using System.IO;

namespace MusicApp.Pages;

public partial class EditConcert : ContentPage
{
    private Concert _concert;
    private Stream _selectedImageStream; // Stream for the selected image file
    private string _selectedImageFileName; // File name of the selected image
    private string _selectedImageContentType; // MIME type of the selected image

    public EditConcert(int id)
    {
        InitializeComponent();
        LoadConcertData(id); // Call the method to load concert data
    }

    private async void LoadConcertData(int id)
    {
        // Retrieve concert data from the API
        _concert = await ApiService.GetConcerts(id);

        // Assign concert data to the form controls
        EntName.Text = _concert.Name;
        EntMusicType.Text = _concert.MusicType;
        EntCity.Text = _concert.City;
        EntLocation.Text = _concert.Location;
        EntOpeningTime.Date = _concert.OpeningTime;

        // If the concert has an assigned image, display it
        if (!string.IsNullOrEmpty(_concert.Image))
        {
            ConcertImage.Source = _concert.Image; // URL of the concert image
        }
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        // Update the concert object with data from the form
        _concert.Name = EntName.Text;
        _concert.City = EntCity.Text;
        _concert.Location = EntLocation.Text;
        _concert.MusicType = EntMusicType.Text;
        _concert.OpeningTime = EntOpeningTime.Date;

        // Check if an image was selected, and set the Image property with its path or URL
        if (!string.IsNullOrEmpty(_selectedImageFileName))
        {
            _concert.Image = _selectedImageFileName;  
        }

        // Send the updated concert data to the server
        await ApiService.UpdateConcert(_concert);  

        // Navigate back after saving
        Navigation.PushAsync(new ConcertPage(_concert.Id));
    }

    private async void OnChooseImageClicked(object sender, EventArgs e)
    {
        try
        {
            // Open file picker to select an image
            var result = await FilePicker.PickAsync(new PickOptions
            {
                FileTypes = FilePickerFileType.Images,
                PickerTitle = "Choose a concert image"
            });

            if (result != null)
            {
                // Set selected file details (we're only storing the filename here, not uploading the file)
                _selectedImageFileName = result.FileName;

                // You can also optionally save the path to the file if needed
                 _selectedImageFileName = result.FullPath;  

                // Display the selected image
                using (var stream = await result.OpenReadAsync())
                {
                    ConcertImage.Source = ImageSource.FromStream(() => stream);
                }
            }
        }
        catch (Exception ex)
        {
            // Error handling (in case something goes wrong during image selection)
            await DisplayAlert("Error", "Failed to choose image: " + ex.Message, "OK");
        }
    }

}
 