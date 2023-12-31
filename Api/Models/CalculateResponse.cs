using Api.Enums;

namespace Api.Models;

public class CalculateResponse
{
	public static CalculateResponse Map(double distance, UnitEnum unit)
	{
		return new CalculateResponse {
			Distance = distance,
			Unit = unit
		};
	}

	public double Distance {get; private set;}
	public UnitEnum Unit {get; private set;}
}