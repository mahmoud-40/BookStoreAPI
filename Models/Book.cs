using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreAPI.Models
{
    public class Book
    {
        public int Id { get; set; }
        [StringLength(150)]
        public string? Title { get; set; }
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        [Column(TypeName = "date")]
        public DateOnly PublishedDate { get; set; }
        public int Stock { get; set; }

        [ForeignKey("Author")]
        public int? AuthorId { get; set; }
        public virtual Author? Author { get; set; }

        [ForeignKey("Catalog")]
        public int? CatalogId { get; set; }
        public virtual Catalog? Catalog { get; set; }

        public virtual List<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();
    }
}
