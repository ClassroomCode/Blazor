using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
public class CustomerController : ControllerBase
{
    [HttpGet("customer")]
    public ActionResult<List<Customer>> GetAllCustomers() {
        using var db = new NorthwindContext();
        var customers = db.Customers.AsNoTracking().ToList();
        return customers;
    }

    [HttpGet("customer/{id}")]
    public ActionResult<Customer> GetCustomer(string id) {
        using var db = new NorthwindContext();
        var customer = db.Customers.Find(id);
        if (customer is null) return NotFound();
        return customer;
    }
}
