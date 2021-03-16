using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymBuddyAPI.Models.RequestModels
{
    public class CreateWorkout
    {
        public string Name { get; set; }
    }

    public class RequestExerciseModel
    {
        public string ExerciseName { get; set; }
        public ExerciseType Type { get; set; }
        public List<RequestSetModel> Sets { get; set; }
    }
    public class RequestSetModel
    {
        public int RepCount { get; set; }
        public int Weights { get; set; }
    }


}
