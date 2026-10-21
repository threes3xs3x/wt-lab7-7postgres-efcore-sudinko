namespace wt_lab6_6minimalapi_sudinko.Models;

public class Flight
{
    public int Id { get; set; }
    public string Number { get; set; } = "";
    public string Origin { get; set; } = "";
    public string Destination { get; set; } = "";
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; }
}