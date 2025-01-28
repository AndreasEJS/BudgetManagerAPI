using BudgetManagerAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace BudgetManagerAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<BudgetContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            // Add services to the container.

            builder.Services.AddControllers();


            //CORS
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("https://budget-manager-frontend-kvvucuqej-andreas-projects-10cbca8b.vercel.app") // Din Vercel-frontend-URL
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors();

            app.UseAuthorization();

            app.MapGet("/", () => "Välkommen till transaktionshanteraren");

            app.MapControllers();

            app.Run();
        }
    }
}
