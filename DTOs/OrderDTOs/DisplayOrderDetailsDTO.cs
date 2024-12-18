namespace BookStoreAPI.DTOs.OrderDTOs
{
    public class DisplayOrderDetailsDTO
    {
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public int BookId { get; set; }
    }
}
