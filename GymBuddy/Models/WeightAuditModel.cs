using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymBuddyAPI.Models
{
    public class WeightAuditModel
    {
        public List<WeightChanges> WeightChanges { get; set; }
    }

    public class WeightChanges
    {
        public int OldWeight { get; set; }
        public int NewWeight { get; set; }
        public DateTime ChangeTime { get; set; }
    }
}
