using System.ComponentModel.DataAnnotations;


namespace MusicStoreData.Models.CMS
{
    public class BookmarkedProduct
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string Name { get; set; }
        public string ApplicationUserId { get; set; }

        public string Detail { get; set; }

        public int Price { get; set; }

        public string ImageUrl { get; set; }

        public bool IsBookmarked { get; set; }
    }
}
