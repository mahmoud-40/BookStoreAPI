using BookStoreAPI.DTOs.CustomerDTOs;
using BookStoreAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Customer Management")]
    public class CustomerController : ControllerBase
    {
        UserManager<IdentityUser> userManager; 
        RoleManager<IdentityRole> roleManager;

        public CustomerController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }


        [HttpPost]
        [SwaggerOperation(Summary = "Create a new customer")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Failed")]
        public IActionResult Create(AddCustomerDTO customerDTO)
        {
            Customer customer = new Customer()
            {
                FullName = customerDTO.CustomerName,
                Address = customerDTO.CustomerAddress,
                PhoneNumber = customerDTO.CustomerPhone,
                Email = customerDTO.CustomerEmail,
                UserName = customerDTO.UserName
            };

            IdentityResult result = userManager.CreateAsync(customer, customerDTO.Password).Result; // here we are creating a new customer by passing the customer object and password to the CreateAsync method, which will create a new customer in the database.

            if (result.Succeeded)
            {
                IdentityResult roleResult = userManager.AddToRoleAsync(customer, "customer").Result; // here we are adding the customer to the customer role.
                if (roleResult.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest( roleResult.Errors);
                }
            }
            else
            {
                return BadRequest( result.Errors);
            }
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all customers")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", Type = typeof(List<SelectCustomerDTO>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Not Found")]
        public IActionResult GetAll()
        {
            var customers = userManager.GetUsersInRoleAsync("customer").Result; // here we are getting all the customers by using the GetUsersInRoleAsync method, which will return a list of customers.

            List<SelectCustomerDTO> customerDTOs = new List<SelectCustomerDTO>();

            foreach (Customer customer in customers)
            {
                SelectCustomerDTO customerDTO = new SelectCustomerDTO()
                {
                    FullName = customer.FullName,
                    Address = customer.Address,
                    PhoneNumber = customer.PhoneNumber,
                    Email = customer.Email,
                    UserName = customer.UserName
                };

                customerDTOs.Add(customerDTO);
            }

            return Ok(customerDTOs);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get a customer by id")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", Type = typeof(SelectCustomerDTO))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Not Found")]
        public IActionResult GetById(string id)
        {
            var customer = (Customer)userManager.FindByIdAsync(id).Result;

            if (customer != null)
            {
                SelectCustomerDTO customerDTO = new SelectCustomerDTO()
                {
                    FullName = customer.FullName,
                    Address = customer.Address,
                    PhoneNumber = customer.PhoneNumber,
                    Email = customer.Email,
                    UserName = customer.UserName
                };

                return Ok(customerDTO);

            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        [SwaggerOperation(Summary = "Edit a customer")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Failed")]
        public IActionResult Edit(EditCustomerDTO customerDTO)
        {
            var customer = (Customer)userManager.FindByIdAsync(customerDTO.Id).Result;

            if (customer != null)
            {
                customer.FullName = customerDTO.CustomerName;
                customer.Address = customerDTO.CustomerAddress;
                customer.PhoneNumber = customerDTO.CustomerPhone;
                customer.Email = customerDTO.CustomerEmail;
                customer.UserName = customerDTO.UserName;

                IdentityResult result = userManager.UpdateAsync(customer).Result;

                if (result.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(result.Errors);
                }
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("ChangePassword")]
        [Authorize]
        [SwaggerOperation(Summary = "Change password")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Failed")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Not Found")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
        public IActionResult ChangePassword(ChangePasswordDTO passwordDto)
        {
            if (ModelState.IsValid)
            {
                var user = userManager.FindByIdAsync(passwordDto.Id).Result;

                if (user != null)
                {
                    var result = userManager.ChangePasswordAsync(user, passwordDto.CurrentPassword, passwordDto.NewPassword).Result;

                    if (result.Succeeded)
                    {
                        return Ok();
                    }
                    else
                    {
                        return BadRequest(result.Errors);
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

    }
}
