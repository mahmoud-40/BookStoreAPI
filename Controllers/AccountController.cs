using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookStoreAPI.DTOs.AccountDTOs;
using BookStoreAPI.DTOs.CustomerDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Account Management")]
    public class AccountController : ControllerBase
    {
        SignInManager<IdentityUser> signInManager;
        UserManager<IdentityUser> userManager;

        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Login")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", typeof(string))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Invalid UserName or Password")]
        public IActionResult LogIn(LogInDTO logInDTO)
        {
            var result = signInManager.PasswordSignInAsync(logInDTO.UserName, logInDTO.Password, false, false).Result;

            if (result.Succeeded)
            {
                var _user = userManager.FindByNameAsync(logInDTO.UserName).Result;

                var userdata = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, _user.UserName),
                    new Claim(ClaimTypes.Email, _user.Email),
                    new Claim(ClaimTypes.NameIdentifier, _user.Id)
                };

                var roles = userManager.GetRolesAsync(_user).Result;

                foreach (var role in roles)
                {
                    userdata.Add(new Claim(ClaimTypes.Role, role));
                }

                string key = "welcome to b3dsh very secret key";
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    claims: userdata,
                    expires: DateTime.Now.AddDays(2),
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(tokenString);
            }
            else
            {
                return Unauthorized("Invalid UserName or Password");
            }
        }

        [HttpPut("ChangePassword")]
        [Authorize]
        [SwaggerOperation(Summary = "Change Password")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Failed")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Not Found")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
        public IActionResult ChangePassword(ChangePasswordDTO passwordDto)
        {
            if (ModelState.IsValid)
            {
                var user = userManager.FindByIdAsync(User.Identity.Name).Result;

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

        [HttpPost("logout")]
        [Authorize]
        [SwaggerOperation(Summary = "Logout")]
        public IActionResult LogOut()
        {
            signInManager.SignOutAsync(); // Expire the token
            return Ok();
        }
    }
}

// User.Identity.[Name, Email, NameIdentifier, Role] -> Claims
