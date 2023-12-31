using Api.Enums;
using Api.Models;

namespace Api.Calculators;

public interface IDistanceCalculator
{
    double Calculate(Coordinates start, Coordinates end, UnitEnum unit);
}
