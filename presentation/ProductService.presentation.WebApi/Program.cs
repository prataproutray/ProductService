using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using ProductService.core.application.command;
using ProductService.core.application.query;
using ProductService.core.domain;
using ProductService.infrastructure.infrastructure.DBContext;
using ProductService.infrastructure.infrastructure.Repository;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration["Mongo:MongoConnectionString"];
var databaseName = builder.Configuration["Mongo:Database"];

builder.Services.AddDbContext<ProductServiceContext>(options =>
{
    options.UseMongoDB(new MongoClient(connectionString), databaseName);
});
// Program.cs

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreateUserCommand).Assembly); // Assembly containing the command
    cfg.RegisterServicesFromAssembly(typeof(CreateUserCommandHandler).Assembly); // Assembly containing the handler
    cfg.RegisterServicesFromAssembly(typeof(GetUserById).Assembly);                                                                            // Add more assemblies as needed
    cfg.RegisterServicesFromAssembly(typeof(GetUserByIdCommandHandler).Assembly);                                                                            // Add more assemblies as needed
    cfg.RegisterServicesFromAssembly(typeof(GetAllUser).Assembly);                                                                            // Add more assemblies as needed
    cfg.RegisterServicesFromAssembly(typeof(GetAllUserCommandHandler).Assembly);                                                                            // Add more assemblies as needed
});
builder.Services.AddScoped<IUserRepository, UserRepository>();
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

app.MapControllers();

app.Run();
