namespace Api.Extensions;

public abstract class BaseMiddleware<T>
{
	protected readonly RequestDelegate _next;
	protected readonly ILogger<T> _logger;

	public BaseMiddleware(RequestDelegate next, ILogger<T> logger)
	{
		_next = next;
		_logger = logger;
	}
}