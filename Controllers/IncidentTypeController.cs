using Microsoft.AspNetCore.Mvc;
using SuperCesiApi.Services;

namespace SuperCesiApi.Controllers;

/// <summary>
/// Controller for managing incident types.
/// </summary>
[ApiController]
[Route("api/incidenttype")]
public class IncidentTypeController : ControllerBase
{
    private readonly IncidentTypeService _incidentTypeService;

    /// <summary>
    /// Initializes a new instance of the <see cref="IncidentTypeController"/> class.
    /// </summary>
    /// <param name="incidentTypeService">The incident type service.</param>
    public IncidentTypeController(IncidentTypeService incidentTypeService)
    {
        _incidentTypeService = incidentTypeService;
    }

    /// <summary>
    /// Retrieves all incident types.
    /// </summary>
    /// <returns>A collection of incident types.</returns>
    [HttpPost("getall")]
    public async Task<IActionResult> GetAllIncident()
    {
        var result = await _incidentTypeService.GetAllIncidentTypes();

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
}