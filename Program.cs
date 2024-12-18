
using System.Text;
using BookStoreAPI.Models;
using BookStoreAPI.Repository;
using BookStoreAPI.UnitOfWorks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;


namespace BookStoreAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            const string corsPolicy = "AllowAll";

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "BookStoreAPI",
                        Version = "v1",
                        Description =
                            "The BookStore API provides a RESTful interface for managing an online bookstore’s inventory, customer orders, and author information. The API allows users to view available books, manage inventory, handle customer orders, and retrieve author details.",
                        TermsOfService = new Uri("http://tempuri.org/terms"),
                        Contact = new OpenApiContact
                        {
                            Name = "Mahmoud Abdulmawlaa",
                            Email = "mahmouda.mawlaa@gmail.com"
                        },
                    }
                );
                c.EnableAnnotations();
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(corsPolicy,
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                    });
            });

            builder.Services.AddDbContext<BookStoreContext>(options => options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("bookcon")));

            builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<BookStoreContext>();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateLifetime = true,
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("welcome to b3dsh very secret key"))
                    };
                });

            builder.Services.AddScoped<UnitOfWork>();

            var app = builder.Build();

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
