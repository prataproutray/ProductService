using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using ProductService.core.application.command;
using ProductService.core.application.query;
using ProductService.core.domain;
using ProductService.infrastructure.infrastructure.DBContext;
using ProductService.infrastructure.infrastructure.Repository;
using ProductService.presentation.WebApi.configurations;
using ProductService.presentation.WebApi.Middleware;
using Serilog;
using Serilog.Events;
using Microsoft.Extensions.Hosting;

Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug() // Set the minimum log level
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning) // Override for specific namespaces
            .Enrich.FromLogContext()
            .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day) // Log to a file, rolling daily
            .CreateLogger();
 Log.Information("Starting application...");
var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();
// Add services to the container.
builder.Services
.RegisterDb(builder.Configuration)
.RegisterMediatR()
.RegisterRepositories();

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "OpenAPI V1");
        options.RoutePrefix = string.Empty;
    });

}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseMiddleware<CorrelationIdMiddleware>();
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
app.MapControllers();

app.Run();
