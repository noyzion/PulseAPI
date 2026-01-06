using Microsoft.AspNetCore.Mvc;
using PulseAPI.Services;
using System.Net;

namespace PulseAPI.Controllers
{
    [ApiController]
    [Route("api/analytic")]
    public class AnalyticController : ControllerBase
    {
        private readonly AnalyticsService _service;

        public AnalyticController(AnalyticsService service)
        {
            _service = service;
        }

        [HttpGet("/global")]
        public async Task<IActionResult> GetGlobalAnalytics()
        {
           return Ok(await _service.GetGlobalStatsAsync());
        }
    }
}
