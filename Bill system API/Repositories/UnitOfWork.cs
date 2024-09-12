using Bill_system_API.IRepositories;
using Bill_system_API.Models;

namespace Bill_system_API.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context ;
        public IGenericRepository<Unit> Units { get; private set; }

        public IGenericRepository<Models.Type> Types { get; private set; }

        public IGenericRepository<Item> Items { get; private set; }

        public IGenericRepository<InvoiceItem> InvoiceItems { get; private set; }

        public IGenericRepository<Invoice> Invoices { get; private set; }

        public IGenericRepository<Employee> Employees { get; private set; }

        public IGenericRepository<Company> Companies { get; private set; }

        public IGenericRepository<Client> Clients { get; private set; }

        public UnitOfWork(ApplicationDbContext _contect)
        {
            context= _contect;
            Clients=new GenericRepository<Client>(context);
            Companies=new GenericRepository<Company>(context);
            Employees=new GenericRepository<Employee>(context);
            Invoices=new GenericRepository<Invoice>(context);
            InvoiceItems=new GenericRepository<InvoiceItem>(context);
            Items = new GenericRepository<Item>(context);
            Types = new GenericRepository<Models.Type>(context);
            Units=new GenericRepository<Unit>(context);
        }
        public int Complete()
        {
            return context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
