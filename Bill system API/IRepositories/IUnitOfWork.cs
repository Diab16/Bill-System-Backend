using Bill_system_API.IRepositories;
using Bill_system_API.Models;
using Bill_system_API.Repositories;
using Type = Bill_system_API.Models.Type;

namespace Bill_system_API.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Unit> Units { get; }
        IGenericRepository<Type> Types { get; }
        IGenericRepository<Item> Items { get; }
        IGenericRepository<InvoiceItem> InvoiceItems { get; }
        IGenericRepository<Invoice> Invoices { get; }
        IGenericRepository<Employee> Employees { get; }
        IGenericRepository<Company> Companies { get; }
        IGenericRepository<Client> Clients { get; }

        int Complete();
    }
}
