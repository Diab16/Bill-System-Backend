using Bill_system_API.IRepositories;
using Bill_system_API.Models;
using Bill_system_API.Repositories;
using Microsoft.EntityFrameworkCore;
using Bill_system_API.MappinigProfiles;
using Type = Bill_system_API.Models.Type;

namespace Bill_system_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<ApplicationDbContext>(op =>
                op.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("cslocal")));

            builder.Services.AddControllers().AddNewtonsoftJson(op =>
                op.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add CORS policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(); // IUnitOfWork

            // AutoMapper configuration
            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile(new ItemProfile());
                cfg.AddProfile(new CompanyProfile());
                cfg.AddProfile(new UnitProfile());
                cfg.AddProfile(new TypesProfile());
            });

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


            // Register repositories
            var types = new[] { typeof(Client), typeof(Company), typeof(Employee), typeof(Invoice), typeof(InvoiceItem), typeof(Item), typeof(Type), typeof(Unit) };
            foreach (var type in types)
            {
                var interfaceType = typeof(IGenericRepository<>).MakeGenericType(type);
                var implementationType = typeof(GenericRepository<>).MakeGenericType(type);
                builder.Services.AddScoped(interfaceType, implementationType);
            }

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // Use CORS
            app.UseCors("AllowAllOrigins");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
