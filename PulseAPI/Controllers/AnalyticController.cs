using Microsoft.AspNetCore.Mvc;
using PulseAPI.Services;

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
           var analystic = await _service.GetGlobalStatsAsync();
            return Ok(analystic);
        }
    }
}
