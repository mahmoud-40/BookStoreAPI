using BookStoreAPI.DTOs.OrderDTOs;
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
    [SwaggerTag("Order Management")]
    public class OrderController : ControllerBase
    {
        private readonly UnitOfWork _unit;

        public OrderController(UnitOfWork unit)
        {
            _unit = unit;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Add a new order")]
        [SwaggerResponse(StatusCodes.Status200OK, "Order added successfully")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid data")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Insufficient stock")]
        public IActionResult Add(AddOrderDTO _order)
        {
            if (ModelState.IsValid)
            {
                Order order = new Order()
                {
                    CustomerId = _order.CustomerId,
                    OrderDate = DateTime.Now.Date,
                    Status = _order.Status,
                };

                _unit.OrderRepo.Add(order);
                _unit.Save();

                decimal totalPrice = 0;

                foreach (var item in _order.Books)
                {
                    Book b = _unit.BookRepo.GetById(item.BookId);

                    totalPrice += b.Price * item.Quantity;

                    OrderDetails details = new OrderDetails()
                    {
                        OrderId = order.Id,
                        BookId = item.BookId,
                        Quantity = item.Quantity,
                        UnitPrice = b.Price,
                    };

                    if (b.Stock >= details.Quantity)
                    {
                        order.OrderDetails.Add(details);

                        b.Stock -= item.Quantity;
                        _unit.BookRepo.Update(b);
                    }
                    else
                    {
                        return BadRequest(new { message = "Insufficient stock" });
                    }
                }

                order.TotalPrice = (double)totalPrice;

                _unit.Save();

                return Ok();
            }
            else
            {
                return BadRequest(new { message = "Invalid data" });
            }
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all orders")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", Type = typeof(DisplayOrderDTO))]
        public IActionResult GetAll()
        {
            var orders = _unit.OrderRepo.GetAll();

            var ordersDTO = orders.Select(x => new DisplayOrderDTO()
            {
                Id = x.Id,
                CustomerName = x.customer?.FullName ?? "Unknown Customer",
                OrderDate = x.OrderDate,
                Status = x.Status,
                TotalPrice = (decimal)x.TotalPrice,
                OrderDetails = x.OrderDetails?.Select(y => new DisplayOrderDetailsDTO()
                {
                    BookId = y.BookId,
                    Quantity = y.Quantity,
                    UnitPrice = (decimal)y.UnitPrice
                }).ToList() ?? new List<DisplayOrderDetailsDTO>()
            });

            return Ok(ordersDTO);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get an order by id")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", Type = typeof(DisplayOrderDTO))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Order not found")]
        public IActionResult GetById(int id)
        {
            var order = _unit.OrderRepo.GetById(id);

            if (order == null)
            {
                return NotFound();
            }

            var orderDTO = new DisplayOrderDTO()
            {
                Id = order.Id,
                CustomerName = order.customer.FullName,
                OrderDate = order.OrderDate,
                Status = order.Status,
                TotalPrice = (decimal)order.TotalPrice,
                OrderDetails = order.OrderDetails.ConvertAll(x => new DisplayOrderDetailsDTO()
                {
                    BookId = x.BookId,
                    Quantity = x.Quantity,
                    UnitPrice = (decimal)x.UnitPrice
                })
            };

            return Ok(orderDTO);
        }

        [HttpDelete("{id}")]
        [Authorize("admin")]
        [SwaggerOperation(Summary = "Delete an order")]
        [SwaggerResponse(StatusCodes.Status200OK, "Order deleted successfully")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Order not found")]
        public IActionResult Delete(int id)
        {
            var order = _unit.OrderRepo.GetById(id);

            if (order == null)
            {
                return NotFound();
            }

            _unit.OrderRepo.Delete(id);
            _unit.Save();

            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize("admin")]
        [SwaggerOperation(Summary = "Update an order status")]
        [SwaggerResponse(StatusCodes.Status200OK, "Order status updated successfully")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Order not found")]
        public IActionResult UpdateStatus(int id, UpdateOrderStatusDTO status)
        {
            var order = _unit.OrderRepo.GetById(id);

            if (order == null)
            {
                return NotFound();
            }

            order.Status = status.Status;

            _unit.OrderRepo.Update(order);
            _unit.Save();

            return Ok();
        }
    }
}
