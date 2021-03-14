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

namespace GymBuddyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class Schedule : ControllerBase
    {
        private readonly GymBuddyContext _context;
        public Schedule(GymBuddyContext context)
        {
            _context = context;
        }
        [HttpGet("today")]
        public ActionResult<Models.WorkoutModel> GetTodaysWorkout()
        {
            var userName = User.Claims.Single(a => a.Type == ClaimTypes.NameIdentifier).Value;

            var Schedule = _context.UserData
                .Include(i => i.Workouts).ThenInclude(i => i.Exercises).ThenInclude(i => i.Sets)
                .Include(i => i.UserSchedule)
                .Where(i => i.User == userName)
                .First().UserSchedule;
            if (Schedule == null) return NotFound("Schedule not found");

            string day = DateTime.Now.DayOfWeek.ToString();

            var result = new Entities.Workout();
            switch (day)
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

            var answer = new Models.WorkoutModel()
            {
                Id = result.Id,
                Name = result.WorkoutName,
                Exercises = result.Exercises.Select(i => new ExerciseModel()
                {
                    Id=i.Id,
                    ExerciseName = i.Name,
                    Type = i.ExerciseType,
                    Sets = i.Sets.Select(j => new SetModel()
                    {
                        Weights=j.Weight,
                        RepCount=j.RepCount
                    }).ToList()
                }).ToList()
            };
            return Ok(answer);
        }

        [HttpGet("{day}")]
        public ActionResult<Models.WorkoutModel> GetWorkoutByDay(int day)
        {
            var userName = User.Claims.Single(a => a.Type == ClaimTypes.NameIdentifier).Value;

            var Schedule = _context.UserData
                .Include(i => i.Workouts).ThenInclude(i => i.Exercises).ThenInclude(i => i.Sets)
                .Include(i => i.UserSchedule)
                .Where(i => i.User == userName)
                .First().UserSchedule;
            if (Schedule == null) return NotFound("Schedule not found");
            var result = new Entities.Workout();
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
                default:
                    return BadRequest("Such day doesnt exist, value should be between 1-7");
                    break;
            }

            var answer = new Models.WorkoutModel()
            {
                Id=result.Id,
                Name = result.WorkoutName,
                Exercises = result.Exercises.Select(i => new ExerciseModel()
                {
                    Id=i.Id,
                    ExerciseName = i.Name,
                    Type = i.ExerciseType,
                    Sets = i.Sets.Select(j => new SetModel()
                    {
                        Weights=j.Weight,
                        RepCount=j.RepCount
                    }).ToList()
                }).ToList()
            };
            return Ok(answer);
        }
        [HttpDelete("{day}")]
        public ActionResult<Models.WorkoutModel> DeleteWorkoutFromDay(int day)
        {
            var userName = User.Claims.Single(a => a.Type == ClaimTypes.NameIdentifier).Value;

            var value = _context.UserData.Include(i=>i.UserSchedule).Include(i=>i.Workouts).Where(i => i.User == userName).First().UserSchedule;
            if (value == null) return NotFound("Schedule not found");

            var result = new Entities.Workout();
            switch (day)
            {
                case 1:
                    result = _context.Schedules.Where(i => i.Id == value.Id).First().Monday = null;
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

        [HttpPut("{day}")]
        public ActionResult<UserSchedule> PutWorkoutFromDay(int day, string workoutName)
        {
            var userName = User.Claims.Single(a => a.Type == ClaimTypes.NameIdentifier).Value;
            var value = _context.UserData
                .Include(i=>i.UserSchedule)
                .Include(i => i.Workouts)
                .First(i => i.User == userName)
                .UserSchedule;
            if (value == null) return NotFound("Schedule not found");

            switch (day)
            {
                case 1:
                    value.Monday = _context.Workouts.Include(i => i.UserData).First(i => i.WorkoutName == workoutName && i.UserData.User == userName);
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

    }
}
