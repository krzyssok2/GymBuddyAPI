using Microsoft.EntityFrameworkCore.Migrations;

namespace GymBuddyAPI.Migrations
{
    public partial class TableNameChangs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExercisesWorkouts");

            migrationBuilder.DropTable(
                name: "Reps");

            migrationBuilder.CreateTable(
                name: "ExerciseWorkout",
                columns: table => new
                {
                    ExercisesId = table.Column<long>(type: "bigint", nullable: false),
                    WorkoutsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseWorkout", x => new { x.ExercisesId, x.WorkoutsId });
                    table.ForeignKey(
                        name: "FK_ExerciseWorkout_Exercises_ExercisesId",
                        column: x => x.ExercisesId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExerciseWorkout_Workouts_WorkoutsId",
                        column: x => x.WorkoutsId,
                        principalTable: "Workouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rep",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    ExerciseSetId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rep", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rep_ExericseSets_ExerciseSetId",
                        column: x => x.ExerciseSetId,
                        principalTable: "ExericseSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseWorkout_WorkoutsId",
                table: "ExerciseWorkout",
                column: "WorkoutsId");

            migrationBuilder.CreateIndex(
                name: "IX_Rep_ExerciseSetId",
                table: "Rep",
                column: "ExerciseSetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExerciseWorkout");

            migrationBuilder.DropTable(
                name: "Rep");

            migrationBuilder.CreateTable(
                name: "ExercisesWorkouts",
                columns: table => new
                {
                    ExercisesId = table.Column<long>(type: "bigint", nullable: false),
                    WorkoutsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExercisesWorkouts", x => new { x.ExercisesId, x.WorkoutsId });
                    table.ForeignKey(
                        name: "FK_ExercisesWorkouts_Exercises_ExercisesId",
                        column: x => x.ExercisesId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExercisesWorkouts_Workouts_WorkoutsId",
                        column: x => x.WorkoutsId,
                        principalTable: "Workouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reps",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExerciseSetId = table.Column<long>(type: "bigint", nullable: true),
                    Weight = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reps_ExericseSets_ExerciseSetId",
                        column: x => x.ExerciseSetId,
                        principalTable: "ExericseSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExercisesWorkouts_WorkoutsId",
                table: "ExercisesWorkouts",
                column: "WorkoutsId");

            migrationBuilder.CreateIndex(
                name: "IX_Reps_ExerciseSetId",
                table: "Reps",
                column: "ExerciseSetId");
        }
    }
}
