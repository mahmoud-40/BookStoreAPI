using BookStoreAPI.DTOs.BookDTOs;

namespace BookStoreAPI.DTOs.AuthorDTO
{
    public class DisplayAuthorsDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<DisplayBooksDTO> Books { get; set; } = new List<DisplayBooksDTO>();
    }
}
