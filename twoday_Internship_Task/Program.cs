
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using twoday_Internship_Task.Database;
using twoday_Internship_Task.Services;
using twoday_Internship_Task.Services.Interfaces;

namespace twoday_Internship_Task
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(typeof(Program).Assembly);
            builder.Services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("Sqlite")));

            builder.Services.AddScoped<IEnclosureService, EnclosureService>();
            builder.Services.AddScoped<IAnimalsService, AnimalsService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
