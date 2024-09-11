using Bill_system_API.Validation;
using System.ComponentModel.DataAnnotations;

namespace Bill_system_API.Models
{
    public class Company
    {
        public int Id { get; set; }
        [UniqueCompany]
        public string Name { get; set; }
        public string? Notes { get; set; }
        public virtual ICollection<Type>? Types { get; set; }
        public virtual ICollection<Item>? Items { get; set; }
        
    }
}
