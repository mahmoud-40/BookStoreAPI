using System.ComponentModel.DataAnnotations.Schema;
using BookStoreAPI.DTOs.OrderDTOs;

namespace BookStoreAPI.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Column(TypeName = "Date")]
        public DateTime OrderDate { get; set; }
        [Column(TypeName = "money")]
        public double TotalPrice { get; set; }
        public Status Status { get; set; }
        [ForeignKey("customer")]
        public string CustomerId { get; set; }
        public virtual Customer customer { get; set; }

        public virtual List<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();
    }

    public enum Status
    {
        Pending,
        Processing,
        Delivered,
        Cancelled
    }
}
