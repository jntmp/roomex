using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using Api;
using Api.Models;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Test.Integrations;

/// <summary>
/// Some integration tests to run the Api and check request validation (model binding)
/// A few sample tests, not thorough
/// </summary>
[TestClass]
public class DistanceControllerTest
{
		private HttpClient _client;

		[TestInitialize]
		public void Initialize()
		{
				var factory = new WebApplicationFactory<Program>();
				_client = factory.CreateClient();
		}

    [TestMethod]
    public void WhenCalculate_WithNoParams_Returns400()
    {
        var response = _client.GetAsync($"/distance").Result;

        Assert.IsFalse(response.IsSuccessStatusCode);
    }

		[TestMethod]
    public void WhenCalculate_WithValidParams_Returns200WithValue()
    {
        var request = new CalculateDistanceRequest {
            Start = new LatLongDto { Latitude = 1, Longitude = 1 },
            End = new LatLongDto { Latitude = 2, Longitude = 2 },
            Locale = "en-US"
        };

        var response = _client.GetAsync($"/distance?{BuildQuery(request)}").Result;

        Assert.IsTrue(response.IsSuccessStatusCode);
				Assert.IsInstanceOfType(ReadJsonResponse<CalculateDistanceResponse>(response.Content), typeof(CalculateDistanceResponse));
    }

		[TestMethod]
		public void WhenCalculate_WithInValidParams_Returns400WithError()
    {
        var request = new CalculateDistanceRequest {
            Start = new LatLongDto { Latitude = 91, Longitude = 1 },
            End = new LatLongDto { Latitude = 2, Longitude = 2 },
            Locale = "en-US"
        };

        var response = _client.GetAsync($"/distance?{BuildQuery(request)}").Result;

        Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
				
				var result = ReadJsonResponse<ProblemDetails>(response.Content);
				// Has Errors
				Assert.IsNotNull(result.Extensions.FirstOrDefault(e => e.Key == "errors"));
    }


		private T ReadJsonResponse<T>(HttpContent content)
		{
			var options = new JsonSerializerOptions
			{
					Converters = { new JsonStringEnumConverter() },
					PropertyNameCaseInsensitive = true
			};
			return content.ReadFromJsonAsync<T>(options).Result;
		}

		private string BuildQuery(CalculateDistanceRequest request)
		{
				return @$"Start.Latitude={request.Start.Latitude}&" +
             $"Start.Longitude={request.Start.Longitude}&" +
             $"End.Latitude={request.End.Latitude}&" +
             $"End.Longitude={request.End.Longitude}&" +
						 $"Locale={request.Locale}";
		}
}