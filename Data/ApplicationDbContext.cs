using Microsoft.EntityFrameworkCore;
using TagerProject.Models.Entities;
namespace TagerProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){ }

        public DbSet<City> Cities { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tax> Taxes { get; set; }
        public DbSet<Unit> Units { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ExpenseItem> ExpenseItems { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Discount> Discounts  { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
         
    }
}
