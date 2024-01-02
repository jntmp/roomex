using System.Net;
using System.Net.Mime;
using System.Text.Json;

namespace Api.Extensions;

public class ErrorHandlerMiddleware : BaseMiddleware<ErrorHandlerMiddleware>
{
	public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger) 
		: base(next, logger)
	{
	}

	public async Task Invoke(HttpContext context)
	{
		try
		{
			await _next(context);
		}
		catch (Exception error)
		{
			var response = context.Response;
			response.ContentType = MediaTypeNames.Application.Json;

			// possible to switch error type here and return different errors
			// for now, let's just handle "unhandled" errors
			response.StatusCode = (int)HttpStatusCode.InternalServerError;

			var result = JsonSerializer.Serialize(new { message = "An internal server has occurred." });
			await response.WriteAsync(result);
		}
	}
}