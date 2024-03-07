using Microsoft.EntityFrameworkCore.Migrations;

namespace Organization_Assessment_Project_NEW.Data.Migrations
{
    public partial class timesheetchangeNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_timesheets_Employees_EmployeeId1",
                table: "timesheets");

            migrationBuilder.DropIndex(
                name: "IX_timesheets_EmployeeId1",
                table: "timesheets");

            migrationBuilder.DropColumn(
                name: "EmployeeId1",
                table: "timesheets");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "timesheets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_timesheets_EmployeeId",
                table: "timesheets",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_timesheets_Employees_EmployeeId",
                table: "timesheets",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_timesheets_Employees_EmployeeId",
                table: "timesheets");

            migrationBuilder.DropIndex(
                name: "IX_timesheets_EmployeeId",
                table: "timesheets");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                table: "timesheets",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId1",
                table: "timesheets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_timesheets_EmployeeId1",
                table: "timesheets",
                column: "EmployeeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_timesheets_Employees_EmployeeId1",
                table: "timesheets",
                column: "EmployeeId1",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
