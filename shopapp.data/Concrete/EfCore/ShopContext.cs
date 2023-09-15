using Microsoft.EntityFrameworkCore;
using shopapp.data.Configurations;
using shopapp.entity;


namespace shopapp.data.Concrete.EfCore
{
    public class ShopContext: DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options):base(options)
        {          
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new ProductCategoryConfiguration());

            // builder.Seed();
        }   
    }
}