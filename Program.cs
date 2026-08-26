using gb_prod_api.Data;
using gb_prod_api.Services;
using Microsoft.AspNetCore.Mvc;
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
builder.Services.AddScoped<ProductionRecordService>();

// validation error
builder.Services
    .AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var key = context.ModelState
                .Where(x => x.Value?.Errors.Count > 0)
                .Where(x => x.Key != "request")
                .Select(x => x.Key)
                .First();

            var field = key.StartsWith("$.")
                ? key[2..]
                : key;

            var errorMessage = context.ModelState[key]!.Errors.First().ErrorMessage;

            var response = new
            {
                title = "Validation",
                status = StatusCodes.Status400BadRequest,
                detail = errorMessage,
                field
            };

            return new BadRequestObjectResult(response);
        };
    });

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
