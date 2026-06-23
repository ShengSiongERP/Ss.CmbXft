using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ss.CmbXft.Api.Infrastructure.Common;

namespace Ss.CmbXft.Api.Controllers;

public class HealthController : ApiControllerBase
{
    public HealthController(ILogger<HealthController> logger) : base(logger)
    {
    }

    [HttpGet]
    public IActionResult Get()
    {
        _logger.LogInformation("Health check performed.");
        return Success(new { Status = "Healthy", Timestamp = DateTime.UtcNow });
    }
}
