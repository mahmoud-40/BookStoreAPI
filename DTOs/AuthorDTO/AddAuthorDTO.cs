using BookStoreAPI.Models;

namespace BookStoreAPI.DTOs.AuthorDTO
{
    public class AddAuthorDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public List<AddBookToAuthor> Books { get; set; } = new List<AddBookToAuthor>(); 

    }
}
