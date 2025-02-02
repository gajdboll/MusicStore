using MusicStoreData.Models.Abstract;
using MusicStoreData.Models.Store;
using MusicStoreData.Models.Users;
using System.ComponentModel.DataAnnotations;

public class Reviews : BaseDataTable
{
    public int ProductTypeId { get; set; }
    public ProductType ProductType { get; set; }
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }

    [Range(1, 5)]
    public int Rating { get; set; }

    [MaxLength(1000)]
    public string Comment { get; set; }


}
