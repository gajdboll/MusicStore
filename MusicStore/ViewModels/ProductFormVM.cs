using MusicStoreData.Models.Store;

public class ProductFormVM
{
    public ProductType Product { get; set; }
    public IEnumerable<ProductType> RelatedProducts { get; set; }
    public int ServiceId { get; set; }
    public int ProductTypeId { get; set; }
    public int NumberOfProducts { get; set; }
}
