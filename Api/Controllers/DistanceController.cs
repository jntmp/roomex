using System.Net.Mime;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class DistanceController : ControllerBase
{
    private readonly ILogger<DistanceController> _logger;

    public DistanceController(ILogger<DistanceController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(CalculateResponse), StatusCodes.Status200OK)]
    public ActionResult<CalculateResponse> Calculate([FromQuery] CalculateRequest request, 
			[FromServices] IDistanceCalculatorService distanceCalculatorService)
    {
        if (!ModelState.IsValid)
        {
						_logger.LogError("{method} request failed with invalid parameters {request}", nameof(Calculate), request);
            return BadRequest(ModelState);
        }

        var distance = distanceCalculatorService.CalculateDistance(request.Start, request.End);

				var response = new CalculateResponse
				{
					Distance = distance,
					Unit = "km"
				};

        return Ok(response);
    }

}
