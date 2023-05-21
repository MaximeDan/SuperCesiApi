namespace SuperCesiApi.Models;

public class SuperHero
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string PhoneNumber { get; set; }
    
    public List<Incident> Incidents { get; set; }

}