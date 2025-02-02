using MusicStoreData.Models.Abstract;

namespace MusicStoreData.Models.CMS
{
    public class CustomerQuery : BaseDataTable
    {
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public bool IsAnswered{ get; set; }
    }
}
