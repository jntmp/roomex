using Api.Types;
using Api.Models;
using System.Globalization;
using Api.Services.Interfaces;

namespace Api.Calculators;

public class DistanceCalculator : IDistanceCalculator
{
	private const double EarthRadius = 6371.0;

	public double Calculate(LatLongPairDto request)
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

	private LatLongDto DegreesToRadians(LatLongDto LatLongDto)
	{
		return new LatLongDto
		{
			Latitude = LatLongDto.Latitude * Math.PI / 180.0,
			Longitude = LatLongDto.Longitude * Math.PI / 180.0
		};
	}
}