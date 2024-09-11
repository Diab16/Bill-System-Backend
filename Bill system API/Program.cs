
using Bill_system_API.IRepositories;
using Bill_system_API.Models;
using Bill_system_API.Repositories;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using static System.Net.Mime.MediaTypeNames;
using Type = Bill_system_API.Models.Type;

namespace Bill_system_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            string txt = "";
            // Add services to the container.
            builder.Services.AddDbContext<ApplicationDbContext>(op => op.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("cslocal")));
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(txt,
                builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });
            });

            //Repository
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

            app.UseAuthorization();

            app.UseCors(txt);

            app.MapControllers();

            app.Run();
        }
    }
}
