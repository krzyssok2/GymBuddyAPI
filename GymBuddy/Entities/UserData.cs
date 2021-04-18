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
        public ICollection<Workout> Workouts { get; set; }
        public UserSchedule UserSchedule { get; set; }
        public int CurrentWeight { get; set; }
        public ICollection<WeighChanges> WeightAudit { get; set; }
    }
}
