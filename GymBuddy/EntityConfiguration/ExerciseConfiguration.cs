using GymBuddyAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymBuddyAPI.EntityConfiguration
{
    public class ExerciseConfiguration : IEntityTypeConfiguration<Exercise>
    {
        public void Configure(EntityTypeBuilder<Exercise> builder)
        {
            builder.HasMany(i => i.Sets)
                .WithOne(i => i.Exercise).OnDelete(DeleteBehavior.Cascade);
            builder.Property(i => i.Creator).IsRequired(false);
        }
    }
}
