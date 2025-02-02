using MusicStoreData.Models.Abstract;
 
public class DiscountCode : BaseDataTable
{
    public decimal DiscountPercent { get; set; } // Discount percentage
    public DateTime? ExpirationDate { get; set; } // Optional expiration date

}
