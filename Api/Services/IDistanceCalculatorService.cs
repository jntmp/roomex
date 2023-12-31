using Api.Models;

namespace Api.Services;

public interface IDistanceCalculatorService
{
    double CalculateDistance(Coordinates start, Coordinates end);
}
