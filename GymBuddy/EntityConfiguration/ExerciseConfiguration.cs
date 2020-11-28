﻿using GymBuddyAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymBuddyAPI.EntityConfiguration
{
    public class ExerciseConfiguration : IEntityTypeConfiguration<Exercises>
    {
        public void Configure(EntityTypeBuilder<Exercises> builder)
        {
            builder.HasMany(i => i.Sets)
                .WithOne(i => i.Exercise);
            builder.Property(i => i.Creator).IsRequired(false);
        }
    }
}
