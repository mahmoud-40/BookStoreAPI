using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.DTOs.CustomerDTOs
{
    public class AddCustomerDTO
    {
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhone { get; set; }
        //[RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Invalid email format.")]
        public string CustomerEmail { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        //[RegularExpression( @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,100}$", ErrorMessage = "Password must be 8-15 characters and contain at least one uppercase letter, one lowercase letter, and one number.")]
        public string Password { get; set; }
    }
}
