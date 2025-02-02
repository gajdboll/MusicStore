 using MusicStoreData.Models.Abstract;
using MusicStoreData.Models.CMS;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 
namespace MusicStoreData.Models.Store
{
    public class Product : BaseDataTable
    {
        [Required(ErrorMessage = "Quantity is required")]
        [Display(Name = "Quantity")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a non-negative number.")]
        public int Quantity { get; set; }
        [ForeignKey("ProductType")]

        public int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }
        [ForeignKey("Color")]
        public int ColorId { get; set; }
        public Color Color { get; set; }

    }
}
