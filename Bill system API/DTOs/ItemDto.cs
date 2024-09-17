using Bill_system_API.Models;
using Bill_system_API.Validatioons;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Type = Bill_system_API.Models.Type;

namespace Bill_system_API.DTOs
{
    public class ItemDto
    {

        public int Id { get; set; }  
        [Required]
        [ItemNameUniqueValidation]
        public string? Name { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Available Amount must be greater than 0.")]
        public double SellingPrice { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Available Amount must be greater than 0.")]
        public double BuyingPrice { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Available Amount must be greater than 0.")]
        public int AvailableAmount { get; set; }
        [Required]
        public int CompanyId { get; set; }
        [Required]

        public int TypeId { get; set; }
        [Required]
        public int UnitId { get; set; }

        public string? Notes { get; set; }
        public ItemCompanyDto? Company { get; set; }
        public itemTypeDto? Type { get; set; }
        public itemUnitDto? Unit { get; set; }



    }
}
