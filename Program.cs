using wt_lab6_6minimalapi_sudinko.Data;
using wt_lab6_6minimalapi_sudinko.Models;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
var app = builder.Build();

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
        Technology = "ASP.NET Core Minimal API",
        Version = "1.1"
    };
});

app.MapGet("/api/flights", (string? search) =>
{
    var flights = FlightStore.Flights.AsEnumerable();

    if (!string.IsNullOrWhiteSpace(search))
    {
        flights = flights.Where(x =>
            x.Destination.Contains(
                search,
                StringComparison.OrdinalIgnoreCase));
    }

    return Results.Ok(flights);
});

app.MapGet("/api/flights/{id}", (int id) =>
{
    var flight = FlightStore.Flights
        .FirstOrDefault(x => x.Id == id);

    if (flight == null)
        return Results.NotFound();

    return Results.Ok(flight);
});

app.MapPost("/api/flights", (Flight flight) =>
{
    if (string.IsNullOrWhiteSpace(flight.Number) || flight.Price <= 0)
        return Results.BadRequest("Номер рейса не может быть пустым, цена должна быть больше нуля.");

    var newId = FlightStore.Flights.Count == 0
        ? 1
        : FlightStore.Flights.Max(x => x.Id) + 1;

    flight.Id = newId;
    FlightStore.Flights.Add(flight);

    return Results.Created($"/api/flights/{flight.Id}", flight);
});

app.MapPut("/api/flights/{id}", (int id, Flight updatedFlight) =>
{
    var flight = FlightStore.Flights
        .FirstOrDefault(x => x.Id == id);

    if (flight == null)
        return Results.NotFound();

    flight.Number = updatedFlight.Number;
    flight.Origin = updatedFlight.Origin;
    flight.Destination = updatedFlight.Destination;
    flight.Price = updatedFlight.Price;
    flight.IsAvailable = updatedFlight.IsAvailable;

    return Results.Ok(flight);
});

app.MapDelete("/api/flights/{id}", (int id) =>
{
    var flight = FlightStore.Flights
        .FirstOrDefault(x => x.Id == id);

    if (flight == null)
        return Results.NotFound();

    FlightStore.Flights.Remove(flight);
    return Results.NoContent();
});

var port = Environment.GetEnvironmentVariable("PORT") ?? "10000";
app.Run($"http://0.0.0.0:{port}");
