using Bill_system_API.Models;

namespace Bill_system_API.DTOs
{
    public class UnitDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Notes { get; set; }
        //public ICollection<Item>? Items { get; set; }

    }
}
