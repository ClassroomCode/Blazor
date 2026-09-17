using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection.Metadata;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<TokenProvider>();

builder.Services.AddOpenApi();

var connStr = builder.Configuration.GetConnectionString("Northwind");
builder.Services.AddDbContext<NorthwindContext>(options =>
    options.UseSqlServer(connStr));

builder.Services.AddControllers().AddJsonOptions(options => {
    options.JsonSerializerOptions.ReferenceHandler =
      System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
  .AddJwtBearer(options => {
      options.RequireHttpsMetadata = false;
      options.TokenValidationParameters = new TokenValidationParameters {
          IssuerSigningKey = new SymmetricSecurityKey(
          Encoding.UTF8.GetBytes(builder.Configuration["Jwt:ClientSecret"]!)),
          ValidIssuer = builder.Configuration["Jwt:Issuer"]!,
          ValidAudience = builder.Configuration["Jwt:Audience"]!
      };
  });

builder.Services.AddAuthorization();

var corsOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? [];
builder.Services.AddCors(options => {
    options.AddPolicy("ClientApp", policy => {
        policy.WithOrigins(corsOrigins)
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseDeveloperExceptionPage();
}
else {
    app.UseExceptionHandler(app => {
        app.Run(async context => {
            await Results.Problem("An unexpected error occurred.").ExecuteAsync(context);
        });
    });
}

if (app.Environment.IsDevelopment()) {
    app.MapOpenApi();
    app.UseSwaggerUI(options => {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });
}

app.UseCors("ClientApp");

app.UseAuthentication();
app.UseAuthorization();

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
