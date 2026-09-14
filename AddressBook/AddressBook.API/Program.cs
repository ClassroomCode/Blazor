var builder = WebApplication.CreateBuilder(args);

// services

var app = builder.Build();

app.Run(async context => {
    await context.Response.WriteAsync("Hello world!");
});

app.Run();