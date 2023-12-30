using Api.Models;

namespace Api.Services;

public interface IDistanceCalculatorService
{
    double CalculateDistance(Coordinates start, Coordinates end);
}

public class DistanceCalculatorService : IDistanceCalculatorService
{
    public double CalculateDistance(Coordinates start, Coordinates end)
    {
        return 123;
    }
}