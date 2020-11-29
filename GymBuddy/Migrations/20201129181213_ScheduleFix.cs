using Microsoft.EntityFrameworkCore.Migrations;

namespace GymBuddyAPI.Migrations
{
    public partial class ScheduleFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserData_Schedules_ScheduleId",
                table: "UserData");

            migrationBuilder.RenameColumn(
                name: "ScheduleId",
                table: "UserData",
                newName: "UserScheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_UserData_ScheduleId",
                table: "UserData",
                newName: "IX_UserData_UserScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserData_Schedules_UserScheduleId",
                table: "UserData",
                column: "UserScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserData_Schedules_UserScheduleId",
                table: "UserData");

            migrationBuilder.RenameColumn(
                name: "UserScheduleId",
                table: "UserData",
                newName: "ScheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_UserData_UserScheduleId",
                table: "UserData",
                newName: "IX_UserData_ScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserData_Schedules_ScheduleId",
                table: "UserData",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
