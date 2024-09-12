using Microsoft.EntityFrameworkCore;

namespace Bill_system_API.Models
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies();
            }
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }
        public DbSet<Item> Items{ get; set; }
        public DbSet<Type> Types{ get; set; }
        public DbSet<Unit>Units  { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             modelBuilder.Entity<Item>().HasKey(e => e.Id);

            modelBuilder.Entity<Company>().HasData(
                 new Company { Id = 1 , Name = "Apple"},
                 new Company { Id = 2 , Name = "Samsung"}
                );


            modelBuilder.Entity<Type>().HasData(
                new Type { Id = 1, Name = "Elctronics" }
               );

            modelBuilder.Entity<Unit>().HasData(
                new Company { Id = 1, Name = "Item" },
                new Company { Id = 2, Name = "m" }
               );

            base.OnModelCreating(modelBuilder);



        }

    }
}
