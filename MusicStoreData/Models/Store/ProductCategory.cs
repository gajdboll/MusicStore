using MusicStoreData.Models.Abstract;
using MusicStoreData.Models.CMS;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicStoreData.Models.Store
{
    public class ProductCategory : BaseDataTable
    {
        public List<ProductSubCategory>? ProductSubCategories { get; set; }

        public int? WebHeaderId { get; set; }
        [ForeignKey("WebHeaderId")]
        public WebHeaders? WebHeader { get; set; }
        [Display(Name = "Category Url")]

        public string? CategoryImageUrl { get; set; }
        [Display(Name = "Category More Info")]

        public string? CategoryMoreInfo { get; set; }

    }
}
