using MusicStoreData.Models.Abstract;
using System.ComponentModel.DataAnnotations;

namespace MusicStoreData.Models.CMS
{
    public class SocialMedia : BaseDataTable
    {
        [Display(Name = "SocialMedia Image URL")]
        public string? SocialMediaImageUrl { get; set; }
        [Display(Name = "SocialMedia More Info")]
        public string? SocialMoreInfo { get; set; }

    }
}
