using Microsoft.EntityFrameworkCore;
using MVC_Web.Entities;

namespace MVC_Web.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Manufacturer> Manufacturer { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<OrdersDetail> OrdersDetails { get; set; }
        public DbSet<Order> Orders {  get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public DatabaseContext(DbContextOptions options):base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
