var builder = WebApplication.CreateBuilder(args);

// services

var app = builder.Build();

app.MapGet("/customer", () => {
    using var db = new NorthwindContext();
    var customers = db.Customers.ToList();
    return customers;
});

app.MapGet("/customer/{id}", (string id) => {
    using var db = new NorthwindContext();
    var customer = db.Customers.Find(id);
    if (customer is null) return Results.NotFound();
    return Results.Ok(customer);
});

app.Run();

