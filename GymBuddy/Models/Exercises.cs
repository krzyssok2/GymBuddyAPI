using System;
using System.Collections.Generic;

namespace GymBuddyAPI.Models
{
    public class AllExercises
    {
        public List<Exercise> Exercises { get; set; }
    }

    public class Exercise
    {
        public string ExerciseName { get; set; }
        public ExerciseType Type {get;set;}
        public List<Set> Sets { get; set; }
    }
    public class Set
    {
        public List<Rep> Reps;
    }
    public class Rep
    {
        public int Weights { get; set; }
    }
}
