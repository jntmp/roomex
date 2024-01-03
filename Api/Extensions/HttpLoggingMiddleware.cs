namespace Api.Extensions;

public class HttpLoggingMiddleware : BaseMiddleware<HttpLoggingMiddleware>
{
	public HttpLoggingMiddleware(RequestDelegate next, ILogger<HttpLoggingMiddleware> logger) 
		: base(next, logger)
	{
	}

	public async Task Invoke(HttpContext context)
	{
		var originalBodyStream = context.Response.Body;

		using var responseBody = new MemoryStream();
		
		context.Response.Body = responseBody;

		await _next(context);

		responseBody.Seek(0, SeekOrigin.Begin);

		using var streamReader = new StreamReader(responseBody);
		var responseText = streamReader.ReadToEnd();

		_logger.LogInformation($"Request: {context.Request.Path + context.Request.QueryString}");
		_logger.LogInformation($"Response: {responseText}");

		responseBody.Seek(0, SeekOrigin.Begin);
		await responseBody.CopyToAsync(originalBodyStream);

	}
}
