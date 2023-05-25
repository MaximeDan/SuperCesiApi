namespace SuperCesiApi.Models;

public class IncidentType
{
    public int Id { get; set; }
    public string DisplayName { get; set; }
    public string Name { get; set; }
    
    public ICollection<SuperHero>? SuperHeroes { get; set; }

}