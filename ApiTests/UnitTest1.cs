using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using GymBuddyAPI.Services;
using GymBuddyAPI.Entities;
using GymBuddyAPI.Models;
using System.Security.Claims;

namespace ApiTests
{
    [TestClass]
    public class UnitTest1
    {
        UserDataService userDataService = new UserDataService();
        //[Fact]
        //public void TestMethod1()
        //{
        //    //Arrange-arrange what i use
            
        //    List<Workout> WorkoutList = new List<Workout>
        //    {
        //        new Workout
        //        {
        //            Id=1,
        //            UserData=null,
        //            WorkoutName="name1",
        //            Exercises= new List<Exercise>
        //            {
        //                new Exercise
        //                {
        //                    Creator="Admin",
        //                    ExerciseType=GymBuddyAPI.Models.ExerciseType.Abs,
        //                    Id=5,
        //                    Name="asd",
        //                    Sets= new List<ExerciseSet>
        //                    {

        //                    }                           
        //                }
        //            }

        //        }
        //    };

        //    AllWorkouts allWorkouts = new AllWorkouts
        //    {
        //        Workouts = new List<WorkoutModel>
        //        {
        //            new WorkoutModel
        //            {
        //                Id=1,
        //                Name="name1",
        //                Exercises= new List<ExerciseModel>
        //                {
        //                    new ExerciseModel
        //                    {
        //                        Id=5,
        //                        ExerciseName="asd",
        //                        Type=ExerciseType.Abs,
        //                        Sets= new List<SetModel>
        //                        {

        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    };

        //    //Act - the call of the method

        //    var answer = userDataService.GetWorkoutTransformation(WorkoutList);

        //    //Assert - assert values from act
        //    Xunit.Assert.True(allWorkouts.Equals(answer));
        //}

        [Fact]
        public void TestGettingUserClaim()
        {
            //Arrange-arrange what i use
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                                        new Claim(ClaimTypes.NameIdentifier, "admin@gmail.com"),
                                        new Claim(ClaimTypes.Name, "gunnar@somecompany.com")
                                   }, "TestAuthentication"));

            var userName = user.Claims.Single(a => a.Type == ClaimTypes.NameIdentifier).Value;

            //Act - the call of the method

            var nickname= userDataService.IdentifyUserClaim(user);


            //Assert - assert values from act
            Xunit.Assert.Equal("admin@gmail.com", nickname);
        }

        [Fact]
        public void TestClaimFake ()
        {
            //Arrange-arrange what i use
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {                                        
                                        new Claim(ClaimTypes.Name, "gunnar@somecompany.com")
                                   }, "TestAuthentication"));


            var ex = Xunit.Assert.Throws<InvalidOperationException>(() => userDataService.IdentifyUserClaim(user));

            //Assert - assert values from act
            Xunit.Assert.Equal("Sequence contains no matching element", ex.Message);
        }

    }
}
