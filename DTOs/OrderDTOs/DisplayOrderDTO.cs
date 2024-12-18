using BookStoreAPI.Models;

namespace BookStoreAPI.DTOs.OrderDTOs
{
    public class DisplayOrderDTO
    {
        public int Id { get; set; }
        public string? CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public Status Status { get; set; }
        public decimal TotalPrice { get; set; }
        public List<DisplayOrderDetailsDTO> OrderDetails { get; set; } = new List<DisplayOrderDetailsDTO>();

    }
}
