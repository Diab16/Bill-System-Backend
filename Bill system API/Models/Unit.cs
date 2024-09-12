namespace Bill_system_API.Models
{
    public class Unit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Notes { get; set; }
        public virtual ICollection<Item>? Items { get; set; }
    }
}
