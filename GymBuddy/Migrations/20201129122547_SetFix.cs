using Microsoft.EntityFrameworkCore.Migrations;

namespace GymBuddyAPI.Migrations
{
    public partial class SetFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Workouts");

            migrationBuilder.CreateTable(
                name: "Reps",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    ExerciseSetsId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reps_ExericseSets_ExerciseSetsId",
                        column: x => x.ExerciseSetsId,
                        principalTable: "ExericseSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reps_ExerciseSetsId",
                table: "Reps",
                column: "ExerciseSetsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reps");

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Workouts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
