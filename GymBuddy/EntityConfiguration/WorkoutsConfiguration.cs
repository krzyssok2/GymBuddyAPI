using GymBuddyAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymBuddyAPI.EntityConfiguration
{
    public class WorkoutsConfiguration : IEntityTypeConfiguration<Workouts>
    {
        public void Configure(EntityTypeBuilder<Workouts> builder)
        {
            builder.HasMany(i => i.Exercises)
                .WithMany(i => i.Workouts);
        }
    }
}
