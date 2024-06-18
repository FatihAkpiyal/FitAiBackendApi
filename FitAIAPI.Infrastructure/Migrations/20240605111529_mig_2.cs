using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitAIAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mig_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "UserWorkoutPlans",
                newName: "Program");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "UserWorkoutPlans",
                newName: "Day");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "UserWorkoutPlans",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "UserWorkoutPlans",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "UserWorkoutPlans",
                type: "timestamp with time zone",
                nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "HealthProblem",
            //    table: "UserDetail",
            //    type: "text",
            //    nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "UserWorkoutPlans");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "UserWorkoutPlans");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "UserWorkoutPlans");

            //migrationBuilder.DropColumn(
            //    name: "HealthProblem",
            //    table: "UserDetail");

            migrationBuilder.RenameColumn(
                name: "Program",
                table: "UserWorkoutPlans",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Day",
                table: "UserWorkoutPlans",
                newName: "Description");
        }
    }
}
