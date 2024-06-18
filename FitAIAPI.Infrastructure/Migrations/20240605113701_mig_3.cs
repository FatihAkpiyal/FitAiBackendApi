﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitAIAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mig_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Day",
                table: "UserWorkoutPlans");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Day",
                table: "UserWorkoutPlans",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
