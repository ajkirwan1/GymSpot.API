using Microsoft.AspNetCore.Mvc;

namespace GymSpot.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseItemController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllExercises()
        {
            string[] exercises = new string[] { "Chin-up", "Pull-up", "Benchpress" };
            return Ok(exercises);
        }
    }
}
