using Microsoft.AspNetCore.Identity;
using GymBuddyAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WeatherForecastAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExerciseController : ControllerBase
    {
        public ExerciseController()
        {
        }
        [HttpGet("")]
        public ActionResult<AllExercises> GetAllExercises()
        {
            return Ok();
        }
        [HttpPost("")]
        public ActionResult<AllExercises> PutAllExercises(AllExercises allExercises)
        {
            return Ok();
        }
        [HttpDelete("{Exercise name}")]
        public ActionResult<AllExercises> DeleteExercise (string exerciseName)
        {
            return Ok();
        }
    }
}