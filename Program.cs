using Microsoft.EntityFrameworkCore;
using wt_lab7_7postgres_efcore_sudinko.Data;
using wt_lab7_7postgres_efcore_sudinko.Models;

var builder = WebApplication.CreateBuilder(args);

var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection");

var databaseUrl =
    builder.Configuration["DATABASE_URL"];

if (!string.IsNullOrWhiteSpace(databaseUrl))
{
    var uri = new Uri(databaseUrl);
    var userInfo = uri.UserInfo.Split(':', 2);
    var dbPort = uri.Port > 0 ? uri.Port : 5432;

    connectionString =
        $"Host={uri.Host};" +
        $"Port={dbPort};" +
        $"Database={uri.AbsolutePath.TrimStart('/')};" +
        $"Username={Uri.UnescapeDataString(userInfo[0])};" +
        $"Password={Uri.UnescapeDataString(userInfo[1])};" +
        "SSL Mode=Disable";
}

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddOpenApi();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider
        .GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGet("/", () => "Minimal API работает!");

app.MapGet("/api/info", () =>
{
    return new
    {
        Application = "Web Technologies",
        Technology = "ASP.NET Core Minimal API + PostgreSQL",
        Version = "1.0"
    };
});

app.MapGet("/api/flights", async (AppDbContext db) =>
{
    var flights = await db.Flights.ToListAsync();
    return Results.Ok(flights);
});

app.MapGet("/api/flights/search", async (
    string? search,
    AppDbContext db) =>
{
    var query = db.Flights.AsQueryable();

    if (!string.IsNullOrWhiteSpace(search))
    {
        query = query.Where(x =>
            x.Destination.ToLower().Contains(search.ToLower()));
    }

    var flights = await query.ToListAsync();
    return Results.Ok(flights);
});

app.MapGet("/api/flights/{id}", async (
    int id,
    AppDbContext db) =>
{
    var flight = await db.Flights
        .FirstOrDefaultAsync(x => x.Id == id);

    if (flight == null)
        return Results.NotFound();

    return Results.Ok(flight);
});

app.MapPost("/api/flights", async (
    Flight flight,
    AppDbContext db) =>
{
    db.Flights.Add(flight);
    await db.SaveChangesAsync();

    return Results.Created(
        $"/api/flights/{flight.Id}",
        flight);
});

app.MapPut("/api/flights/{id}", async (
    int id,
    Flight updatedFlight,
    AppDbContext db) =>
{
    var flight = await db.Flights
        .FirstOrDefaultAsync(x => x.Id == id);

    if (flight == null)
        return Results.NotFound();

    flight.Number = updatedFlight.Number;
    flight.Origin = updatedFlight.Origin;
    flight.Destination = updatedFlight.Destination;
    flight.Price = updatedFlight.Price;
    flight.IsAvailable = updatedFlight.IsAvailable;

    await db.SaveChangesAsync();

    return Results.Ok(flight);
});

app.MapDelete("/api/flights/{id}", async (
    int id,
    AppDbContext db) =>
{
    var flight = await db.Flights
        .FirstOrDefaultAsync(x => x.Id == id);

    if (flight == null)
        return Results.NotFound();

    db.Flights.Remove(flight);
    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapGet("/api/flights/expensive", async (AppDbContext db) =>
{
    var flights = await db.Flights
        .Where(x => x.Price > 100)
        .ToListAsync();
    return Results.Ok(flights);
});

app.MapGet("/api/flights/sorted", async (AppDbContext db) =>
{
    var flights = await db.Flights
        .OrderBy(x => x.Price)
        .ToListAsync();
    return Results.Ok(flights);
});

app.MapGet("/api/flights/available", async (AppDbContext db) =>
{
    var flights = await db.Flights
        .Where(x => x.IsAvailable)
        .OrderBy(x => x.Number)
        .ToListAsync();
    return Results.Ok(flights);
});

app.MapGet("/api/flights/from/{origin}", async (
    string origin,
    AppDbContext db) =>
{
    var flights = await db.Flights
        .Where(x => x.Origin == origin)
        .OrderByDescending(x => x.Price)
        .ToListAsync();
    return Results.Ok(flights);
});

var port = Environment.GetEnvironmentVariable("PORT") ?? "10000";
app.Run($"http://0.0.0.0:{port}");