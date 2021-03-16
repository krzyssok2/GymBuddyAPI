using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymBuddyAPI.Models
{
    public class ScheduleModel
    {
        public long Id { get; set; }
        public List<DayWorkout> WorkoutList { get; set; }
    }

    public class DayWorkout
    {
        public DaysEnum Day {get;set;}
        public long WorkoutId { get; set; }
    }
}
