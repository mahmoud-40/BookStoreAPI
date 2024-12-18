namespace BookStoreAPI.DTOs.AuthorDTO
{
    public class AddBookToAuthor
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public DateOnly PublishedDate { get; set; }
        public int Stock { get; set; }
        public int CatalogId { get; set; }
    }
}
