using Microsoft.EntityFrameworkCore.Migrations;

namespace GymBuddyAPI.Migrations
{
    public partial class CascadeSets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExericseSets_Exercises_ExerciseId",
                table: "ExericseSets");

            migrationBuilder.AddForeignKey(
                name: "FK_ExericseSets_Exercises_ExerciseId",
                table: "ExericseSets",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExericseSets_Exercises_ExerciseId",
                table: "ExericseSets");

            migrationBuilder.AddForeignKey(
                name: "FK_ExericseSets_Exercises_ExerciseId",
                table: "ExericseSets",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
