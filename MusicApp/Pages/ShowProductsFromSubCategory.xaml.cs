using MusicApp.Services;

namespace MusicApp.Pages;

public partial class ShowProductsFromSubCategory : ContentPage
{
    public ShowProductsFromSubCategory(int subCategoryId)
    {
        InitializeComponent();
        GetProductTypes(subCategoryId);
    }

    private async void GetProductTypes(int subCategoryId)
    {
        //4 that endpoint return single product type selected by specific id type from the list
        var productTypes = await ApiService.GetShowProductsFromSubCategory(subCategoryId);
        if (productTypes == null )
        {
            NoProductsLabel.IsVisible = true;   
            CvProductTypes.IsVisible = false;  
        }
        else
        {
            NoProductsLabel.IsVisible = false;  
            CvProductTypes.IsVisible = true;    
            CvProductTypes.ItemsSource = productTypes.Products; 
        }
    }
     
    private void CvProductTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var currentSelection = e.CurrentSelection.FirstOrDefault() as ProductTypeList; 
        if (currentSelection == null) return;


        Navigation.PushAsync(new ShowProduct(currentSelection.Id));

         ((CollectionView)sender).SelectedItem = null;
    }

}
 