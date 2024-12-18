using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.DTOs.CustomerDTOs
{
    public class EditCustomerDTO
    {
        public string Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhone { get; set; }
        //[RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Invalid email format.")]
        public string CustomerEmail { get; set; }
        [Required]
        public string UserName { get; set; }
    }
}
