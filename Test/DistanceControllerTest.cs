using System;
using Api.Calculators;
using Api.Controllers;
using Api.Enums;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Test;

[TestClass]
public class DistanceControllerTest
{
    [TestMethod]
    public void WhenCalculate_WithValidParams_ReturnsResult()
    {
        // Arrange
        var mockDistanceCalculator = new Mock<IDistanceCalculator>();
        mockDistanceCalculator.Setup(calculator =>
            calculator.Calculate(It.IsAny<Coordinates>(), It.IsAny<Coordinates>(), It.IsAny<UnitEnum>()))
            .Returns(123);
        var controller = new DistanceController();
        var request = new CalculateRequest {
            Start = new Coordinates { Latitude = 1, Longitude = 1 },
            End = new Coordinates { Latitude = 1, Longitude = 1 },
            Unit = UnitEnum.Kilometres
        };

        // Act
        var result = controller.Calculate(request, mockDistanceCalculator.Object);

        // Assert
        Assert.IsInstanceOfType(result, typeof(ActionResult<CalculateResponse>));
        Assert.AreEqual(123, result.Value?.Distance);
        Assert.AreEqual(UnitEnum.Kilometres, result.Value?.Unit);
    }
}