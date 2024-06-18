using FitAIAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FitAIAPI.Infrastructure.Data.Configurations
{
    public class UserDetailConfiguration : IEntityTypeConfiguration<UserDetail>
    {
        
            public void Configure(EntityTypeBuilder<UserDetail> builder)
            {
            builder.Property(e => e.Id).IsRequired(true).ValueGeneratedOnAdd();
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Gender).IsRequired(true).HasMaxLength(50);
            builder.Property(e => e.Height).IsRequired(true);
            builder.Property(e=>e.FirstWeight).IsRequired(true).HasMaxLength(50);
            builder.Property(e => e.CurrentWeight).IsRequired(true);
            builder.Property(e => e.TargetWeight).IsRequired(true);
            builder.Property(e => e.DateOfBirth).IsRequired(true).HasColumnType("date");
            builder.Property(e => e.Goals).IsRequired(false);

            builder.Property(e => e.PreferredActivities).IsRequired(false);
            builder.Property(e => e.WorkoutFrequency).IsRequired(false);
            builder.Property(e => e.FocusAreas).IsRequired(false);
            builder.Property(e => e.BasalMetabolism).IsRequired(false);
            builder.Property(e => e.DailyKcalGoal).IsRequired(false);
            builder.Property(e => e.HealthProblem).IsRequired(false);



            builder.Property(e => e.IsDeleted).HasDefaultValue(false);

            builder.HasOne(e => e.User)
                   .WithOne(u => u.UserDetail)
                   .HasForeignKey<UserDetail>(e => e.UserId);
        }
        }
    }

