using Api.Models;

namespace Api.Services.Interfaces;

public interface IDistanceService
{
	CalculateDistanceResponse CalculateDistance(GeoLocationRangeDto request, string locale);
}
