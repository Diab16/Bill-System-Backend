using Microsoft.EntityFrameworkCore;

namespace Bill_system_API.Models
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }
        public DbSet<Item> Items{ get; set; }
        public DbSet<Type> Types{ get; set; }
        public DbSet<Unit>Units  { get; set; }




    }
}
