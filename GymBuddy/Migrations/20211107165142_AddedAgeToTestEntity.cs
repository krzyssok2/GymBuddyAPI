using Microsoft.EntityFrameworkCore.Migrations;

namespace GymBuddyAPI.Migrations
{
    public partial class AddedAgeToTestEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Age",
                table: "TestEntity",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "TestEntity");
        }
    }
}
