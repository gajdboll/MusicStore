using MusicStoreData.Models.Abstract;
using System.ComponentModel.DataAnnotations;

namespace MusicStoreData.Models.CMS
{
    //That model holds all the links to the navigation section / more section
    public class More : BaseDataTable
    {
        [Display(Name = "Icon More Section")]
        public string? MoreIcon { get; set; }
    }
}
