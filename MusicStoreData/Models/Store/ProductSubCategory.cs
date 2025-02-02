using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MusicStoreData.Models.Abstract;

namespace MusicStoreData.Models.Store
{
    public class ProductSubCategory : BaseDataTable
    {
        [Display(Name = "ProductCategoryId")]
        public int? ProductCategoryId { get; set; }
        [ForeignKey("ProductCategoryId")]
        public ProductCategory? ProductCategory { get; set; }
        public List<ProductType>? Products { get; set; }
        [Display(Name = "SubCategory Image Url")]

        public string? SubCategoryImageUrl { get; set; }
        [Display(Name = "SubCategory More Info")]

        public string? SubCategoryMoreInfo { get; set; }
    }
}
