﻿using GymBuddyAPI.Entities;
using GymBuddyAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GymBuddyAPI.Services
{
    public class UserDataService
    {
        public AllWorkouts GetWorkoutTransformation(List<Workout> result)
        {
            AllWorkouts allworkouts = new AllWorkouts
            {
                Workouts = result.Select(i => new GymBuddyAPI.Models.WorkoutModel()
                {
                    Id = i.Id,
                    Name = i.WorkoutName,
                    Exercises = i.Exercises.Select(j => new ExerciseModel()
                    {
                        Id = j.Id,
                        ExerciseName = j.Name,
                        Type = j.ExerciseType,
                        Sets = j.Sets.Select(k => new SetModel()
                        {
                            Id = k.Id,
                            RepCount = k.RepCount,
                            Weights = k.Weight
                        }).ToList()
                    }).ToList()
                }).ToList()
            };

            return allworkouts;
        }

        public string IdentifyUserClaim(ClaimsPrincipal user)
        {
            string username = user.Claims.Single(a => a.Type == ClaimTypes.NameIdentifier).Value;
            return username;
        }

        public AllExercises GetExercisesByClaim(List<Exercise> Exercises, string username)
        {
            var exercises = Exercises.Where(i => i.Creator == username).ToList();
            var allExercises = new AllExercises()
            {
                Exercises = exercises.Select(i => new ExerciseModel()
                {
                    Id = i.Id,
                    ExerciseName = i.Name,
                    Type = i.ExerciseType,
                    Sets = i.Sets.Select(j => new SetModel
                    {
                        Id = j.Id,
                        RepCount = j.RepCount,
                        Weights = j.Weight,
                    }).ToList()
                }).ToList()
            };
            return allExercises;
        }
    }
}