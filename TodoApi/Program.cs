using System.Text;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;   
using TodoApi.Data;
using TodoApi.Services;                                            

namespace TodoApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<TodoContext>(opt =>
            opt.UseSqlite(builder.Configuration.GetConnectionString("Default")  
                          ?? "Data Source=todo.db"));

        builder.Services.AddAutoMapper(typeof(MappingService));

        builder.Services.AddControllers();

        builder.Services.AddFluentValidationAutoValidation();

        builder.Services.AddValidatorsFromAssemblyContaining<Program>();

        var jwtKey = builder.Configuration["Jwt:Key"]              
                    ?? throw new InvalidOperationException("Missing Jwt:Key");

        builder.Services
               .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(opts =>
               {
                   opts.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = false,
                       ValidateAudience = false,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(
                                                      Encoding.UTF8.GetBytes(jwtKey))
                   };
               });


        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();   // <- musi byæ przed UseAuthorization
        app.UseAuthorization();

        app.MapControllers();
        app.Run();
    }
}