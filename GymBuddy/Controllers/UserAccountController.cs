using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymBuddyAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GymBuddy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserAccountController : ControllerBase
    {


        public UserAccountController()
        {

        }

        [HttpGet("workouts/today")]
        public ActionResult<Workout> GetTodaysWorkout()
        {
            Workout workout = new Workout
            {
                Name = "test",
                Exercises = new List<Exercise>
                {
                    new Exercise
                    {
                        ExerciseName="test1",
                        Sets = new List<Set>
                        {
                            new Set
                            {
                                Reps= new List<Rep>
                                {
                                    new Rep
                                    {
                                        Weights=5
                                    },
                                    new Rep
                                    {
                                        Weights=6
                                    },
                                    new Rep
                                    {
                                        Weights=7
                                    }
                                }
                            }
                        }
                    }
                }
            };

            return Ok(workout);
        }
        [HttpGet("workouts")]
        public ActionResult<AllWorkouts> GetAllWorkouts()
        {
            AllWorkouts allworkouts = new AllWorkouts();

            return Ok(allworkouts);
        }
        [HttpPut("workouts")]
        public ActionResult<AllWorkouts> PutAllWorkouts()
        {
            AllWorkouts allworkouts = new AllWorkouts();

            return Ok(allworkouts);
        }
        [HttpPost("workouts")]
        public ActionResult<Workout> PostWorkout(Workout workout)
        {

            return Ok(workout);
        }
        [HttpGet("workouts/{day}")]
        public ActionResult<Workout> GetWorkoutByDay(int day)
        {
            Workout workout = new Workout();

            return Ok(workout);
        }
        [HttpDelete("workouts/{day}")]
        public ActionResult<Workout> DeleteWorkoutFromDay(int day)
        {
            Workout workout = new Workout();

            return Ok(workout);
        }
        [HttpDelete("workouts/{name}")]
        public ActionResult<AllWorkouts> DeleteWorkout(string name)
        {
            AllWorkouts allWorkouts = new AllWorkouts();

            return Ok(allWorkouts);
        }
        [HttpPut("workouts/{day}")]
        public ActionResult<Workout> PutWorkoutFromDay(int day, Workout workout)
        {
            Workout currentworkout = new Workout();

            return Ok(workout);
        }

        [HttpGet("exercises")]
        public ActionResult<AllExercises> GetAllExercises()
        {
            AllExercises allExercises = new AllExercises();

            return Ok(allExercises);
        }
        [HttpPut("exercises")]
        public ActionResult<AllExercises> PutAllExercises()
        {
            AllExercises allExercises = new AllExercises();

            return Ok(allExercises);
        }
        [HttpPost("exercises")]
        public ActionResult<AllExercises> PostExercise(Exercise exercise)
        {
            AllExercises allExercises = new AllExercises();

            return Ok(allExercises);
        }
        [HttpDelete("exercises/{ExerciseName}")]
        public ActionResult<AllExercises> DeleteExercise(string ExerciseName)
        {
            AllExercises allExercises = new AllExercises();

            return Ok(allExercises);
        }
    }
}
