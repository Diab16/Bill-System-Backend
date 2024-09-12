
using Bill_system_API.IRepositories;
using Bill_system_API.Models;
using Bill_system_API.Repositories;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using System.Text.Json.Serialization;
using static System.Net.Mime.MediaTypeNames;
using Type = Bill_system_API.Models.Type;

using Bill_system_API.MappinigProfiles;
using Bill_system_API.Models;
using Microsoft.EntityFrameworkCore;

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
              //builder.Services.AddControllers().AddJsonOptions(x =>
              //x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
              //builder.Services.AddControllers();
            builder.Services.AddControllers().AddNewtonsoftJson(op=>op.SerializerSettings.ReferenceLoopHandling=Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle




            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            #region          ADDING Cros
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });
            #endregion
            builder.Services.AddDbContext<ApplicationDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("Alaa"));
            });
            builder.Services.AddAutoMapper(M => M.AddProfile(new ItemProfile()));
            builder.Services.AddAutoMapper(M => M.AddProfile(new CompanyProfile()));
            builder.Services.AddAutoMapper(M => M.AddProfile(new UnitProfile()));
            builder.Services.AddAutoMapper(M => M.AddProfile(new TypesProfile()));


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("AllowAllOrigins");

            app.UseAuthorization();

            app.UseCors(txt);

            app.MapControllers();

            app.Run();
        }
    }
}
