using Microsoft.EntityFrameworkCore;
using SuperCesiApi.Data;
using SuperCesiApi.Models;

namespace SuperCesiApi.Services;

/// <summary>
/// Service class for handling SuperHero-related operations.
/// </summary>
public class SuperHeroService
{
    private readonly SuperCesiApiDbContext _dbContext;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="SuperHeroService"/> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    public SuperHeroService(SuperCesiApiDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    /// <summary>
    /// Creates a new SuperHero.
    /// </summary>
    /// <param name="superHero">The SuperHero object to create.</param>
    /// <returns>An <see cref="ActionResponse"/> indicating the result of the operation.</returns>
    public async Task<ActionResponse> CreateAsync(SuperHero superHero)
    {
        var existingSuperHero = await _dbContext.SuperHeroes.FirstOrDefaultAsync(s => s.Name == superHero.Name);

        if (existingSuperHero != null)
        {
            return new ActionResponse(StatusCodes.Status409Conflict, "Superhero with the same name already exists.");        
        }

        // Create a new superhero object
        var newSuperHero = new SuperHero
        {
            Name = superHero.Name,
            Latitude = superHero.Latitude,
            Longitude = superHero.Longitude,
            PhoneNumber = superHero.PhoneNumber,
            IncidentTypes = new List<IncidentType>() 
        };

        foreach (var incidentType in superHero.IncidentTypes)
        {
            var incidentTypeDb = await _dbContext.IncidentTypes.FindAsync(incidentType.Id);
            if (incidentTypeDb != null)
            {
                newSuperHero.IncidentTypes.Add(incidentTypeDb);
            }
        }
        
        // Add the new superhero to the database
        _dbContext.SuperHeroes.Add(newSuperHero);

        // Save the changes to the database
        await _dbContext.SaveChangesAsync();
        
        return new ActionResponse(StatusCodes.Status201Created);
    }

    /// <summary>
    /// Updates an existing SuperHero.
    /// </summary>
    /// <param name="superHero">The SuperHero object with updated data.</param>
    /// <returns>An <see cref="ActionResponse"/> indicating the result of the operation.</returns>
    public async Task<ActionResponse> UpdateSuperHero(SuperHero superHero)
    {
        var existingSuperHero = await _dbContext.SuperHeroes.FirstOrDefaultAsync(s => s.Name == superHero.Name);

        if (existingSuperHero is null)
        {
            return new ActionResponse(StatusCodes.Status400BadRequest, "No Super Hero with this name exists");
        }
        
        // Update the properties of the existing superhero only if the new values are not empty or null
        if (!string.IsNullOrEmpty(superHero.Name))
        {
            existingSuperHero.Name = superHero.Name;
        }

        if (superHero.Latitude != 0)
        {
            existingSuperHero.Latitude = superHero.Latitude;
        }

        if (superHero.Longitude != 0)
        {
            existingSuperHero.Longitude = superHero.Longitude;
        }

        if (!string.IsNullOrEmpty(superHero.PhoneNumber))
        {
            existingSuperHero.PhoneNumber = superHero.PhoneNumber;
        }

        if (superHero.IncidentTypes.Count > 0)
        {
            existingSuperHero.IncidentTypes = superHero.IncidentTypes;
        }

        // Save the changes to the database
        await _dbContext.SaveChangesAsync();
        
        return new ActionResponse(StatusCodes.Status200OK, "Super Hero updated successfully");
    }
}