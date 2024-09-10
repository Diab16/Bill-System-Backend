namespace Bill_system_API.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public ICollection<Invoice> Invoices{ get; set; }
    }
}
