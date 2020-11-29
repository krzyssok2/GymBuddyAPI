using GymBuddyAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymBuddyAPI.Entities
{
    public class UserData
    {
        public long Id { get; set; }
        public string User { get; set; }
        public ICollection<Workouts> Workouts { get; set; }
        public UserSchedules UserSchedule { get; set; }
    }
}
