using Api.Models;
using Api.Services.Interfaces;

namespace Api.Services;

/// <summary>
/// A utility calculator to do the math calculation
/// </summary>
public class DistanceCalculator : IDistanceCalculator
{
	private const double EarthRadius = 6371.0;

	public double Calculate(GeoLocationRangeDto request)
	{
		// Convert degrees to radians
		var startRadians = DegreesToRadians(request.Start);
		var endRadians = DegreesToRadians(request.End);

		// Haversine formula
		var dLat = endRadians.Latitude - startRadians.Latitude;
		var dLon = endRadians.Longitude - startRadians.Longitude;

		var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
						Math.Cos(startRadians.Latitude) * Math.Cos(endRadians.Latitude) *
						Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

		var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

		var distance = EarthRadius * c;

		return distance;
	}

	private GeoLocationDto DegreesToRadians(GeoLocationDto GeoLocationDto)
	{
		return new GeoLocationDto
		{
			Latitude = GeoLocationDto.Latitude * Math.PI / 180.0,
			Longitude = GeoLocationDto.Longitude * Math.PI / 180.0
		};
	}
}