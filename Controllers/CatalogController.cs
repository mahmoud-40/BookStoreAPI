using BookStoreAPI.DTOs.BookDTOs;
using BookStoreAPI.DTOs.CatalogDTO;
using BookStoreAPI.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Catalog Management")]
    public class CatalogController : ControllerBase
    {
        private readonly UnitOfWork _unit;

        public CatalogController(UnitOfWork unit)
        {
            _unit = unit;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all catalogs")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", typeof(List<DisplayCatalogDTO>))]
        public IActionResult GetAll()
        {
            var catalogs = _unit.CatalogRepo.GetAll();

            var displayCatalogs = new List<DisplayCatalogDTO>();

            foreach (var catalog in catalogs)
            {
                var displayCatalog = new DisplayCatalogDTO()
                {
                    Title = catalog.Name
                };

                foreach (var book in catalog.Books)
                {
                    displayCatalog.Books.Add(new DisplayBookCatalogDTO()
                    {
                        Title = book.Title,
                        Author = book.Author.Name,
                        Price = book.Price,
                        Stock = book.Stock,
                    });
                }

                displayCatalogs.Add(displayCatalog);
            }

            return Ok(displayCatalogs);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get a catalog by id")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", typeof(DisplayCatalogDTO))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Not Found")]
        public IActionResult GetById(int id)
        {
            var catalog = _unit.CatalogRepo.GetById(id);

            if (catalog == null)
            {
                return NotFound();
            }

            var displayCatalog = new DisplayCatalogDTO()
            {
                Title = catalog.Name
            };

            foreach (var book in catalog.Books)
            {
                displayCatalog.Books.Add(new DisplayBookCatalogDTO()
                {
                    Title = book.Title,
                    Author = book.Author.Name,
                    Price = book.Price,
                    Stock = book.Stock,
                });
            }

            return Ok(displayCatalog);
        }

        [HttpPost("{catalogId}/books/{bookId}")]
        [SwaggerOperation(Summary = "Add a book to a catalog")]
        [SwaggerResponse(StatusCodes.Status200OK, "Book added to catalog")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Catalog or book not found")]
        public IActionResult AddBookToCatalog(int catalogId, int bookId)
        {
            var catalog = _unit.CatalogRepo.GetById(catalogId);
            var book = _unit.BookRepo.GetById(bookId);

            if (catalog == null || book == null)
            {
                return NotFound();
            }

            catalog.Books.Add(book);
            _unit.CatalogRepo.Update(catalog);
            _unit.Save();

            return Ok();
        }
    }
}
