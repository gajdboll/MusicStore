 using MusicStoreData.Models.Abstract;
using MusicStoreData.Models.Store;

public class FAQ : BaseDataTable
{
    public int ProductTypeId { get; set; }
    public ProductType ProductType { get; set; }
    public string Email {  get; set; }
    public string Subject {  get; set; }
    public string Answer{  get; set; }

}
