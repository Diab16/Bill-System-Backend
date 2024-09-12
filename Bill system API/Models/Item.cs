using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bill_system_API.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public double SellingPrice { get; set; }
        public double BuyingPrice { get; set; }
        public int AvailableAmount {  get; set; }
        public string? Notes { get; set; }
        public int AvailableAmount { get; set; }
        [ForeignKey("Company")]
        public int CompanyId { get; set; }
        public virtual Company? Company { get; set; }
        public virtual Company? Company { get; set; }
        [ForeignKey("Type")]
        public int TypeId { get; set; }
        public virtual Type? Type { get; set; }
        public virtual Type? Type { get; set; }
        [ForeignKey("Unit")]
        public int UnitId { get; set; }
        public virtual Unit? Unit { get; set; }
        //public ICollection<invoiceItem> invoices { get; set; }

    }
}
