using System.Text;
using LeaguesApi.Data;
using LeaguesApi.Data.Seeders;
using LeaguesApi.Filters;
using LeaguesApi.Middlewares;
using LeaguesApi.Models;
using LeaguesApi.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace LeaguesApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Leagues API", Version = "v1" });
            c.OperationFilter<ConditionalJwtFilter>();
            c.OperationFilter<ConditionalClientFilter>();
        });
        builder.Services.AddDbContext<ApplicationDbContext>(o =>
            o.UseSqlServer(builder.Configuration.GetConnectionString("default")));
        builder.Services.AddScoped<IAdminService, AdminService>();
        var jwtSettings = builder.Configuration.GetSection("Jwt");
        var key = jwtSettings["Key"];
        var issuer = jwtSettings["Issuer"];
        var audience = jwtSettings["Audience"];

        builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer("JwtBearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(key)) 
                };
            })
            .AddScheme<AuthenticationSchemeOptions, ClientCredentialsAuthHandler>("ClientCredentials", null);
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("JwtPolicy", policy =>
            {
                policy.AddAuthenticationSchemes("JwtBearer");
                policy.RequireAuthenticatedUser();
            });

            options.AddPolicy("ClientPolicy", policy =>
            {
                policy.AddAuthenticationSchemes("ClientCredentials");
                policy.RequireAuthenticatedUser();
            });


        });

        builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
        builder.Services.AddScoped<IPasswordHasher<Admin>, PasswordHasher<Admin>>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<ApplicationDbContext>();
                var passwordHasher = services.GetRequiredService<IPasswordHasher<Admin>>();

                var seeder = new ApplicationDbSeeder(context,passwordHasher);
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