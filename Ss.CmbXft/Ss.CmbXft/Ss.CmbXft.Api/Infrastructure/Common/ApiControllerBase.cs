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
}
