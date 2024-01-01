using Api.Enums;
using Api.Models;

namespace Api.Calculators;

public class DistanceCalculator : IDistanceCalculator
{
	private Dictionary<UnitEnum, double> EarthRadiusByUnit => new Dictionary<UnitEnum, double> {
	 	{ UnitEnum.Kilometres, 6371.0 },
		{ UnitEnum.Miles, 3958.75587 }
	};

    public double Calculate(Coordinates start, Coordinates end, UnitEnum unit)
    {
		// Convert degrees to radians
		var startRadians = DegreesToRadians(start);
        var endRadians = DegreesToRadians(end);

        // Haversine formula
        var dLat = endRadians.Latitude - startRadians.Latitude;
        var dLon = endRadians.Longitude - startRadians.Longitude;

        var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(startRadians.Latitude) * Math.Cos(endRadians.Latitude) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

		// use distance unit based on provided param
        var distance = EarthRadiusByUnit[unit] * c;

		return distance;
    }

	private Coordinates DegreesToRadians(Coordinates coordinates)
    {
		return new Coordinates
		{
			Latitude = coordinates.Latitude * Math.PI / 180.0,
			Longitude = coordinates.Longitude * Math.PI / 180.0
		};
    }
}