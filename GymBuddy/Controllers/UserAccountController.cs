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

            var UserData = _context.UserData.Where(i => i.User == userName).First();
            var result = _context.Workouts
                .Where(i => i.UserData == UserData)
                .Include(i=>i.Exercises).ThenInclude(i=>i.Sets).ThenInclude(i=>i.AllReps)
                .ToList();

            AllWorkouts allworkouts = new AllWorkouts
            {
                Workouts = result.Select(i => new GymBuddyAPI.Models.WorkoutModel()
                {
                    Name = i.WorkoutName,
                    Exercises = i.Exercises.Select(j => new ExerciseModel()
                    {
                        ExerciseName = j.Name,
                        Type= j.ExerciseType,
                        Sets = j.Sets.Select(k => new SetModel()
                        {
                            Reps = k.AllReps.Select(l => new RepModel()
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
                .Select(i => new WorkoutModel
                {
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
                .Where(i => i.Creator == userName).Include(i=>i.Sets).ThenInclude(i=>i.AllReps)
                .ToList();


            var allExercises = new AllExercises()
            {
                Exercises=exercises.Select(i=> new ExerciseModel()
                {
                    Id=i.Id,
                    ExerciseName=i.Name,
                    Type=i.ExerciseType,
                    Sets=i.Sets.Select(j=>new SetModel
                    {
                        Reps=j.AllReps.Select(k=> new RepModel()
                        {
                            Weights=k.Weight
                        }).ToList()
                    }).ToList()
                }).ToList()
            };

            return Ok(allExercises);
        }
        [HttpPut("exercises/{id}")]
        public ActionResult<ExerciseModel> PutExercise(long id, ExerciseModel exercise)
        {
            var userName = User.Claims.Single(a => a.Type == ClaimTypes.NameIdentifier).Value;
            var neededExercise = _context.Exercises.AsNoTracking().First(i => i.Id == id);
            _context.Remove(neededExercise);

            var Updateexercise = new Exercise()
            {
                Id = neededExercise.Id,
                ExerciseType = exercise.Type,
                Creator = userName,
                Name = exercise.ExerciseName,
                Workouts=neededExercise.Workouts,
                Sets = exercise.Sets.Select(i => new ExerciseSet
                {
                    AllReps = i.Reps.Select(j => new Rep
                    {
                        Weight = j.Weights
                    }).ToList()
                }).ToList()
            };
            _context.SaveChanges();
            return Ok(exercise);
        }
        [HttpPost("exercises")]
        public ActionResult<ExerciseModel> PostExercise(ExerciseModel exercise)
        {
            var userName = User.Claims.Single(a => a.Type == ClaimTypes.NameIdentifier).Value;

            var newExercise = new GymBuddyAPI.Entities.Exercise()
            {
                Creator = userName,
                ExerciseType = exercise.Type,
                Name= exercise.ExerciseName
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
