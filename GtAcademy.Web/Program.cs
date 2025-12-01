using GtAcademy.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructre(builder.Configuration);

var app = builder.Build();


app.MapGet("/", () => "Hello World!");

app.Run();
