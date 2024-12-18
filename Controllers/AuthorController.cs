using BookStoreAPI.DTOs.AuthorDTO;
using BookStoreAPI.DTOs.BookDTOs;
using BookStoreAPI.Models;
using BookStoreAPI.UnitOfWorks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Author Management")]
    public class AuthorController : ControllerBase
    {
        UnitOfWork _unit;

        public AuthorController(UnitOfWork unit)
        {
            _unit = unit;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all authors")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", Type = typeof(List<DisplayAuthorsDTO>))]
        public IActionResult GetAll()
        {
            List<Author> authors = _unit.AuthorRepo.GetAll();

            List<DisplayAuthorsDTO> authorDTOs = new List<DisplayAuthorsDTO>();

            foreach (var author in authors)
            {
                DisplayAuthorsDTO authorDTO = new DisplayAuthorsDTO()
                {
                    Id = author.Id,
                    Name = author.Name,
                    Description = author.Description,
                    Books = author.Books.ConvertAll(x => new DisplayBooksDTO()
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Price = x.Price,
                        Stock = x.Stock,
                        Catalog = x.Catalog.Name
                    })
                };
                authorDTOs.Add(authorDTO);
            }

            return Ok(authorDTOs);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get an author by id")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", Type = typeof(DisplayAuthorsDTO))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Not Found")]
        public IActionResult GetById(int id)
        {
            Author author = _unit.AuthorRepo.GetById(id);

            if (author == null)
            {
                return NotFound();
            }

            DisplayAuthorsDTO authorDTO = new DisplayAuthorsDTO()
            {
                Id = author.Id,
                Name = author.Name,
                Description = author.Description,
                Books = author.Books.ConvertAll(x => new DisplayBooksDTO()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Price = x.Price,
                    Stock = x.Stock,
                    Catalog = x.Catalog.Name
                })
            };

            return Ok(authorDTO);
        }

        [HttpPost]
        [Authorize("admin")]
        [SwaggerOperation(Summary = "Add an author")]
        [SwaggerResponse(StatusCodes.Status200OK, "Author added successfully")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Failed")]
        public IActionResult Add(AddAuthorDTO _authorDto)
        {
            if (ModelState.IsValid)
            {
                Author author = new Author()
                {
                    Name = _authorDto.Name,
                    Description = _authorDto.Description,
                    Books = _authorDto.Books.ConvertAll(x => new Book()
                    {
                        Title = x.Title,
                        Price = x.Price,
                        Stock = x.Stock,
                        CatalogId = x.CatalogId
                    })
                };

                _unit.AuthorRepo.Add(author);

                _unit.Save();

                return Ok(new { message = "Author added successfully" });
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut("{id}")]
        [Authorize("admin")]
        [SwaggerOperation(Summary = "Update an author")]
        [SwaggerResponse(StatusCodes.Status200OK, "Author updated successfully")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Data is invalid or missing")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Not Found")]
        public IActionResult Update(int id, UpdateAuthorDTO _authorDto)
        {
            if (ModelState.IsValid)
            {
                Author author = _unit.AuthorRepo.GetById(id);

                if (author == null)
                {
                    return NotFound();
                }

                author.Name = _authorDto.Name;
                author.Description = _authorDto.Description;

                _unit.AuthorRepo.Update(author);

                _unit.Save();

                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


        [HttpDelete("{id}")]
        [Authorize("admin")]
        [SwaggerOperation(Summary = "Delete an author")]
        [SwaggerResponse(StatusCodes.Status200OK, "Author deleted successfully")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The author was not found")]
        public IActionResult Delete(int id)
        {
            Author author = _unit.AuthorRepo.GetById(id);

            if (author == null)
            {
                return NotFound();
            }

            _unit.AuthorRepo.Delete(author.Id);

            _unit.Save();

            return Ok();
        }

    }
}
