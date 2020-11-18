using GymBuddy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymBuddyAPI.Models
{
    public class AllWorkouts
    {
        public List<Workout> Workouts { get; set; }
    }
    public class Workout
    {
        public string Name { get; set; }
        public List<Exercise> Exercises {get;set;}
    }
}
