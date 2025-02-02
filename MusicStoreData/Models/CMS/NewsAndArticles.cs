using MusicStoreData.Models.Abstract;
using System.ComponentModel.DataAnnotations;

namespace MusicStoreData.Models.CMS
{
    public class NewsAndArticles : BaseDataTable
    {
        [Required(ErrorMessage = "Enter THE Author")]

        [Display(Name = "Author")]
        public string Author { get; set; }
        [Display(Name = "Button Name")]
        public string? BtnName { get; set; }
        [Display(Name = "News Info")]
        public string? NewsInfo { get; set; }
 
        [Display(Name = "Blog Image")]
        public string? BlogImage { get; set; }
    }
}
