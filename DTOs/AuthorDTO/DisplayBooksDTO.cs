namespace BookStoreAPI.DTOs.AuthorDTO
{
    public class DisplayBooksDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string? Catalog { get; set; }
    }
}
