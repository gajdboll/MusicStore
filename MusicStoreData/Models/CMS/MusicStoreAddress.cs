using MusicStoreData.Models.Abstract;
using System.ComponentModel.DataAnnotations;

namespace MusicStoreData.Models.CMS
{
    public class MusicStoreAddress : BaseDataTable
    {
         [Display(Name = "Address")]
        public string? Address { get; set; }

         [Display(Name = "Address prt 2")]
        public string? Address2 { get; set; }

        [MaxLength(50, ErrorMessage = "Phone numer may have a maximum of 50 characters")]
        [Display(Name = "Phone numer")]
        public string? PhoneNumber { get; set; }

        [MaxLength(90, ErrorMessage = "Email adres may have a maximum of 50 characters")]
        [Display(Name = "Email address")]
        public string? EmailAdres { get; set; }
         [Display(Name = "Is That Current Store")]
        public bool  IsItCurrentStore { get; set; }
        public string? PostCode { get; set; }
        public string? Country { get; set; }
        // Navigation property
        public ICollection<OpeningHours> OpeningHours { get; set; }
    }
}
