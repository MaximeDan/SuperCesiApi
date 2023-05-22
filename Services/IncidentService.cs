using Microsoft.EntityFrameworkCore;
using SuperCesiApi.Data;
using SuperCesiApi.Models;

namespace SuperCesiApi.Services;

/// <summary>
/// Service class for handling Incident-related operations.
/// </summary>
public class IncidentService
{
    private readonly SuperCesiApiDbContext _dbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="IncidentService"/> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    public IncidentService(SuperCesiApiDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    

    /// <summary>
    /// Creates a new incident in the database.
    /// </summary>
    /// <param name="incident">The incident object containing the details of the new incident.</param>
    /// <returns>An ActionResponse indicating the outcome of the operation.</returns>
    public async Task<ActionResponse> CreateIncident(Incident incident)
    {
        var existingIncident = await _dbContext.Incidents.FirstOrDefaultAsync(i => i.Id == incident.Id);

        if (existingIncident is null)
        {
            return new ActionResponse(StatusCodes.Status409Conflict, "An incident has already been opened");
        }

        var newIncident = new Incident
        {
            City = incident.City,
            Longitude = incident.Longitude,
            Latitude = incident.Latitude,
            IncidentTypeId = incident.IncidentTypeId,
            IsResolved = incident.IsResolved
        };

        _dbContext.Incidents.Add(newIncident);

        await _dbContext.SaveChangesAsync();
        return new ActionResponse(StatusCodes.Status201Created);
    }

    /// <summary>
    /// Retrieves all incidents.
    /// </summary>
    /// <returns>A collection of incidents.</returns>
    public async Task<ICollection<Incident>> GetAllIncident()
    {
        return _dbContext.Incidents.ToList();
    }

    /// <summary>
    /// Retrieves an incident by ID.
    /// </summary>
    /// <param name="id">The ID of the incident.</param>
    /// <returns>The incident if found, or null if not found.</returns>
    public async Task<ActionResponse> GetIncident(int id)
    {
        var incident = await _dbContext.Incidents.FindAsync(id);

        if (incident == null)
        {
            
            return new ActionResponse(StatusCodes.Status204NoContent);
        }

        return new ActionResponse(StatusCodes.Status200OK, incident);
    }
}