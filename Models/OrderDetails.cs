using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreAPI.Models
{
    public class OrderDetails
    {
        [ForeignKey("Book")]
      public int BookId { get; set; }
      [ForeignKey("Order")]
      public int OrderId { get; set; }
      public int Quantity { get; set; }
      [Column(TypeName = "money")]
      public decimal UnitPrice { get; set; }
      public virtual Book Book { get; set; }
      public virtual Order Order { get; set; }
    }
}
