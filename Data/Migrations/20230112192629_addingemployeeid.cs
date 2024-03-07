using Microsoft.EntityFrameworkCore.Migrations;

namespace Organization_Assessment_Project_NEW.Data.Migrations
{
    public partial class addingemployeeid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmployeeId",
                table: "createLeaveRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId1",
                table: "createLeaveRequests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_createLeaveRequests_EmployeeId1",
                table: "createLeaveRequests",
                column: "EmployeeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_createLeaveRequests_Employees_EmployeeId1",
                table: "createLeaveRequests",
                column: "EmployeeId1",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_createLeaveRequests_Employees_EmployeeId1",
                table: "createLeaveRequests");

            migrationBuilder.DropIndex(
                name: "IX_createLeaveRequests_EmployeeId1",
                table: "createLeaveRequests");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "createLeaveRequests");

            migrationBuilder.DropColumn(
                name: "EmployeeId1",
                table: "createLeaveRequests");
        }
    }
}
