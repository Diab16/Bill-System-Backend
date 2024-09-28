using System.ComponentModel.DataAnnotations;

namespace Bill_system_API.DTOs
{
    public class ClientDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Client Name is required.")]
        public string Name { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
    }
}
