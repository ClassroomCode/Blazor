using Entities;
using Microsoft.EntityFrameworkCore;

internal class NorthwindContext : DbContext, INorthwindService
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }

    public async Task<Customer[]> GetCustomers() {
        return await Customers.ToArrayAsync();
    }

    public IQueryable<Customer> GetCustomersDef() {
        return Customers;
    }

    public async Task<Customer[]> ToAnArray(IQueryable<Customer> q) {
        return await q.ToArrayAsync();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        optionsBuilder.LogTo(Console.WriteLine);
        optionsBuilder.UseSqlServer(@"Server=localhost;Database=Northwind;Integrated Security=True;TrustServerCertificate=True");
    }
}