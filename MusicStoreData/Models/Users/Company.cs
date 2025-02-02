using MusicStoreData.Models.Abstract;

namespace MusicStoreData.Models.Users
{
    public class Company : BaseDataTable
    {
        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public string? Country{ get; set; }
        public string? CompanyPicUrl{ get; set; }
        public string? CompanyWeb{ get; set; }
        public string? CompanyTel{ get; set; }
        public string? NIP{ get; set; }
         
    
    }
}
