using Api.Models;

namespace Api.Services.Interfaces;

public interface IDistanceCalculator
{
    double Calculate(LatLongPairDto request);
}
