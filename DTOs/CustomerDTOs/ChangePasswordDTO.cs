namespace BookStoreAPI.DTOs.CustomerDTOs
{
    public class ChangePasswordDTO
    {
        public string Id { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}
