using Microsoft.AspNetCore.Mvc;

namespace GymSpot.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return Ok();
        }
    }
}
