using System.Net;
using System.Net.Http.Json;

namespace AddressBook.Tests;

public class CustomerApiTests
{
    [Fact]
    public async Task GetAllCustomers_ReturnsOkWithSeededCustomers() {
        using var factory = new CustomerApiFactory();
        factory.SeedCustomers(
            new Customer { CustomerID = "ALFKI", CompanyName = "Alfreds Futterkiste" },
            new Customer { CustomerID = "ANATR", CompanyName = "Ana Trujillo Emparedados" });
        var client = factory.CreateClient();

        var response = await client.GetAsync("/customer");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var customers = await response.Content.ReadFromJsonAsync<Customer[]>();
        Assert.NotNull(customers);
        Assert.Equal(2, customers!.Length);
        Assert.Contains(customers, c => c.CustomerID == "ALFKI");
        Assert.Contains(customers, c => c.CustomerID == "ANATR");
    }

    [Fact]
    public async Task GetAllCustomers_RespectsOffsetAndLimit() {
        using var factory = new CustomerApiFactory();
        factory.SeedCustomers(
            new Customer { CustomerID = "AAAAA", CompanyName = "A Company" },
            new Customer { CustomerID = "BBBBB", CompanyName = "B Company" },
            new Customer { CustomerID = "CCCCC", CompanyName = "C Company" });
        var client = factory.CreateClient();

        var response = await client.GetAsync("/customer?offset=1&limit=1");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var customers = await response.Content.ReadFromJsonAsync<Customer[]>();
        Assert.NotNull(customers);
        var customer = Assert.Single(customers!);
        Assert.Equal("BBBBB", customer.CustomerID);
    }

    [Theory]
    [InlineData(-1, 10)]
    [InlineData(0, 0)]
    [InlineData(0, 21)]
    public async Task GetAllCustomers_ReturnsBadRequest_ForInvalidOffsetOrLimit(int offset, int limit) {
        using var factory = new CustomerApiFactory();
        var client = factory.CreateClient();

        var response = await client.GetAsync($"/customer?offset={offset}&limit={limit}");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
