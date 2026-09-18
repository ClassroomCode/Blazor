using AddressBook.ClientApp.Client.Pages;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace AddressBook.Tests;

public class CustomersTests : BunitContext
{
    private readonly Mock<INorthwindService> _northwindServiceMock = new();

    public CustomersTests() {
        Services.AddSingleton(_northwindServiceMock.Object);
    }

    [Fact]
    public void ShowsLoadingMessage_WhileCustomersAreBeingFetched() {
        var tcs = new TaskCompletionSource<Customer[]>();
        _northwindServiceMock.Setup(s => s.GetCustomers()).Returns(tcs.Task);

        var cut = Render<Customers>();

        Assert.Contains("Loading...", cut.Markup);
    }

    [Fact]
    public void RendersCustomerRow_ForEachCustomerReturnedByTheService() {
        var customers = new[]
        {
            new Customer { CustomerID = "ALFKI", CompanyName = "Alfreds Futterkiste" },
            new Customer { CustomerID = "ANATR", CompanyName = "Ana Trujillo Emparedados" },
        };
        _northwindServiceMock.Setup(s => s.GetCustomers()).ReturnsAsync(customers);

        var cut = Render<Customers>();
        cut.WaitForState(() => cut.FindAll("tbody tr").Count == 2, TimeSpan.FromSeconds(2));

        var rows = cut.FindAll("tbody tr");
        Assert.Equal(2, rows.Count);
        Assert.Contains("Alfreds Futterkiste", cut.Markup);
        Assert.Contains("Ana Trujillo Emparedados", cut.Markup);
    }

    [Fact]
    public void RendersEditLink_WithCorrectCustomerId() {
        var customers = new[]
        {
            new Customer { CustomerID = "ALFKI", CompanyName = "Alfreds Futterkiste" },
        };
        _northwindServiceMock.Setup(s => s.GetCustomers()).ReturnsAsync(customers);

        var cut = Render<Customers>();
        cut.WaitForState(() => cut.FindAll("a").Count == 1, TimeSpan.FromSeconds(2));

        var link = cut.Find("a");
        Assert.Equal("/customer/ALFKI/edit", link.GetAttribute("href"));
    }

    [Fact]
    public void RendersNoRows_WhenServiceReturnsNoCustomers() {
        _northwindServiceMock.Setup(s => s.GetCustomers()).ReturnsAsync([]);

        var cut = Render<Customers>();
        cut.WaitForState(() => cut.Markup.Contains("<table"), TimeSpan.FromSeconds(2));

        Assert.Empty(cut.FindAll("tbody tr"));
    }

    [Fact]
    public void CallsGetCustomersExactlyOnce() {
        _northwindServiceMock.Setup(s => s.GetCustomers()).ReturnsAsync([]);

        var cut = Render<Customers>();
        cut.WaitForState(() => cut.Markup.Contains("<table"), TimeSpan.FromSeconds(2));

        _northwindServiceMock.Verify(s => s.GetCustomers(), Times.Once);
    }
}
