using System.Globalization;
using Api.Models;

namespace Api.Services.Interfaces;

public interface ILocaleService
{
	string NumberFormat(double distance, CultureInfo cultureInfo);
	DistanceWithUnitDto ConvertDistance(double distance, string locale);
}
