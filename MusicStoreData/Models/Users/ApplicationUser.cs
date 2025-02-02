
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicStoreData.Models.Users
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "Name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Surname is required")]
        public string? LastName { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string? StreetAddress { get; set; }
        [Required(ErrorMessage = "City is required")]
        public string? City { get; set; }
        [Required(ErrorMessage = "State is required")]
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        [Required(ErrorMessage = "Country is required")]
        public string? Country { get; set; }
        public string? ProfilePicture { get; set; }
        public int? CompanyId{ get; set; }
        public bool isActive { get; set; } = true;

        [ForeignKey("CompanyId")]
        [ValidateNever]
        public Company? Company{ get; set; }
        [NotMapped]
        public string Role { get; set; }
        public ICollection<Reviews> Reviews { get; set; }


    }
}