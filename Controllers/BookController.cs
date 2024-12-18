using BookStoreAPI.DTOs.BookDTOs;
using BookStoreAPI.Models;
using BookStoreAPI.UnitOfWorks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin,customer")]
    [SwaggerTag("Book Management")]
    public class BookController : ControllerBase
    {
        UnitOfWork _unit;

        public BookController(UnitOfWork unit)
        {
            _unit = unit;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all books")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", Type = typeof(List<BookDTO>))]
        public IActionResult GetAll()
        {
            List<Book> books = _unit.BookRepo.GetAll();

            List<BookDTO> bookDTOs = new List<BookDTO>();

            foreach (var book in books)
            {
                BookDTO bookDTO = new BookDTO()
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author?.Name,
                    Price = book.Price,
                    Stock = book.Stock,
                    Catalog = book.Catalog?.Name
                };

                bookDTOs.Add(bookDTO);
            }

            return Ok(bookDTOs);
        }


        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get a book by id")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", Type = typeof(BookDTO))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Book not found")]
        public IActionResult GetById(int id)
        {
            Book book = _unit.BookRepo.GetById(id);

            if (book == null)
            {
                return NotFound();
            }

            BookDTO bookDTO = new BookDTO()
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author.Name,
                Price = book.Price,
                Stock = book.Stock,
                Catalog = book.Catalog.Name
            };

            return Ok(bookDTO);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [SwaggerOperation(Summary = "Create a book")]
        [SwaggerResponse(StatusCodes.Status201Created, "Book created")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid data")]
        public IActionResult Create(AddBookDTO bookDTO)
        {
            if (ModelState.IsValid)
            {
                Book book = new Book()
                {
                    Title = bookDTO.Title,
                    Price = bookDTO.Price,
                    PublishedDate = bookDTO.PublishedDate,
                    Stock = bookDTO.Stock,
                    AuthorId = bookDTO.AuthorId,
                    CatalogId = bookDTO.CatalogId
                };

                _unit.BookRepo.Add(book);
                _unit.Save();

                return StatusCode(StatusCodes.Status201Created);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        [SwaggerOperation(Summary = "Update a book")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Book updated")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid data")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Book not found")]
        public IActionResult Update(EditBookDTO bookDTO)
        {
            if (ModelState.IsValid)
            {
                Book book = _unit.BookRepo.GetById(bookDTO.Id);

                if (book == null)
                {
                    return NotFound();
                }

                book.Title = bookDTO.Title;
                book.Price = bookDTO.Price;
                book.PublishedDate = bookDTO.PublishedDate;
                book.Stock = bookDTO.Stock;
                book.AuthorId = bookDTO.AuthorId;
                book.CatalogId = bookDTO.CatalogId;

                _unit.BookRepo.Update(book);
                _unit.Save();

                return NoContent();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        [SwaggerOperation(Summary = "Delete a book")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Book deleted")]
        public IActionResult Delete(int id)
        {
            _unit.BookRepo.Delete(id);
            _unit.Save();

            return NoContent();
        }
    }
}
