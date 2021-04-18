using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymBuddyAPI.Entities
{
    public class WeighChanges
    {
        public long Id { get; set; }
        public int OldWeight { get; set; }
        public int NewWeight { get; set; }
        public DateTime ChangeDate { get; set; }
        public UserData User { get; set; }
    }
}
