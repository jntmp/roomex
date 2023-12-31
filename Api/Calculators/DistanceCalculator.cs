using Api.Enums;
using Api.Models;

namespace Api.Calculators;

public class DistanceCalculator : IDistanceCalculator
{
		private const double EarthDiameter = 456;

    public double Calculate(Coordinates start, Coordinates end, UnitEnum unit)
    {
			if (unit != UnitEnum.Kilometres) {
				// do conversion and return
			}
      
			// return original
			return 123;
    }

		private double Convert(double distance, UnitEnum unit)
		{
				switch (unit)
				{
					case UnitEnum.Kilometres:
					case UnitEnum.Miles:
					default:
						return distance;
				}
		}
}