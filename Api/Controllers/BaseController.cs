using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class BaseController<T> : ControllerBase
{
	protected readonly ILogger<T> _logger;

	public BaseController(ILogger<T> logger)
	{
			_logger = logger;
	}

}
