using System.ComponentModel.DataAnnotations;

namespace Bill_system_API.DTOs
{
    public class R_NewUserDTO
    {
        [Required]
        public string userName {  get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string email { get; set; }
        public string? phoneNumber { get; set; }
    }
}
