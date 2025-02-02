using MusicStoreData.Models.Abstract;
using MusicStoreData.Models.Store;
using System.ComponentModel.DataAnnotations;

namespace MusicStoreData.Models.CMS
{
    public class Manufacturer : BaseDataTable
    {

        [Display(Name = "LogoUrl")]
        public string? LogoUrl { get; set; }

        [Display(Name = "Country of Origin")]
        public string? CountryOfOrigin { get; set; }

        [Display(Name = "Contact Number")]
        public string? ContactNumber { get; set; }

        [Display(Name = "Manufacturer email")]
        public string? ManufacturerEmail { get; set; }
        [Display(Name = "Manufacturer web")]
        public string? ManufacturerWeb { get; set; }
        [Display(Name = "Manufacturer Address")]
        public string? ManufactureAddress { get; set; }

        public List<ProductType>? ProductTypes { get; set; }
    }
}
