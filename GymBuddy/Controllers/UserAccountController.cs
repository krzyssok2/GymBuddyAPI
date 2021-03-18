using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GymBuddyAPI;
using GymBuddyAPI.Entities;
using GymBuddyAPI.Models;
using GymBuddyAPI.Models.RequestModels;
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

       
        [HttpGet("workouts")]
        public ActionResult<AllWorkouts> GetAllWorkouts()
        {
            var userName = User.Claims.Single(a => a.Type == ClaimTypes.NameIdentifier).Value;
            var exercises = _context.Exercises.Where(i => i.Creator == userName || i.Creator == "").ToList();
            if (exercises == null) return NotFound("Exercises not found");

            var UserData = _context.UserData.Where(i => i.User == userName).First();

            var result = _context.Workouts
                .Where(i => i.UserData == UserData)
                .Include(i=>i.Exercises).ThenInclude(i=>i.Sets)
                .ToList();
            if (result == null) return NotFound("Workouts not found");

            AllWorkouts allworkouts = new AllWorkouts
            {
                Workouts = result.Select(i => new GymBuddyAPI.Models.WorkoutModel()
                {
                    Id=i.Id,
                    Name = i.WorkoutName,
                    Exercises = i.Exercises.Select(j => new ExerciseModel()
                    {
                        Id=j.Id,
                        ExerciseName = j.Name,
                        Type= j.ExerciseType,
                        Sets = j.Sets.Select(k => new SetModel()
                        {
                            Id=k.Id,
                            RepCount= k.RepCount,
                            Weights=k.Weight
                        }).ToList()
                    }).ToList()
                }).ToList()
            };

            return Ok(allworkouts);
        }

        [HttpGet("workouts/{id}")]
        public ActionResult<AllWorkouts> GetAllWorkouts(long id)
        {
            var userName = User.Claims.Single(a => a.Type == ClaimTypes.NameIdentifier).Value;
            var workout = _context.Workouts
                .Include(i=>i.Exercises).ThenInclude(i=>i.Sets)
                .Where(i => i.Id==id&&i.UserData.User==userName).First();
            if (workout == null) return NotFound("workout not found");


            WorkoutModel responseWorkout = new WorkoutModel
            {
                Id=workout.Id,
                Name=workout.WorkoutName,
                Exercises= workout.Exercises.Select(i=> new ExerciseModel
                {
                    ExerciseName=i.Name,
                    Id=i.Id,
                    Type=i.ExerciseType,
                    Sets=i.Sets.Select(j=> new SetModel
                    {
                        Id=j.Id,
                        RepCount=j.RepCount,
                        Weights=j.Weight
                    }).ToList()
                }).ToList()
            };

            return Ok(responseWorkout);
        }



        [HttpPut("workouts")]
        public ActionResult<Workout> PutAllWorkouts(UpdateWorkout updateWorkout)
        {
            var userName = User.Claims.Single(a => a.Type == ClaimTypes.NameIdentifier).Value;
            var currentUsser = _context.UserData.First(i => i.User == userName);

            var value = _context.Workouts
                .Where(i => i.UserData.User == userName && i.Id == updateWorkout.id)
                .Include(i=>i.Exercises)
                .ThenInclude(i=>i.Sets).First();
            if (value == null) return NotFound("Not found");


            value.WorkoutName = updateWorkout.name;
            _context.SaveChanges();

            return Ok();
        }



        [HttpPost("workouts")]
        public ActionResult<GymBuddyAPI.Models.WorkoutModel> PostWorkout(CreateWorkout workout)
        {
            var userName = User.Claims.Single(a => a.Type == ClaimTypes.NameIdentifier).Value;
            var currentUsser = _context.UserData.First(i => i.User == userName);
            var insertValue = new GymBuddyAPI.Entities.Workout()
            {
                WorkoutName = workout.Name,
                UserData= currentUsser
            };

            var value = _context.Workouts.Add(insertValue);
            _context.SaveChanges();
            return Ok(workout);
        }

        [HttpDelete("workouts/{id}")]
        public ActionResult<AllWorkouts> DeleteWorkout(long id)
        {
            var userName = User.Claims.Single(a => a.Type == ClaimTypes.NameIdentifier).Value;

            var value = _context.Workouts.First(i => i.UserData.User == userName && i.Id==id);
            if (value == null) return NotFound("Workout not found");

            var delete = _context.Workouts.Remove(value);

            var databaseFunction = _context.Workouts.Include(i => i.UserData).Select(i => i.UserData.User == userName).ToList();

            _context.SaveChanges();

            var result2 = new AllWorkouts()
            {
                Workouts = _context.Workouts
                .Include(i => i.UserData)
                .Where(i => i.UserData.User == userName)
                .Select(i => new WorkoutModel
                {
                    Id=i.Id,
                    Name = i.WorkoutName,
                }).ToList()
            };

            return Ok(result2);
        }




        
        [HttpGet("exercises")]
        public ActionResult<AllExercises> GetAllExercises()
        {
            var userName = User.Claims.Single(a => a.Type == ClaimTypes.NameIdentifier).Value;

            var exercises = _context.Exercises
                .Where(i => i.Creator == userName).Include(i=>i.Sets)
                .ToList();

            if (exercises == null) return NotFound("Exercises were not found");


            var allExercises = new AllExercises()
            {
                Exercises=exercises.Select(i=> new ExerciseModel()
                {
                    Id=i.Id,
                    ExerciseName=i.Name,
                    Type=i.ExerciseType,
                    Sets=i.Sets.Select(j=>new SetModel
                    {
                        Id=j.Id,
                        RepCount=j.RepCount,
                        Weights=j.Weight,
                    }).ToList()
                }).ToList()
            };

            return Ok(allExercises);
        }

        [HttpGet("exercises/{id}")]
        public ActionResult<AllExercises> GetExercises(long id)
        {
            var userName = User.Claims.Single(a => a.Type == ClaimTypes.NameIdentifier).Value;

            var exercise = _context.Exercises
                .Where(i => i.Creator == userName&&i.Id==id).Include(i => i.Sets)
                .First();

            if (exercise == null) return NotFound("Exercise was not found");


            var exerciseResponse = new ExerciseModel()
            {
                ExerciseName=exercise.Name,
                Id=exercise.Id,
                Type=exercise.ExerciseType,
                Sets= exercise.Sets.Select(i=> new SetModel
                {
                    Id=i.Id,
                    RepCount=i.RepCount,
                    Weights=i.Weight
                }).ToList()
            };

            return Ok(exerciseResponse);
        }

        [HttpPut("exercises/{id}")]
        public ActionResult<ExerciseModel> PutExercise(long id, PutExerciseModel exercise)
        {
            var userName = User.Claims.Single(a => a.Type == ClaimTypes.NameIdentifier).Value;
            var neededExercise = _context.Exercises.Include(i=>i.Sets).First(i => i.Id == id);
            if (neededExercise == null) return NotFound("Exercise not found");

            neededExercise.ExerciseType = exercise.Type;
            neededExercise.Name = exercise.ExerciseName;
            var sets = neededExercise.Sets.ToList();
            foreach(var set in sets)
            {
                neededExercise.Sets.Remove(set);
            }

            
            foreach(var set in exercise.Sets)
            {
                if(set.Id==0)
                {
                    neededExercise.Sets.Add(new ExerciseSet
                    {
                        RepCount = set.RepCount,
                        Weight = set.Weights
                    });
                }
                else
                {
                    neededExercise.Sets.Add(new ExerciseSet
                    {
                        Id=set.Id,
                        RepCount = set.RepCount,
                        Weight = set.Weights
                    });
                }
                
            }    
            _context.SaveChanges();
            var returnExercise = _context.Exercises.Include(i => i.Sets).First(i => i.Id == id);
            var returnExerciseTransformed = new ExerciseModel
            {
                Id = returnExercise.Id,
                ExerciseName = returnExercise.Name,
                Type = returnExercise.ExerciseType,
                Sets = returnExercise.Sets.Select(i => new SetModel
                {
                    Id = i.Id,
                    RepCount = i.RepCount,
                    Weights = i.Weight
                }).ToList()
            };
            return Ok(returnExerciseTransformed);
        }
        [HttpPost("exercises")]
        public ActionResult<ExerciseModel> PostExercise(RequestExerciseModel exercise)
        {
            var userName = User.Claims.Single(a => a.Type == ClaimTypes.NameIdentifier).Value;

            var newExercise = new GymBuddyAPI.Entities.Exercise()
            {
                Creator = userName,
                ExerciseType = exercise.Type,
                Name= exercise.ExerciseName,
                Sets= exercise.Sets.Select(i=> new ExerciseSet
                {
                    RepCount=i.RepCount,
                    Weight=i.Weights
                }).ToList()              
                
            };

            var add = _context.Exercises.Add(newExercise);

            _context.SaveChanges();

            return Ok(newExercise);
        }        
        [HttpDelete("exercises/{id}")]
        public ActionResult<AllExercises> DeleteExercise(long id)
        {
            var userName = User.Claims.Single(a => a.Type == ClaimTypes.NameIdentifier).Value;
            
            var exercise= _context.Exercises.First(i=>i.Creator==userName&&i.Id==id);
            if (exercise == null) return NotFound("Exercise was not found");
            var delete = _context.Exercises.Remove(exercise);

            _context.SaveChanges();

            var result = _context.Exercises.Select(i => i.Creator == userName).ToList();

            return Ok("Deleted");
        }
        [HttpPut("execises/assign")]
        public IActionResult AssingExerciseToWorkout(long workoutId, long exerciseId)
        {
            var userName = User.Claims.Single(a => a.Type == ClaimTypes.NameIdentifier).Value;

            var workout = _context.Workouts.Include(i=>i.Exercises).ThenInclude(i=>i.Sets).Where(i => i.Id == workoutId && i.UserData.User == userName).First();
            if (workout == null) return NotFound("Workout not found");

            var exercise = _context.Exercises.Include(i=>i.Sets).Where(i => i.Id == exerciseId && i.Creator == userName).First();
            if (exercise == null) return NotFound("Exercise not found");

            workout.Exercises.Add(exercise);
            _context.SaveChanges();

            return Ok("Assigned");
        }
    }
}
