using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Ss.CmbXft.Api.Infrastructure.Common;

[ApiController]
[Route("api/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    protected readonly ILogger _logger;

    protected ApiControllerBase(ILogger logger)
    {
        _logger = logger;
    }

    protected IActionResult Success<T>(T data, string message = "Success")
    {
        return Ok(new { code = 0, data = data, message = message });
    }

    protected IActionResult Failure(string message = "Failure", string code = "ERROR")
    {
        return BadRequest(new { code = code, message = message });
    }
}
