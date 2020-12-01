using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymBuddyAPI.Entities
{
    public class ExerciseSet
    {
        public long Id { get; set; }
        public ICollection<Rep> AllReps { get; set; }
        public Exercise Exercise { get; set; }
    }
}
