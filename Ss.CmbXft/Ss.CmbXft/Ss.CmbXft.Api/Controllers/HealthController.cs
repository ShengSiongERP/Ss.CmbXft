using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ss.CmbXft.Api.Infrastructure.Common;
using Ss.CmbXft.Common.Models;

namespace Ss.CmbXft.Api.Controllers;

public class HealthController : ApiControllerBase
{
    public HealthController(ILogger<HealthController> logger) : base(logger)
    {
    }

    [HttpGet]
    public ApiResult Get()
    {
        _logger.LogInformation("Health check performed.");
        return ApiResult.Success();
    }
}
