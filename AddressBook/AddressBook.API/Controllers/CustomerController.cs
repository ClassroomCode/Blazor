using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
// [Authorize]
public class CustomerController(NorthwindContext db) 
    : ControllerBase
{
    [HttpGet("customer")]
    public ActionResult<Customer[]> GetAllCustomers(int offset = 0, int limit = 10) {
        if (offset < 0) {
            return BadRequest(new { message = "Offset must be positive" });
        }
        if (limit < 1 || limit > 20) {
            return BadRequest(new { message = "Limit must be in the range 1-20" });
        }

        var customers = db.Customers
            .AsNoTracking()
            .OrderBy(c => c.CustomerID)
            .Skip(offset).Take(limit)
            .ToArray();

        return customers;
    }

    [HttpGet("customer/{id}")]
    public ActionResult<Customer> GetCustomer(string id) {
        var customer = db.Customers
          .Include(c => c.Orders)
          .SingleOrDefault(c => c.CustomerID == id);

        if (customer is null) return NotFound();
        return customer;
    }

    [HttpPost("customer")]
    public ActionResult CreateCustomer(Customer customer) {
        var existingCustomer = db.Customers.Find(customer.CustomerID);
        if (existingCustomer is not null) {
            ModelState.AddModelError("CustomerID", "A customer with this ID already exists");
        }
        if (!ModelState.IsValid) return ValidationProblem();

        db.Customers.Add(customer);
        db.SaveChanges();

        return CreatedAtAction("GetCustomer",
          new { id = customer.CustomerID }, customer);
    }

    [HttpPut("customer/{id}")]
    public ActionResult PutCustomer(string id, Customer customer) {
        var existingCustomer = db.Customers.Find(id);
        if (existingCustomer is null) {
            db.Customers.Add(customer);
            db.SaveChanges();
            return CreatedAtAction("GetCustomer",
              new { id = customer.CustomerID }, customer);
        }
        else {
            db.Entry(existingCustomer).CurrentValues.SetValues(customer);
            db.SaveChanges();
            return NoContent();
        }
    }

    [HttpDelete("customer/{id}")]
    public ActionResult DeleteCustomer(string id) {
        var existingCustomer = db.Customers.Find(id);
        if (existingCustomer is null) {
            return NotFound();
        }
        else {
            db.Customers.Remove(existingCustomer);
            db.SaveChanges();
            return NoContent();
        }
    }
}
