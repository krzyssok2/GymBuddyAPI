using GymBuddyAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymBuddyAPI.Entities
{
    public class Exercises
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ExerciseType ExerciseType { get; set; }
        public string Creator { get; set; }
        public ICollection<ExerciseSets> Sets { get; set; }
        public ICollection<Workouts> Workouts { get; set; }
    }
}
