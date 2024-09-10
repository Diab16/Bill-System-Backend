namespace Bill_system_API.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Item> Items { get; set; }

    }
}
