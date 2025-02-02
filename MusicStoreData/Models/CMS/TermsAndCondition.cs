using MusicStoreData.Models.Abstract;
using System.ComponentModel.DataAnnotations;

namespace MusicStoreData.Models.CMS
{
    public class TermsAndCondition : BaseDataTable
    {
        [Display(Name = "TermsAndCondition More Info")]
        public string? TermsAndConditionMoreInfo { get; set; }
        [Display(Name = "Terms Photo URL")]
        public string? TermsPhotoUrl { get; set; }
    }
}
