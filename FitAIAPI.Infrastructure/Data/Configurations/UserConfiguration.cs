using FitAIAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitAIAPI.Infrastructure.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(e => e.Id).IsRequired(true).ValueGeneratedOnAdd();
            builder.HasKey(e => e.Id);

            builder.Property(e => e.FirstName).IsRequired(false).HasMaxLength(50);
            builder.Property(e => e.LastName).IsRequired(false).HasMaxLength(50);
            builder.Property(e => e.UserName).IsRequired(true).HasMaxLength(50);
            builder.HasIndex(e => e.UserName).IsUnique(true);
            builder.Property(e => e.Password).IsRequired(true).HasMaxLength(250);
            builder.Property(e => e.Email).IsRequired(true).HasMaxLength(50);
            builder.HasIndex(e => e.Email).IsUnique(true);
            builder.Property(e => e.Role).IsRequired(true).HasMaxLength(50).HasDefaultValue("User");
            builder.Property(e => e.IsDeleted).HasDefaultValue(false);
            builder.Property(e => e.ProfilePictureUrl).HasMaxLength(250);

            builder.Property(e => e.LastLogin)
                .HasConversion(v => v, v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : (DateTime?)null);
            builder.Property(e => e.CreatedOn).HasDefaultValueSql("getdate()").HasConversion(v => v, v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : (DateTime?)null);
            builder.Property(e => e.UpdatedOn)
                .HasConversion(v => v, v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : (DateTime?)null);
            builder.Property(e => e.DeletedOn)
                .HasConversion(v => v, v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : (DateTime?)null);

            builder.HasQueryFilter(e => !e.IsDeleted);
            builder.Property(e => e.IsFirstLogin).HasDefaultValue(true);

            // Navigation properties configuration
            builder.HasOne(e => e.UserDetail)
                .WithOne(d => d.User)
                .HasForeignKey<UserDetail>(d => d.UserId);

            builder.HasMany(e => e.UserWorkoutPlans)
                .WithOne(wp => wp.User)
                .HasForeignKey(wp => wp.UserId);

            builder.HasMany(e => e.NutritionPlans)
                .WithOne(np => np.User)
                .HasForeignKey(np => np.UserId);

            builder.HasMany(e => e.FoodEntries)
                .WithOne(fe => fe.User)
                .HasForeignKey(fe => fe.UserId);
        }
    }
}
