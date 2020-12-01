using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GymBuddyAPI.Entities
{
    public class UserSchedule
    {
        public long Id { get; set; }
        public Workout Monday { get; set; }
        public Workout Tuesday { get; set; }
        public Workout Wednesday { get; set; }
        public Workout Thursday { get; set; }
        public Workout Friday { get; set; }
        public Workout Saturday { get; set; }
        public Workout Sunday { get; set; }
    }
}
