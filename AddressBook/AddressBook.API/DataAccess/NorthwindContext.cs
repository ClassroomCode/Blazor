using Microsoft.EntityFrameworkCore;

public class NorthwindContext : DbContext
{
    public NorthwindContext(DbContextOptions<NorthwindContext> options)
        : base(options) { }

    public DbSet<Customer> Customers { get; set; }
}