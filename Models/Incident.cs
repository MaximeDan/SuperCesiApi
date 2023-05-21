namespace SuperCesiApi.Models;

public class Incident
{
    public int Id { get; set; }
    public string Type { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public bool IsResolved { get; set; }
    
    public int CityId { get; set; }
    
    public City City { get; set; }
    public ICollection<SuperHero> SuperHeroes { get; set; }
}