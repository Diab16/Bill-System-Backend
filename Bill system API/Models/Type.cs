namespace Bill_system_API.Models
{
    public class Type
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Notes { get; set; }
        public int CompanyId{ get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<Item>? Items { get; set; }

    }
}
