﻿// <auto-generated />
using System;
using FitAIAPI.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FitAIAPI.Infrastructure.Migrations
{
    [DbContext(typeof(FitAIDbContext))]
    partial class FitAIDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FitAIAPI.Domain.Entities.Exercise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Repetitions")
                        .HasColumnType("integer");

                    b.Property<int>("Sets")
                        .HasColumnType("integer");

                    b.Property<int>("UserWorkoutDayId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserWorkoutDayId");

                    b.ToTable("Exercise");
                });

            modelBuilder.Entity("FitAIAPI.Domain.Entities.Food", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Calories")
                        .HasColumnType("integer");

                    b.Property<int>("Carbs")
                        .HasColumnType("integer");

                    b.Property<int>("Fat")
                        .HasColumnType("integer");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("NutritionPlanId")
                        .HasColumnType("integer");

                    b.Property<int>("Protein")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("NutritionPlanId");

                    b.ToTable("Foods");
                });

            modelBuilder.Entity("FitAIAPI.Domain.Entities.FoodEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("FoodId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("FoodId");

                    b.HasIndex("UserId");

                    b.ToTable("FoodEntries");
                });

            modelBuilder.Entity("FitAIAPI.Domain.Entities.NutritionPlan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("NutritionPlans");
                });

            modelBuilder.Entity("FitAIAPI.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsFirstLogin")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("LastLogin")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProfilePictureUrl")
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FitAIAPI.Domain.Entities.UserDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("BasalMetabolism")
                        .HasColumnType("integer");

                    b.Property<double?>("CurrentWeight")
                        .HasColumnType("double precision");

                    b.Property<int?>("DailyKcalGoal")
                        .HasColumnType("integer");

                    b.Property<string>("DateOfBirth")
                        .HasColumnType("text");

                    b.Property<double?>("FirstWeight")
                        .HasColumnType("double precision");

                    b.Property<string>("FocusAreas")
                        .HasColumnType("text");

                    b.Property<string>("Gender")
                        .HasColumnType("text");

                    b.Property<string>("Goals")
                        .HasColumnType("text");

                    b.Property<string>("HealthProblem")
                        .HasColumnType("text");

                    b.Property<double?>("Height")
                        .HasColumnType("double precision");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("PreferredActivities")
                        .HasColumnType("text");

                    b.Property<double?>("TargetWeight")
                        .HasColumnType("double precision");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<string>("WorkoutFrequency")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserDetail");
                });

            modelBuilder.Entity("FitAIAPI.Domain.Entities.UserWorkoutDay", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Day")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<int>("UserWorkoutPlanId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserWorkoutPlanId");

                    b.ToTable("UserWorkoutDays");
                });

            modelBuilder.Entity("FitAIAPI.Domain.Entities.UserWorkoutPlan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Program")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserWorkoutPlans");
                });

            modelBuilder.Entity("FitAIAPI.Domain.Entities.Exercise", b =>
                {
                    b.HasOne("FitAIAPI.Domain.Entities.UserWorkoutDay", "UserWorkoutDay")
                        .WithMany("Exercises")
                        .HasForeignKey("UserWorkoutDayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserWorkoutDay");
                });

            modelBuilder.Entity("FitAIAPI.Domain.Entities.Food", b =>
                {
                    b.HasOne("FitAIAPI.Domain.Entities.NutritionPlan", "NutritionPlan")
                        .WithMany("Foods")
                        .HasForeignKey("NutritionPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NutritionPlan");
                });

            modelBuilder.Entity("FitAIAPI.Domain.Entities.FoodEntry", b =>
                {
                    b.HasOne("FitAIAPI.Domain.Entities.Food", "Food")
                        .WithMany()
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FitAIAPI.Domain.Entities.User", "User")
                        .WithMany("FoodEntries")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Food");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FitAIAPI.Domain.Entities.NutritionPlan", b =>
                {
                    b.HasOne("FitAIAPI.Domain.Entities.User", "User")
                        .WithMany("NutritionPlans")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FitAIAPI.Domain.Entities.UserDetail", b =>
                {
                    b.HasOne("FitAIAPI.Domain.Entities.User", "User")
                        .WithOne("UserDetail")
                        .HasForeignKey("FitAIAPI.Domain.Entities.UserDetail", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FitAIAPI.Domain.Entities.UserWorkoutDay", b =>
                {
                    b.HasOne("FitAIAPI.Domain.Entities.UserWorkoutPlan", "UserWorkoutPlan")
                        .WithMany()
                        .HasForeignKey("UserWorkoutPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserWorkoutPlan");
                });

            modelBuilder.Entity("FitAIAPI.Domain.Entities.UserWorkoutPlan", b =>
                {
                    b.HasOne("FitAIAPI.Domain.Entities.User", "User")
                        .WithMany("UserWorkoutPlans")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FitAIAPI.Domain.Entities.NutritionPlan", b =>
                {
                    b.Navigation("Foods");
                });

            modelBuilder.Entity("FitAIAPI.Domain.Entities.User", b =>
                {
                    b.Navigation("FoodEntries");

                    b.Navigation("NutritionPlans");

                    b.Navigation("UserDetail")
                        .IsRequired();

                    b.Navigation("UserWorkoutPlans");
                });

            modelBuilder.Entity("FitAIAPI.Domain.Entities.UserWorkoutDay", b =>
                {
                    b.Navigation("Exercises");
                });
#pragma warning restore 612, 618
        }
    }
}
