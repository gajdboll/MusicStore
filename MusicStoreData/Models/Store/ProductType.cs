using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MusicStoreData.Models.Abstract;
using MusicStoreData.Models.CMS;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using MusicStoreData.Models.ShoppingCart;

namespace MusicStoreData.Models.Store
{
    public class ProductType : BaseDataTable
    {
        [Display(Name = "Price")]
        [Required(ErrorMessage = "Enter Price")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a non-negative number.")]

        public decimal Price { get; set; }
        public decimal SupplierPrice { get; set; }

        // status - is the item new => 0 /used => 1
        [Display(Name = "Product Status")]
        // used or new
        public bool ProductStatus { get; set; }
        // is product on sale
        public bool OnSale { get; set; }

        // connection with manufacturer
        public int? ManufacturerId { get; set; }

        [ForeignKey("ManufacturerId")]
        public Manufacturer? Manufacturer { get; set; }

        public int? ProductSubCategoryId { get; set; }
        [ForeignKey("ProductSubCategoryId")]
        public ProductSubCategory? ProductSubCategory { get; set; }
         public string? MoreInfo { get; set; }
         public ICollection<Reviews> Reviews { get; set; }
        public ICollection<Product>? Products { get; set; }
        [ValidateNever]
        public ICollection<ProductImage> ProductImages { get; set; }
        public ICollection<WishlistItem> WishlistItems { get; set; }

        // Navigation property for TechInfo
        public TechInfo TechInfo { get; set; }
        public FAQ FAQ { get; set; }
     }
}
