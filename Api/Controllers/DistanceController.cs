using Api.Models;
using Api.Calculators;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class DistanceController : ControllerBase
{
	[HttpGet]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(CalculateResponse), StatusCodes.Status200OK)]
	public ActionResult<CalculateResponse> Calculate([FromQuery] CalculateRequest request, 
		[FromServices] IDistanceCalculator distanceCalculator)
	{
        double distance = distanceCalculator.Calculate(request.Start, request.End, request.Unit);

        return Ok(CalculateResponse.Map(distance, request.Unit));
	}
}
