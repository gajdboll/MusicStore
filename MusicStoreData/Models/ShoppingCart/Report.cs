 

namespace MusicStoreData.Models.ShoppingCart
{
    public class Report
    {
        public int Id { get; set; }
        public DateTime IssueDate { get; set; }
        public decimal? TotalElectricGuitars { get; set; }
        public decimal? TotalAcusticGuitars { get; set; }
        public decimal? TotalClasicalGuitars { get; set; }
        public decimal? TotalOtherGuitars { get; set; }
        public decimal? TotalBassGuitars { get; set; }
        public decimal? TotalAmplifiers { get; set; }
        public decimal? TotalStrings { get; set; }
    }
}
