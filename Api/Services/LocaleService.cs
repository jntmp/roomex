using System.Globalization;
using Api.Models;
using Api.Types;
using Api.Services.Interfaces;

namespace Api.Services;

/// <summary>
/// A utility helper to handle locale specific formatting 
/// </summary>
public class LocaleService : ILocaleService
{
		public string NumberFormat(double distance, CultureInfo cultureInfo)
		{
			return distance.ToString("N2", cultureInfo);
		}

		public DistanceWithUnitDto ConvertDistance(double distance, string locale)
		{
			switch(locale)
			{
				case "en-US":
				case "en-GB":
					return new DistanceWithUnitDto {
						Distance = distance * 0.62,
						Unit = UnitEnum.Miles
					};
				default:
					return new DistanceWithUnitDto {
						Distance = distance, 
						Unit = UnitEnum.Kilometres
					};
			}
		}
}