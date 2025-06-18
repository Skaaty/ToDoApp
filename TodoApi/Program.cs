using System;
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using TodoApi.Data;
using TodoApi.Services;

namespace TodoApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure SQLite DbContext
            builder.Services.AddDbContext<TodoContext>(opt =>
                opt.UseSqlite(builder.Configuration.GetConnectionString("Default") ??
                              "Data Source=todo.db"));

            // Register AutoMapper profiles
            builder.Services.AddAutoMapper(typeof(MappingService));

            // Add MVC controllers
            builder.Services.AddControllers();

            // Enable FluentValidation automatic model validation
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddValidatorsFromAssemblyContaining<Program>();

            // Swagger/OpenAPI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // Map attribute‑routed controllers
            app.MapControllers();

            app.Run();
        }
    }
}
