using MusicStoreData.Models.Abstract;
using System.ComponentModel.DataAnnotations;
using MusicStoreData.Models.Store;
 
namespace MusicStoreData.Models.CMS
{
    public class WebHeaders : BaseDataTable
    {

        [Display(Name = "Image Url")]
        public string? ImageUrl { get; set; }
        // image url per section - that will navigate you to the sevtion
        public List<ProductCategory>? ProductCategories { get; set; }
    }
}
