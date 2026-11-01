namespace wt_lab7_7postgres_efcore_sudinko.Models;

public class Flight
{
    public int Id { get; set; }
    public string Number { get; set; } = "";
    public string Origin { get; set; } = "";
    public string Destination { get; set; } = "";
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Description { get; set; } = "";
}