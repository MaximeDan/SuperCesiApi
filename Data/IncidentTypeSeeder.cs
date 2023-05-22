using SuperCesiApi.Data;

namespace SuperCesiApi.Models;

public class IncidentTypeSeeder
{
    public static void Seed(SuperCesiApiDbContext context)
    {
        // Check if incidents already exist in the database
        if (context.Incidents.Any())
        {
            return; // Data already seeded
        }

        // Create and add incidents
        var incidentTypes = new[]
        {
            new IncidentType { Name = "Incendie"},
            new IncidentType { Name = "Accident routier"},
            new IncidentType { Name = "Accident fluviale"},
            new IncidentType { Name = "Accident aérien"},
            new IncidentType { Name = "Eboulement"},
            new IncidentType { Name = "Invasion de serpent"},
            new IncidentType { Name = "Fuite de gaz"},
            new IncidentType { Name = "Manifestation"},
            new IncidentType { Name = "Braquage"},
            new IncidentType { Name = "Evasion d’un prisonnier"}
        };

        context.IncidentTypes.AddRange(incidentTypes);
        context.SaveChanges();
    }
}