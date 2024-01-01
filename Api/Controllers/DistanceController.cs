using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Api.Services;
using AutoMapper;
using Api.Services.Interfaces;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class DistanceController : ControllerBase
{
	private readonly IDistanceService _distanceService;
	private readonly IMapper _mapper;
	
	public DistanceController(IMapper mapper, DistanceService distanceService)
	{
		_distanceService = distanceService;
		_mapper = mapper;
	}

	[HttpGet]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(CalculateDistanceResponse), StatusCodes.Status200OK)]
	public ActionResult<CalculateDistanceResponse> Calculate([FromQuery] CalculateDistanceRequest request)
	{
			var LatLongPairDto = _mapper.Map<LatLongPairDto>(request);

			var distanceServiceResponse = _distanceService.CalculateDistance(LatLongPairDto, request.Locale);

			return Ok(distanceServiceResponse);
	}
}
