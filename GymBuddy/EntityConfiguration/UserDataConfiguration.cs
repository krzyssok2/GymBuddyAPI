using GymBuddyAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymBuddyAPI.EntityConfiguration
{
    public class UserDataConfiguration : IEntityTypeConfiguration<UserData>
    {
        public void Configure(EntityTypeBuilder<UserData> builder)
        {
            builder.HasMany(i => i.Workouts)
                .WithOne(i => i.UserData);
            builder.HasMany(i => i.WeightAudit)
                .WithOne(i => i.User);
        }
    }
}