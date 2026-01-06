using Microsoft.AspNetCore.Mvc;
using PulseAPI.Services;

namespace PulseAPI.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly UsersService _service;

        public UsersController(UsersService service)
        {
            _service = service;
        }

        [HttpGet("{id}/summary")]
        public async Task<IActionResult> GetUserSummaryByIdAsync(int id)
        {
            var userData = await _service.GetUserSummaryAsync(id);

            if(userData == null)  return NotFound("User Not Found");

            return Ok(userData);
        }
    }
}
