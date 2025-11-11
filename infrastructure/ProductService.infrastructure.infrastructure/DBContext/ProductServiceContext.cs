using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;
using ProductService.core.domain;

namespace ProductService.infrastructure.infrastructure.DBContext
{
    public class ProductServiceContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public ProductServiceContext(DbContextOptions<ProductServiceContext> options) : base(options){ }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToCollection("users"); // Map Product entity to "products" collection
        }

        public static ProductServiceContext Create(IMongoDatabase database)
        {
            return new ProductServiceContext(new DbContextOptionsBuilder<ProductServiceContext>()
                .UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName)
                .Options);
        }
    }
}