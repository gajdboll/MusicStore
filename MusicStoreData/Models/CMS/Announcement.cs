using MusicStoreData.Models.Abstract;
 
namespace MusicStoreData.Models.CMS
{
    public class Announcement : BaseDataTable
    {
        public string PictureUrl { get; set; }
        public string MoreInfo { get; set; }
        public string? FullNameContact { get; set; }
        public string? PhoneContact { get; set; }
        public string? EmailContact { get; set; }

    }
}
