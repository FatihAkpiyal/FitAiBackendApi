using FitAIAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace FitAIAPI.Infrastructure.Data.Context
{
    public class FitAIDbContext : DbContext
    {

        public FitAIDbContext(DbContextOptions options) : base(options) 
        { }


        public DbSet<User> Users { get; set; }
        public DbSet<UserWorkoutPlan> UserWorkoutPlans { get; set; }
        public DbSet<UserWorkoutDay> UserWorkoutDays { get; set; }
        public DbSet<NutritionPlan> NutritionPlans { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<FoodEntry> FoodEntries { get; set; }

       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasOne(u => u.UserDetail) // UserDetails ile ilişki kurduk
                .WithOne(ud => ud.User)
                .HasForeignKey<UserDetail>(ud => ud.UserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.UserWorkoutPlans)
                .WithOne(uwp => uwp.User)
                .HasForeignKey(uwp => uwp.UserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.NutritionPlans)
                .WithOne(np => np.User)
                .HasForeignKey(np => np.UserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.FoodEntries)
                .WithOne(fe => fe.User)
                .HasForeignKey(fe => fe.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<UserWorkoutPlan>()
            //    .HasMany(uwp => uwp.UserWorkoutDays)
            //    .WithOne(uwd => uwd.UserWorkoutPlan)
            //    .HasForeignKey(uwd => uwd.UserWorkoutPlanId);

            modelBuilder.Entity<NutritionPlan>()
                .HasMany(np => np.Foods)
                .WithOne(f => f.NutritionPlan)
                .HasForeignKey(f => f.NutritionPlanId);
        }
    }
}
