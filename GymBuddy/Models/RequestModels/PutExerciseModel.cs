using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymBuddyAPI.Models.RequestModels
{
    public class PutExerciseModel
    {
        public string ExerciseName { get; set; }
        public ExerciseType Type { get; set; }
        public List<PutSetModel> Sets { get; set; }
    }
    public class PutSetModel
    {
        public long Id { get; set; }
        public int RepCount { get; set; }
        public int Weights { get; set; }
    }
}
