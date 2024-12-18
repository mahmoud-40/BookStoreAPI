namespace BookStoreAPI.DTOs.BookDTOs
{
    public class EditBookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public DateOnly PublishedDate { get; set; }
        public int Stock { get; set; }
        public int AuthorId { get; set; }
        public int CatalogId { get; set; }
    }
}
