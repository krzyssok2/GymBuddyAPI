using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GymBuddyAPI;
using GymBuddyAPI.Entities;
using GymBuddyAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GymBuddy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserAccountController : ControllerBase
    {

        private readonly GymBuddyContext _context;
        public UserAccountController(GymBuddyContext context)
        {
            _context = context;
        }

        [HttpGet("workouts/today")]
        public ActionResult<Workout> GetTodaysWorkout()
        {
            var userName = User.Claims.Single(a => a.Type == ClaimTypes.NameIdentifier).Value;

            var Schedule = _context.UserData
                .Include(i=>i.Workouts).ThenInclude(i=>i.Exercises).ThenInclude(i=>i.Sets).ThenInclude(i=>i.AllReps)
                .Include(i=>i.UserSchedule)
                .Where(i => i.User == userName)
                .First().UserSchedule;

            string day = DateTime.Now.DayOfWeek.ToString();

            var result = new Workouts();
            switch(day)
            {
                case "Monday":
                    result = Schedule.Monday;
                    break;
                case "Tuesday":
                    result = Schedule.Tuesday;
                    break;
                case "Wednesday":
                    result = Schedule.Wednesday;
                    break;
                case "Thursday":
                    result = Schedule.Thursday;
                    break;
                case "Friday":
                    result = Schedule.Friday;
                    break;
                case "Saturday":
                    result = Schedule.Saturday;
                    break;
                case "Sunday":
                    result = Schedule.Sunday;
                    break;
            }

            var answer = new Workout()
            {
                Name = result.WorkoutName,
                Exercises = result.Exercises.Select(i => new Exercise()
                {
                    ExerciseName = i.Name,
                    Type = i.ExerciseType,
                    Sets = i.Sets.Select(j => new Set()
                    {
                        Reps = j.AllReps.Select(k => new Rep()
                        {
                            Weights = k.Weight
                        }).ToList()
                    }).ToList()
                }).ToList()
            };
            return Ok(answer);
        }
        [HttpGet("workouts")]
        public ActionResult<AllWorkouts> GetAllWorkouts()
        {
            var userName = User.Claims.Single(a => a.Type == ClaimTypes.NameIdentifier).Value;
            var exercises = _context.Exercises.Where(i => i.Creator == userName || i.Creator == "").ToList();

            var UserData = _context.UserData.Where(i => i.User == userName).First();
            var result = _context.Workouts
                .Where(i => i.UserData == UserData)
                .Include(i=>i.Exercises).ThenInclude(i=>i.Sets).ThenInclude(i=>i.AllReps)
                .ToList();

            AllWorkouts allworkouts = new AllWorkouts
            {
                Workouts = result.Select(i => new Workout()
                {
                    Name = i.WorkoutName,
                    Exercises = i.Exercises.Select(j => new Exercise()
                    {
                        ExerciseName = j.Name,
                        Type=j.ExerciseType,
                        Sets = j.Sets.Select(k => new Set()
                        {
                            Reps = k.AllReps.Select(l => new Rep()
                            {
                                Weights = l.Weight
                            }).ToList()
                        }).ToList()
                    }).ToList()
                }).ToList()
            };

            return Ok(allworkouts);
        }
        
        
        
        [HttpPut("workouts")]
        public ActionResult<AllWorkouts> PutAllWorkouts()
        {
            var userName = User.Claims.Single(a => a.Type == ClaimTypes.NameIdentifier).Value;
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
            var userName = User.Claims.Single(a => a.Type == ClaimTypes.NameIdentifier).Value;

            var Schedule = _context.UserData
                .Include(i => i.Workouts).ThenInclude(i => i.Exercises).ThenInclude(i => i.Sets).ThenInclude(i => i.AllReps)
                .Include(i => i.UserSchedule)
                .Where(i => i.User == userName)
                .First().UserSchedule;

            var result = new Workouts();
            switch (day)
            {
                case 1:
                    result = Schedule.Monday;
                    break;
                case 2:
                    result = Schedule.Tuesday;
                    break;
                case 3:
                    result = Schedule.Wednesday;
                    break;
                case 4:
                    result = Schedule.Thursday;
                    break;
                case 5:
                    result = Schedule.Friday;
                    break;
                case 6:
                    result = Schedule.Saturday;
                    break;
                case 7:
                    result = Schedule.Sunday;
                    break;
            }

            var answer = new Workout()
            {
                Name = result.WorkoutName,
                Exercises = result.Exercises.Select(i => new Exercise()
                {
                    ExerciseName = i.Name,
                    Type = i.ExerciseType,
                    Sets = i.Sets.Select(j => new Set()
                    {
                        Reps = j.AllReps.Select(k => new Rep()
                        {
                            Weights = k.Weight
                        }).ToList()
                    }).ToList()
                }).ToList()
            };
            return Ok(answer);
        }
        [HttpDelete("workouts/{day}")]
        public ActionResult<Workout> DeleteWorkoutFromDay(int day)
        {
            var userName = User.Claims.Single(a => a.Type == ClaimTypes.NameIdentifier).Value;

            var value = _context.UserData.Where(i => i.User == userName).First().UserSchedule;

            var result = new Workouts();
            switch (day)
            {
                case 1:
                    result = _context.Schedules.Where(i=>i.Id==value.Id).First().Monday=null;
                    break;
                case 2:
                    result = _context.Schedules.Where(i => i.Id == value.Id).First().Tuesday = null;
                    break;
                case 3:
                    result = _context.Schedules.Where(i => i.Id == value.Id).First().Wednesday = null;
                    break;
                case 4:
                    result = _context.Schedules.Where(i => i.Id == value.Id).First().Thursday = null;
                    break;
                case 5:
                    result = _context.Schedules.Where(i => i.Id == value.Id).First().Friday = null;
                    break;
                case 6:
                    result = _context.Schedules.Where(i => i.Id == value.Id).First().Saturday = null;
                    break;
                case 7:
                    result = _context.Schedules.Where(i => i.Id == value.Id).First().Sunday = null;
                    break;
            }
            _context.SaveChanges();
            return Ok();
        
        }

        [HttpDelete("workouts/{name}")]
        public ActionResult<AllWorkouts> DeleteWorkout(string name)
        {
            var userName = User.Claims.Single(a => a.Type == ClaimTypes.NameIdentifier).Value;

            var value = _context.Workouts.First(i => i.UserData.User == userName && i.WorkoutName == name);

            var delete = _context.Workouts.Remove(value);

            var databaseFunction = _context.Workouts.Include(i => i.UserData).Select(i => i.UserData.User == userName).ToList();

            _context.SaveChanges();

            var result2 = new AllWorkouts()
            {
                Workouts = _context.Workouts
                .Include(i => i.UserData)
                .Where(i => i.UserData.User == userName)
                .Select(i => new Workout
                {
                    Name = i.WorkoutName,
                }).ToList()
            };

            return Ok(result2);
        }
        [HttpPut("workouts/{day}")]
        public ActionResult<UserSchedules> PutWorkoutFromDay(int day, string workoutName)
        {
            var userName = User.Claims.Single(a => a.Type == ClaimTypes.NameIdentifier).Value;
            var value = _context.UserData
                .First(i => i.User == userName)
                .UserSchedule;

            switch(day)
            {
                case 1:
                    value.Monday = _context.Workouts.Include(i=>i.UserData).First(i => i.WorkoutName == workoutName && i.UserData.User == userName);
                    break;
                case 2:
                    value.Tuesday = _context.Workouts.Include(i => i.UserData).First(i => i.WorkoutName == workoutName && i.UserData.User == userName);
                    break;
                case 3:
                    value.Wednesday = _context.Workouts.Include(i => i.UserData).First(i => i.WorkoutName == workoutName && i.UserData.User == userName);
                    break;
                case 4:
                    value.Thursday = _context.Workouts.Include(i => i.UserData).First(i => i.WorkoutName == workoutName && i.UserData.User == userName);
                    break;
                case 5:
                    value.Friday = _context.Workouts.Include(i => i.UserData).First(i => i.WorkoutName == workoutName && i.UserData.User == userName);
                    break;
                case 6:
                    value.Saturday = _context.Workouts.Include(i => i.UserData).First(i => i.WorkoutName == workoutName && i.UserData.User == userName);
                    break;
                case 7:
                    value.Sunday = _context.Workouts.Include(i => i.UserData).First(i => i.WorkoutName == workoutName && i.UserData.User == userName);
                    break;
            }
            _context.SaveChanges();
            return Ok(value);
        }

        [HttpGet("exercises")]
        public ActionResult<AllExercises> GetAllExercises()
        {
            var userName = User.Claims.Single(a => a.Type == ClaimTypes.NameIdentifier).Value;

            var exercises = _context.Exercises
                .Where(i => i.Creator == userName).Include(i=>i.Sets).ThenInclude(i=>i.AllReps)
                .ToList();


            var allExercises = new AllExercises()
            {
                Exercises=exercises.Select(i=> new Exercise()
                {
                    ExerciseName=i.Name,
                    Type=i.ExerciseType,
                    Sets=i.Sets.Select(j=>new Set
                    {
                        Reps=j.AllReps.Select(k=> new Rep()
                        {
                            Weights=k.Weight
                        }).ToList()
                    }).ToList()
                }).ToList()
            };

            return Ok(allExercises);
        }
        [HttpPut("exercises")]
        public ActionResult<AllExercises> PutAllExercises()
        {
            AllExercises allExercises = new AllExercises();

            return Ok(allExercises);
        }
        [HttpPost("exercises")]
        public ActionResult<Exercise> PostExercise(Exercise exercise)
        {
            var userName = User.Claims.Single(a => a.Type == ClaimTypes.NameIdentifier).Value;

            var newExercise = new Exercises()
            {
                Creator = userName,
                ExerciseType = exercise.Type,
                Name=exercise.ExerciseName
            };

            var add = _context.Exercises.Add(newExercise);

            _context.SaveChanges();

            return Ok(newExercise);
        }        
        [HttpDelete("exercises/{ExerciseName}")]
        public ActionResult<AllExercises> DeleteExercise(string ExerciseName)
        {
            var userName = User.Claims.Single(a => a.Type == ClaimTypes.NameIdentifier).Value;
            
            var exercise= _context.Exercises.First(i=>i.Creator==userName&&i.Name.ToLower()==ExerciseName.ToLower());
            if (exercise == null) return Ok("Failed");
            var delete = _context.Exercises.Remove(exercise);

            _context.SaveChanges();

            var result = _context.Exercises.Select(i => i.Creator == userName).ToList();

            return Ok("Deleted");
        }
    }
}
