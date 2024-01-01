using System;
using Api.Calculators;
using Api.Enums;
using Api.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test;

[TestClass]
public class CalculatorTest
{
    private readonly IDistanceCalculator _distanceCalculator;

    public CalculatorTest()
    {
        _distanceCalculator = new DistanceCalculator();
    }

    [TestMethod]
    public void WhenCalculate_WithValidParams_ReturnsResult()
    {
        var start = new Coordinates() { Latitude = 1, Longitude = 1 };
        var end = new Coordinates() { Latitude = 5, Longitude = 5 };
        
        var distance = _distanceCalculator.Calculate(start, end, Api.Enums.UnitEnum.Kilometres);

        Assert.IsTrue(distance > 0);
    }

    [TestMethod]
    public void WhenCalculate_WithSameStartAndEnd_ReturnsZero()
    {
        var start = new Coordinates() { Latitude = 1, Longitude = 1 };
        var end = new Coordinates() { Latitude = 1, Longitude = 1 };
        
        var distance = _distanceCalculator.Calculate(start, end, Api.Enums.UnitEnum.Kilometres);

        Assert.AreEqual(0, distance);
    }

    [TestMethod]
    public void WhenCalculate_WithDifferentUnit_ReturnsUnitResult()
    {
        var start = new Coordinates() { Latitude = 1, Longitude = 1 };
        var end = new Coordinates() { Latitude = 5, Longitude = 5 };
        
        var distanceKm = _distanceCalculator.Calculate(start, end, UnitEnum.Kilometres);
        var distanceMiles = _distanceCalculator.Calculate(start, end, UnitEnum.Miles);

        Assert.IsTrue(distanceKm > distanceMiles);
    }
}