using BookStoreAPI.DTOs.AdminDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Admin Management")]
    public class AdminController : ControllerBase
    {
        UserManager<IdentityUser> userManager;
        RoleManager<IdentityRole> roleManager;

        public AdminController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new admin")]
        [SwaggerResponse(StatusCodes.Status200OK, "Admin added successfully")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Failed to create admin")]
        public IActionResult Create(AddAdminDTO adminDTO)
        {
            IdentityUser admin = new IdentityUser()
            {
                UserName = adminDTO.UserName,
                Email = adminDTO.Email,
                PhoneNumber = adminDTO.Phone
            };

            IdentityResult result = userManager.CreateAsync(admin, adminDTO.Password).Result;

            if (result.Succeeded)
            {
                IdentityResult roleResult = userManager.AddToRoleAsync(admin, "admin").Result;
                if (roleResult.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(roleResult.Errors);
                }
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all admins")]
        [SwaggerResponse(StatusCodes.Status200OK, "Successfully retrieved all admins")]
        public IActionResult GetAllAdmins()
        {
            var _admins = userManager.GetUsersInRoleAsync("admin").Result;

            var adminsDtos = new List<DisplayAdminDTO>();

            foreach (var admin in _admins)
            {
                adminsDtos.Add(new DisplayAdminDTO
                {
                    UserName = admin.UserName,
                    Email = admin.Email,
                    Phone = admin.PhoneNumber
                });
            }

            return Ok(adminsDtos);
        }
    }
}
