namespace Bill_system_API.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
    }
}
