using GymBuddyAPI.Entities;
using GymBuddyAPI.EntityConfiguration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GymBuddyAPI
{
    public class GymBuddyContext : IdentityDbContext<IdentityUser>
    {
        public GymBuddyContext(DbContextOptions options) : base(options) { }

        public DbSet<UserData> UserData { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<UserSchedule> Schedules { get; set; }
        public DbSet<ExerciseSet> ExericseSets { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserDataConfiguration());
            builder.ApplyConfiguration(new WorkoutsConfiguration());
            builder.ApplyConfiguration(new ExerciseConfiguration());
        }
    }
}
