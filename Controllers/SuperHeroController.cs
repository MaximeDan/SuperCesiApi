using Microsoft.AspNetCore.Mvc;
using SuperCesiApi.Models;
using SuperCesiApi.Services;

namespace SuperCesiApi.Controllers;


/// <summary>
/// Controller responsible for handling superhero-related operations.
/// </summary>
[ApiController]
[Route("api/superhero")]
public class SuperHeroController : ControllerBase
{
    private readonly SuperHeroService _superHeroService;

    /// <summary>
    /// Initializes a new instance of the <see cref="SuperHeroController"/> class.
    /// </summary>
    /// <param name="superHeroService">The service used for superhero operations.</param>
    public SuperHeroController(SuperHeroService superHeroService)
    {
        _superHeroService = superHeroService;
    }

    /// <summary>
    /// Registers a new superhero in the system.
    /// </summary>
    /// <param name="superHero">The superhero object containing the details of the new superhero.</param>
    /// <returns>An IActionResult representing the result of the operation.</returns>
    [HttpPost("register")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> RegisterSuperHero(SuperHero superHero)
    {
        var result = await _superHeroService.CreateAsync(superHero);

        if (result.StatusCode == StatusCodes.Status201Created)
        {
            return Ok();
        }
        
        return BadRequest(result.Message);
    }
    
    
    /// <summary>
    /// Retrieves all superheroes from the system.
    /// </summary>
    /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
    [HttpPost("getall")]
    public async Task<IActionResult> GetAllSuperHero()
    {
        var result = await _superHeroService.GetAllSuperHero();

        if (result.StatusCode == StatusCodes.Status200OK)
        {
            return Ok(result.Object);
        }
        
        return BadRequest(result.Message);
    }
    
}