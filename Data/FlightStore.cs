using wt_lab6_6minimalapi_sudinko.Models;

namespace wt_lab6_6minimalapi_sudinko.Data;

public static class FlightStore
{
    public static List<Flight> Flights { get; } =
        new()
        {
            new Flight
            {
                Id = 1,
                Number = "B2-101",
                Origin = "Минск",
                Destination = "Москва",
                Price = 150,
                IsAvailable = true
            },
            new Flight
            {
                Id = 2,
                Number = "B2-202",
                Origin = "Минск",
                Destination = "Варшава",
                Price = 120,
                IsAvailable = true
            },
            new Flight
            {
                Id = 3,
                Number = "B2-303",
                Origin = "Минск",
                Destination = "Стамбул",
                Price = 260,
                IsAvailable = false
            },
            new Flight
            {
                Id = 4,
                Number = "B2-404",
                Origin = "Минск",
                Destination = "Тбилиси",
                Price = 230,
                IsAvailable = true
            },
            new Flight
            {
                Id = 5,
                Number = "B2-505",
                Origin = "Минск",
                Destination = "Дубай",
                Price = 480,
                IsAvailable = true
            }
        };
}