using MusicApp.Models;
using MusicApp.Services;

namespace MusicApp.Pages
{
    public partial class ShowProductSubCategories : ContentPage
    {
        public ShowProductSubCategories(int categoryId)
        {
            InitializeComponent();
            GetSubcategories(categoryId);
        }

        private async void GetSubcategories(int categoryId)
        {
            // 3 that endpoint is run when first category is selected from list of categories - return list of subcategories
            var subcategories = await ApiService.GetShowProductSubCategories(categoryId);

            if (subcategories != null && subcategories.Any())
            {
                CvSubcategories.ItemsSource = subcategories;
            }
            else
            {
                await DisplayAlert("Lack of category", "No sub-category for that category.", "OK");
                Navigation.PushAsync(new HomePage());
            }
        }
        private void CvSubcategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var currentSelection = e.CurrentSelection.FirstOrDefault() as SubCategory;
            if (currentSelection == null) return;

            Navigation.PushAsync(new ShowProductsFromSubCategory(currentSelection.Id));

             ((CollectionView)sender).SelectedItem = null;
        }

    }
}
