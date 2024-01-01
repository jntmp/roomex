using System.ComponentModel.DataAnnotations;
using Api.Types;

namespace Api.Models;

public class CalculateDistanceRequest
{
	[Required]
	public LatLongDto Start {get;set;}
	[Required]
	public LatLongDto End {get;set;}
	[RegularExpression("^(en-US|en-GB|en-ZA)$", ErrorMessage = "Invalid value. Allowed values are 'en-US', en-GB', or 'en-ZA'.")]
	public string Locale {get;set;} = "en-US";

}