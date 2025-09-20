using LeaguesApi.Data;
using LeaguesApi.Data.Seeders;
using Microsoft.EntityFrameworkCore;

namespace LeaguesApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<ApplicationDbContext>(o =>
            o.UseSqlServer(builder.Configuration.GetConnectionString("default")));
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<ApplicationDbContext>();
                var seeder = new ApplicationDbSeeder(context);
                seeder.Seed();
            }
            catch (Exception ex)
            {
               
            }
        }

        app.MapControllers();

        app.Run();
    }
}