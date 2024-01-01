using Api;
using Api.Calculators;
using Api.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Api.Services.Interfaces;
using Api.Models;

namespace Test;

[TestClass]
public class DistanceCalculatorTest
{
    private readonly IDistanceCalculator _distanceCalculator;

    public DistanceCalculatorTest()
    {
        _distanceCalculator = new DistanceCalculator();
    }

    [TestMethod]
    public void WhenCalculate_WithValidParams_ReturnsResult()
    {
				var latLongPair = new LatLongPairDto {
					Start = new LatLongDto() { Latitude = 1, Longitude = 1 },
					End = new LatLongDto() { Latitude = 5, Longitude = 5 }
				};
        
        var distance = _distanceCalculator.Calculate(latLongPair);

        Assert.IsTrue(distance > 0);
    }

    [TestMethod]
    public void WhenCalculate_WithSameStartAndEnd_ReturnsZero()
    {
			var latLongPair = new LatLongPairDto {
        Start = new LatLongDto() { Latitude = 1, Longitude = 1 },
        End = new LatLongDto() { Latitude = 1, Longitude = 1 }
			};
        
				var distance = _distanceCalculator.Calculate(latLongPair);

        Assert.AreEqual(0, distance);
    }

}