using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymBuddyAPI.Entities
{
    public class Workouts
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string WorkoutName { get; set; }
        public UserData UserData { get; set; }
        public ICollection<Exercises> Exercises { get; set; }
    }
}
