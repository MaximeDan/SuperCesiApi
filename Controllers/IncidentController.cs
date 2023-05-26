using Microsoft.AspNetCore.Mvc;
using SuperCesiApi.Models;
using SuperCesiApi.Services;

namespace SuperCesiApi.Controllers;

/// <summary>
/// Controller responsible for handling incident-related operations.
/// </summary>
[ApiController]
[Route("api/incident")]
public class IncidentController : Controller
{
    private readonly IncidentService _incidentService;

    /// <summary>
    /// Initializes a new instance of the <see cref="IncidentController"/> class.
    /// </summary>
    /// <param name="incidentService">The service used for incident operations.</param>
    public IncidentController(IncidentService incidentService)
    {
        _incidentService = incidentService;
    }


    [HttpPost("getall")]
    public async Task<IActionResult> GetAllIncident()
    {
        var result = await _incidentService.GetAllIncident();

        if (result.StatusCode == StatusCodes.Status200OK)
        {
            return Ok(result.Object);
        }
        
        if (result.StatusCode == StatusCodes.Status204NoContent)
        {
            return NoContent();
        }
        
        // Return a BadRequestObjectResult with the error message
        return BadRequest(result.Message);
    }
    
    [HttpPost("get/{id}")]
    public async Task<IActionResult> GetIncident(int id)
    {
        var result = await _incidentService.GetIncident(id);
        if (result.StatusCode == StatusCodes.Status200OK)
        {
            // Return the incident object
            return Ok(result.Object);
        }

        if (result.StatusCode == StatusCodes.Status204NoContent)
        {
            // Return a 204 No Content response when the incident is not found
            return NoContent();
        }
        
        // Return a BadRequestObjectResult with the error message
        return BadRequest(result.Message);
    }

    /// <summary>
    /// Creates a new incident in the system.
    /// </summary>
    /// <param name="incident">The incident object containing the details of the new incident.</param>
    /// <returns>An IActionResult representing the result of the operation.</returns>
    [HttpPost("create")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CreateIncident(Incident incident)
    {
        var result = await _incidentService.CreateIncident(incident);

        if (result.StatusCode == StatusCodes.Status201Created)
        {
            return Ok();
        }

        return BadRequest(result.Message);
    }
}