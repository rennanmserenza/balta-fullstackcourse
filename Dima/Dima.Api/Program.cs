using Dima.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var cnnStr = builder
    .Configuration
    .GetConnectionString("DefaultConnection")
    ?? string.Empty;

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(cnnStr);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.CustomSchemaIds(n => n.FullName);
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "Hello World!");

app.Run();
