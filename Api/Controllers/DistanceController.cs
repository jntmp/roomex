using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class DistanceController : ControllerBase
{
    private readonly ILogger<DistanceController> _logger;
    private IDistanceCalculatorService _distanceCalculatorService;

    public DistanceController(ILogger<DistanceController> logger, IDistanceCalculatorService distanceCalculatorService)
    {
        _logger = logger;
        _distanceCalculatorService = distanceCalculatorService;
    }

    [HttpGet(Name = "CalculateDistance")]
    public ActionResult<double> Get([FromQuery] Coordinates start, [FromQuery] Coordinates end)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var distance = _distanceCalculatorService.CalculateDistance(start, end);

        return Ok(distance);
    }

}
