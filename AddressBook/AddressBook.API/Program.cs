using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<NorthwindContext>(options =>
  options.UseSqlServer(@"Server=localhost;Database=Northwind;Integrated Security=True;TrustServerCertificate=True"));

builder.Services.AddControllers();

var app = builder.Build();

/*
app.MapGet("/customer", () => {
    using var db = new NorthwindContext();
    var customers = db.Customers.AsNoTracking().ToList();
    return customers;
});

app.MapGet("/customer/{id}", (string id) => {
    using var db = new NorthwindContext();
    var customer = db.Customers.Find(id);
    if (customer is null) return Results.NotFound();
    return Results.Ok(customer);
});
*/

app.MapControllers();

app.Run();
