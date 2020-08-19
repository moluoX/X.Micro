using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace X.Micro.Service.ServiceA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        private ILogger<HealthCheckController> _logger;
        public HealthCheckController(ILogger<HealthCheckController> logger) => _logger = logger;

        [HttpGet]
        public IActionResult Index()
        {
            _logger.LogInformation("[HealthCheck]");
            return Ok();
        }
    }
}
