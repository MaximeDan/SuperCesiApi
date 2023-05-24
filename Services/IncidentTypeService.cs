using SuperCesiApi.Data;

namespace SuperCesiApi.Services;

/// <summary>
/// Service class for handling IncidentType-related operations.
/// </summary>
public class IncidentTypeService
{
    private readonly SuperCesiApiDbContext _dbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="IncidentTypeService"/> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    public IncidentTypeService(SuperCesiApiDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    /// <summary>
    /// Retrieves all incidents.
    /// </summary>
    /// <returns>A collection of incidents.</returns>
    public async Task<ActionResponse> GetAllIncidentTypes()
    {
        var incidentTypes = _dbContext.IncidentTypes.ToList();
        if (incidentTypes.Count > 0)
        {
            return new ActionResponse(StatusCodes.Status200OK, incidentTypes);
        }
        
        return new ActionResponse(StatusCodes.Status204NoContent);
    }
}