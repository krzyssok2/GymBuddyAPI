using Microsoft.EntityFrameworkCore.Migrations;

namespace GymBuddyAPI.Migrations
{
    public partial class SetFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rep");

            migrationBuilder.AddColumn<int>(
                name: "RepCount",
                table: "ExericseSets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "ExericseSets",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RepCount",
                table: "ExericseSets");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "ExericseSets");

            migrationBuilder.CreateTable(
                name: "Rep",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExerciseSetId = table.Column<long>(type: "bigint", nullable: true),
                    Weight = table.Column<int>(type: "int", nullable: false)
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
                name: "IX_Rep_ExerciseSetId",
                table: "Rep",
                column: "ExerciseSetId");
        }
    }
}
