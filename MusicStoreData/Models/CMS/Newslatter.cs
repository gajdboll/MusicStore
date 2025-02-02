using MusicStoreData.Models.Abstract;
using System.ComponentModel.DataAnnotations;

namespace MusicStoreData.Models.CMS
{
    public class Newslatter : BaseDataTable
    {
        [Display(Name = "Position")]
        [Required(ErrorMessage = "Enter position")]
        [Range(0, int.MaxValue, ErrorMessage = "Position must be a non-negative number.")]
        public int Position { get; set; }
        [Display(Name = "Newslatter Button")]
        [Required(ErrorMessage = "Enter Button name")]
        public string NewslatterButton { get; set; }
        [Display(Name = "Background Image")]
        [Required(ErrorMessage = "Enter Background Image")]
        public string BackGroundImage { get; set; }
        [Display(Name = "Placeholder")]
        [Required(ErrorMessage = "Enter Placeholder")]
        public string Placeholder { get; set; }
    }
}
