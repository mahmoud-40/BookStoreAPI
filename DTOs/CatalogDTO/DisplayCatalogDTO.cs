using BookStoreAPI.DTOs.BookDTOs;

namespace BookStoreAPI.DTOs.CatalogDTO
{
    public class DisplayCatalogDTO
    {
        public string Title { get; set; }
        public List<DisplayBookCatalogDTO> Books { get; set; } = new List<DisplayBookCatalogDTO>();
    }
}
