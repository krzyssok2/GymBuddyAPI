using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GymBuddyAPI.Entities
{
    public class UserSchedules
    {
        public long Id { get; set; }
        public Workouts Monday { get; set; }
        public Workouts Tuesday { get; set; }
        public Workouts Wednesday { get; set; }
        public Workouts Thursday { get; set; }
        public Workouts Friday { get; set; }
        public Workouts Saturday { get; set; }
        public Workouts Sunday { get; set; }
    }
}
