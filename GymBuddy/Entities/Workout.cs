using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymBuddyAPI.Entities
{
    public class Workout
    {
        public long Id { get; set; }
        public string WorkoutName { get; set; }
        public UserData UserData { get; set; }
        public ICollection<Exercise> Exercises { get; set; }
    }
}
