using GymBuddyAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymBuddyAPI.EntityConfiguration
{
    public class WorkoutsConfiguration : IEntityTypeConfiguration<Workout>
    {
        public void Configure(EntityTypeBuilder<Workout> builder)
        {
            builder.HasMany(i => i.Exercises)
                .WithMany(i => i.Workouts);
        }
    }
}
