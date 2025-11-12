using ProductService.infrastructure.infrastructure.DBContext;
using MongoDB.Driver;
using ProductService.core.application.command;
using ProductService.core.application.query;
using ProductService.core.domain;
using ProductService.infrastructure.infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
namespace ProductService.presentation.WebApi.configurations
{
    public static class DependencyInjection
    {
         public static IServiceCollection RegisterDb(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["Mongo:MongoConnectionString"];
            var databaseName = configuration["Mongo:Database"];

            services.AddDbContext<ProductServiceContext>(options =>
            {
                options.UseMongoDB(new MongoClient(connectionString), databaseName);
            });
            return services;
        }
        public static IServiceCollection RegisterMediatR(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
                            {
                                cfg.RegisterServicesFromAssembly(typeof(CreateUserCommand).Assembly); // Assembly containing the command
                                cfg.RegisterServicesFromAssembly(typeof(CreateUserCommandHandler).Assembly); // Assembly containing the handler
                                cfg.RegisterServicesFromAssembly(typeof(GetUserById).Assembly);                                                                            // Add more assemblies as needed
                                cfg.RegisterServicesFromAssembly(typeof(GetUserByIdCommandHandler).Assembly);                                                                            // Add more assemblies as needed
                                cfg.RegisterServicesFromAssembly(typeof(GetAllUser).Assembly);                                                                            // Add more assemblies as needed
                                cfg.RegisterServicesFromAssembly(typeof(GetAllUserCommandHandler).Assembly);                                                                            // Add more assemblies as needed
                            });
            return services;
        }
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            // Program.cs

            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}