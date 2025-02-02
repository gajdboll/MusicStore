using System.ComponentModel.DataAnnotations;

namespace MusicStoreData.Models.Abstract
{
    public class BaseDataTable
    {
        [Key]
        public int Id { get; set; }
        public bool isActive { get; set; } = true;
        [Required(ErrorMessage = "Enter Name!")]
        public string Name { get; set; } = null!;
        [Display(Name = "Position")]
        [Required(ErrorMessage = "Enter position")]
        [Range(0, int.MaxValue, ErrorMessage = "Position must be a non-negative number.")]
        public int Position { get; set; }

        public string? Descriptions { get; set; } = null!;

        //public bool isOnPromotion { get; set; } = false;
        public DateTime? DateAdded { get; set; }
        public DateTime? ModificationDate { get; set; }
        public DateTime? DateOfDelete { get; set; }
 
    }
}