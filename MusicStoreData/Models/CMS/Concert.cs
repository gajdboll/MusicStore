using MusicStoreData.Models.Abstract;
 
namespace MusicStoreData.Models.CMS
{
    public class Concert : BaseDataTable
    {
        public string? City{ get; set; }
        public string? Location{ get; set; }
        public string? MusicType{ get; set; }
        public DateTime? OpeningTime{ get; set; }
        public string? Image{ get; set; }

    }
}
