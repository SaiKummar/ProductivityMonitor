using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProductivityMonitor.Contracts;
using ProductivityMonitor.Dao;
using ProductivityMonitor.Models;
using ProductivityMonitor.Repository;
using ProductivityMonitor.Utilities;
using System.Text;

namespace ProductivityMonitor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors();

            // Add services to the container.
            builder.Services.AddSingleton<INLoggerManager, NLoggerManager>();
            builder.Services.AddTransient<IManagerRepository, ManagerRepository>();
            builder.Services.AddTransient<IManagerDao, ManagerDao>();
            builder.Services.AddTransient<IJwtTokenManager, JwtTokenManager>();

            builder.Services.AddAutoMapper(typeof(Program));

            //configure dbcontext with pgsql
            builder.Services.AddDbContext<AppDbContext>(
                options => options.UseNpgsql(builder.Configuration.GetConnectionString("csnpgsql"))
                );

            //for identity
            builder.Services.AddIdentity<CustomUser, CustomRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            //add authentication
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.SaveToken = true;
                x.RequireHttpsMetadata = false;
                x.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["JWT:Key"])),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });

            //for authentication
            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}