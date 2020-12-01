using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymBuddyAPI.Entities
{
    public class Rep
    {
        public long Id { get; set; }
        public int Weight { get; set; }
        public ExerciseSet ExerciseSet { get; set; }
    }
}
