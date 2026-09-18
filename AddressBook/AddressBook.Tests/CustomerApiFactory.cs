extern alias AddressBookApi;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ApiProgram = AddressBookApi::Program;

namespace AddressBook.Tests;

public class CustomerApiFactory : WebApplicationFactory<ApiProgram>
{
    private readonly string _dbName = Guid.NewGuid().ToString();

    protected override void ConfigureWebHost(IWebHostBuilder builder) {
        builder.ConfigureServices(services => {
            services.RemoveAll<DbContextOptions<NorthwindContext>>();
            services.RemoveAll<Microsoft.EntityFrameworkCore.Infrastructure.IDbContextOptionsConfiguration<NorthwindContext>>();
            services.AddDbContext<NorthwindContext>(options =>
                options.UseInMemoryDatabase(_dbName));
        });
    }

    public void SeedCustomers(params Customer[] customers) {
        using var scope = Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<NorthwindContext>();
        db.Customers.AddRange(customers);
        db.SaveChanges();
    }
}
