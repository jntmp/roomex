using System.ComponentModel.DataAnnotations;
using Api.Enums;

namespace Api.Models;

public class CalculateRequest
{
	[Required]
	public Coordinates Start {get;set;}
	[Required]
	public Coordinates End {get;set;}
	[Required]
	public UnitEnum Unit {get;set;}
}