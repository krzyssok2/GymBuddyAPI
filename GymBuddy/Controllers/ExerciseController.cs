using Microsoft.AspNetCore.Identity;
using GymBuddyAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using GymBuddyAPI;

namespace WeatherForecastAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExerciseController : ControllerBase
    {
        private readonly GymBuddyContext _context;
        public ExerciseController(GymBuddyContext context)
        {
            _context = context;
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