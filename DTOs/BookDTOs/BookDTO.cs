namespace BookStoreAPI.DTOs.BookDTOs
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string? Catalog { get; set; }
    }
}
