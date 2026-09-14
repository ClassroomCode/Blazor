var builder = WebApplication.CreateBuilder(args);

// services

var app = builder.Build();

// middleware

app.Run();