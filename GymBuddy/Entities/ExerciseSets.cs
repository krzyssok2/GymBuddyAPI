using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymBuddyAPI.Entities
{
    public class ExerciseSets
    {
        public long Id { get; set; }
        public int Reps { get; set; }
        public double Weight { get; set; }
        public Exercises Exercise { get; set; }
    }
}
