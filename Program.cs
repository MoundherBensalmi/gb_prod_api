using gb_prod_api.Data;
using gb_prod_api.Services;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

// Register services
builder.Services.AddScoped<ProductionDayService>();
builder.Services.AddScoped<TunnelService>();

// ----------------------
var app = builder.Build();

// Destructive: wipes every table before reseeding. Opt in with "dotnet run -- --seed".
if (args.Contains("--seed"))
{
    using var seedScope = app.Services.CreateScope();
    var seedDbContext = seedScope.ServiceProvider.GetRequiredService<AppDbContext>();
    await DbSeeder.SeedAsync(seedDbContext);
    Console.WriteLine("Database cleared and seeded.");
    return;
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
