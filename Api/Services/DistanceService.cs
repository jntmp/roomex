using Api.Models;
using System.Globalization;
using AutoMapper;
using Api.Services.Interfaces;

namespace Api.Services;

public class DistanceService : IDistanceService
{
	private readonly IDistanceCalculator _distanceCalculator;
	private readonly ILocaleService _localeService;
	private readonly IMapper _mapper;

	public DistanceService(IMapper mapper, IDistanceCalculator distanceCalculator, ILocaleService localeService)
	{
		_distanceCalculator = distanceCalculator;
		_localeService = localeService;
		_mapper = mapper;
	}

	public CalculateDistanceResponse CalculateDistance(LatLongPairDto request, string locale)
	{
		double distance = _distanceCalculator.Calculate(request);

		var converted = _localeService.ConvertDistance(distance, locale);

		var response = _mapper.Map<CalculateDistanceResponse>(converted); 
		response.DisplayDistance = _localeService.NumberFormat(converted.Distance, new CultureInfo(locale));

		return response;
	}

	
}