namespace Api.Types;

/// <summary>
/// A distance unit for locales
/// </summary>
public enum UnitEnum
{
	// Would be more efficient to use an integer base for requests, 
	// especially in high load scenarios
	Miles, 
	Kilometres
}