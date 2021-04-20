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

        [Fact]
        public void TestGettingUserClaim()
        {
            //Arrange-arrange what i use
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                                        new Claim(ClaimTypes.NameIdentifier, "admin@gmail.com"),
                                        new Claim(ClaimTypes.Name, "test@gmail.com")
                                   }, "TestAuthentication"));

            var WrongClaimUser = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                                        new Claim(ClaimTypes.Name, "test@gmail.com")
                                   }, "TestAuthentication"));

            //Act - the call of the method

            var nickname= userDataService.IdentifyUserClaim(user);

            var ex = Xunit.Assert.Throws<InvalidOperationException>(() => userDataService.IdentifyUserClaim(WrongClaimUser));


            //Assert - assert values from act

            //Corrent
            Xunit.Assert.Equal("admin@gmail.com", nickname);
            //Not Correct
            Xunit.Assert.NotEqual("test@gmail.com", nickname);
            //Error
            Xunit.Assert.Equal("Sequence contains no matching element", ex.Message);
        }

        [Fact]
        public void GetCreatedWorkoutClaimedData()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                                        new Claim(ClaimTypes.NameIdentifier, "admin@gmail.com"),
                                        new Claim(ClaimTypes.Name, "test@gmail.com")
                                   }, "TestAuthentication"));


            List<Exercise> Exercises = new List<Exercise>
            {
                new Exercise
                {
                    Creator="admin@gmail.com",
                    Name="Test1",
                    Sets= new List<ExerciseSet>
                    {

                    }
                },
                new Exercise
                {
                    Creator="aadmin@gmail.com",
                    Name="Test2",
                    Sets= new List<ExerciseSet>
                    {

                    }
                },
                new Exercise
                {
                    Creator="hahahahahahaha@gmail.com",
                    Name="Test3",
                    Sets= new List<ExerciseSet>
                    {

                    }
                },
                 new Exercise
                {
                    Creator="admin@gmail.com",
                    Name="Test4",
                    Sets= new List<ExerciseSet>
                    {

                    }
                },
            };

            var nickname = userDataService.IdentifyUserClaim(user);

            var result=userDataService.GetExercisesByClaim(Exercises, nickname);

            Xunit.Assert.Equal(2, result.Exercises.Count);
            //Not Correct
            Xunit.Assert.NotEqual(1, result.Exercises.Count);

        }

    }
}
