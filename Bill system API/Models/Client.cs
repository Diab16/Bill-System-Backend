using Bill_system_API.Validation;
using System.ComponentModel.DataAnnotations;


namespace Bill_system_API.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public virtual ICollection<Invoice>? Invoices{ get; set; }
    }
}
