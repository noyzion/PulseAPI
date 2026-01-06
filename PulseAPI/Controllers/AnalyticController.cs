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

        [HttpGet("global")]
        public async Task<IActionResult> GetGlobalAnalytics()
        {
           return Ok(await _service.GetGlobalStatsAsync());
        }
        [HttpGet("users/activity")]
        public async Task<IActionResult> GetUsersActivityLevel()
        {
            return Ok(await _service.GetUsersActivityAsync());
        }
        [HttpGet("users/top/{limit}")]
        public async Task<IActionResult> GetNTopUser(int limit)
        {
            return Ok(await _service.GetNTopUsersAsync(limit));
        }
    }
}
