using System;
using System.Collections.Generic;

namespace GymBuddyAPI.Models
{
    public class AllExercises
    {
        public List<ExerciseModel> Exercises { get; set; }
    }

    public class ExerciseModel
    {
        public long Id { get; set; } 
        public string ExerciseName { get; set; }
        public ExerciseType Type {get;set;}
        public List<SetModel> Sets { get; set; }
    }
    public class SetModel
    {
        public long Id { get; set; }
        public int RepCount { get; set; }
        public int Weights { get; set; }
    }
}
