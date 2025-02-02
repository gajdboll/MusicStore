using Microsoft.AspNetCore.Mvc.Rendering;
using MusicStoreData.Models.Store;
 
namespace MusicStore.ViewModels
{
    public class ProductTypeVM
    {
        // The main product type being displayed or edited
        public ProductType ProductType { get; set; }

        // List for selecting a Manufacturer in a dropdown
        public IEnumerable<SelectListItem> ManufacturerList { get; set; }
        public string ManufacturerName { get; set; } // Additional manufacturer details

        // List for selecting a Product SubCategory in a dropdown
        public IEnumerable<SelectListItem> ProductSubCategoryList { get; set; }

        // List for selecting a Product Category in a dropdown (if needed)
        public IEnumerable<SelectListItem> ProductCategoryList { get; set; }

        // Additional properties can be added here as needed, for example:
        public TechInfo TechInfo { get; set; }

        // A property to determine if the product is new or existing
        public bool IsNewProduct { get; set; }
    }
}
