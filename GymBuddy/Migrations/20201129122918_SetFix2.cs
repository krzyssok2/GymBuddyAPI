using Microsoft.EntityFrameworkCore.Migrations;

namespace GymBuddyAPI.Migrations
{
    public partial class SetFix2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reps_ExericseSets_ExerciseSetsId",
                table: "Reps");

            migrationBuilder.DropColumn(
                name: "Reps",
                table: "ExericseSets");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "ExericseSets");

            migrationBuilder.RenameColumn(
                name: "ExerciseSetsId",
                table: "Reps",
                newName: "ExerciseSetId");

            migrationBuilder.RenameIndex(
                name: "IX_Reps_ExerciseSetsId",
                table: "Reps",
                newName: "IX_Reps_ExerciseSetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reps_ExericseSets_ExerciseSetId",
                table: "Reps",
                column: "ExerciseSetId",
                principalTable: "ExericseSets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reps_ExericseSets_ExerciseSetId",
                table: "Reps");

            migrationBuilder.RenameColumn(
                name: "ExerciseSetId",
                table: "Reps",
                newName: "ExerciseSetsId");

            migrationBuilder.RenameIndex(
                name: "IX_Reps_ExerciseSetId",
                table: "Reps",
                newName: "IX_Reps_ExerciseSetsId");

            migrationBuilder.AddColumn<int>(
                name: "Reps",
                table: "ExericseSets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Weight",
                table: "ExericseSets",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddForeignKey(
                name: "FK_Reps_ExericseSets_ExerciseSetsId",
                table: "Reps",
                column: "ExerciseSetsId",
                principalTable: "ExericseSets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
