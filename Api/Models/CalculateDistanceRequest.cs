using System.ComponentModel.DataAnnotations;
using Api.Types;

namespace Api.Models;

public class CalculateDistanceRequest : IValidatableObject
{
	[Required]
	public LatLongDto Start {get;set;}
	
	[Required]
	public LatLongDto End {get;set;}

	// possible security concern having regex parameter validation.
	// if I had more time I would move these locales to a static list, read from config 
	[RegularExpression("^(en-US|en-GB|en-ZA)$", ErrorMessage = "Invalid value. Allowed values are 'en-US', en-GB', or 'en-ZA'.")]
	public string Locale {get;set;} = "en-US";

	// Usually I prefer annotation rules to define input boundaries (as above)
	// But here we can do custom validation to have more control and
	// move the responsibility to the request, so we can reuse a clean LatLongDto in the service layer
	public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
	{
		var results = new List<ValidationResult>();

		if (Start?.Latitude == null || End?.Latitude == null) 
		{
			results.Add(new ValidationResult("Latitude is required."));
		}

		if (Start?.Longitude == null || End?.Longitude == null) 
		{
			results.Add(new ValidationResult("Longitude is required."));
		}

		if (Start?.Latitude < -90 || Start?.Latitude > 90 || End?.Latitude < -90 || End?.Latitude > 90)
			results.Add(new ValidationResult("Latitude must be between -90 and 90."));

		if (Start?.Longitude < -180 || Start?.Longitude > 180 || End?.Longitude < -180 || End?.Longitude > 180)
			results.Add(new ValidationResult("Longitude must be between -180 and 180."));
		
		return results;
	}
}