using BookStoreAPI.Models;

namespace BookStoreAPI.DTOs.AuthorDTO
{
    public class UpdateAuthorDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }

        public List<Book> Books { get; set; } = new List<Book>();
    }
}
