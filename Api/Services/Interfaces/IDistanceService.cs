using Api.Models;

namespace Api.Services.Interfaces;

public interface IDistanceService
{
	CalculateDistanceResponse CalculateDistance(LatLongPairDto request, string locale);
}
