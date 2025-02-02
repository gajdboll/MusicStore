 

namespace MusicStoreData.Models.CMS
{
    public class OpeningHours
    {
        public int Id { get; set; }
        public string DayOfWeek { get; set; } 
        public string Openhours { get; set; }
        // Foreign key to the Store
        public int MusicStoreAddressId { get; set; }
        public MusicStoreAddress Store { get; set; }
    }
}
