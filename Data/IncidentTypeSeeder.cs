using SuperCesiApi.Data;

namespace SuperCesiApi.Models;

public class IncidentTypeSeeder
{
    public static void Seed(SuperCesiApiDbContext context)
    {
        // Check if incidents already exist in the database
        if (context.IncidentTypes.Any())
        {   
            return; // Data already seeded
        }

        // Create and add incidents
        var incidentTypes = new[]
        {
            new IncidentType { Name = "Fire", DisplayName = "Incendie"},
            new IncidentType { Name = "CarAccident", DisplayName = "Accident routier"},
            new IncidentType { Name = "BoatAccident", DisplayName = "Accident fluviale"},
            new IncidentType { Name = "PlaneAccident", DisplayName = "Accident aérien"},
            new IncidentType { Name = "LandSlide", DisplayName = "Eboulement"},
            new IncidentType { Name = "SnakeInvasion", DisplayName = "Invasion de serpent"},
            new IncidentType { Name = "GasLeak", DisplayName = "Fuite de gaz"},
            new IncidentType { Name = "Strike", DisplayName = "Manifestation"},
            new IncidentType { Name = "Robbery", DisplayName = "Braquage"},
            new IncidentType { Name = "PrisonerEscape", DisplayName = "Evasion d’un prisonnier"}
        }; 

        context.IncidentTypes.AddRange(incidentTypes);
        context.SaveChanges();
    }
}