namespace SuperCesiApi.Models;

public class Incident
{
    public int Id { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public bool IsResolved { get; set; }
    public int IncidentTypeId { get; set; }
    public string City { get; set; }
    public IncidentType IncidentType { get; set; }
}