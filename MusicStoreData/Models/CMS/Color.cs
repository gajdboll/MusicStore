using MusicStoreData.Models.Abstract;
using MusicStoreData.Models.Store;

namespace MusicStoreData.Models.CMS
{
    public class Color : BaseDataTable
    {
        public ICollection<Product>? ProductColors { get; set; }

    }
}
