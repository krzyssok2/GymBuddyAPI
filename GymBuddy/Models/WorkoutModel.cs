using GymBuddy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymBuddyAPI.Models
{
    public class AllWorkouts
    {
        public List<WorkoutModel> Workouts { get; set; }
    }
    public class WorkoutModel
    {
        public string Name { get; set; }
        public List<ExerciseModel> Exercises {get;set;}
    }
}
