using Microsoft.EntityFrameworkCore;

public class NorthwindContext : DbContext, INorthwindService
{
    public NorthwindContext(DbContextOptions<NorthwindContext> options)
        : base(options) { }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }

    public async Task<Customer[]> GetCustomers() {
        var customers = await Customers.ToArrayAsync();
        return customers;
    }
}