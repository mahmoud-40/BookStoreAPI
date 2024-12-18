using BookStoreAPI.Models;

namespace BookStoreAPI.DTOs.OrderDTOs
{
    public class AddOrderDTO
    {
        public string CustomerId { get; set; }
        public Status Status { get; set; }

        public List<AddOrderDetailsDTO> Books { get; set; } = new List<AddOrderDetailsDTO>();
    }
}
