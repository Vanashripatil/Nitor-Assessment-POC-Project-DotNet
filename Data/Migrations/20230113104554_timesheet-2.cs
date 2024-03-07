using Microsoft.EntityFrameworkCore.Migrations;

namespace Organization_Assessment_Project_NEW.Data.Migrations
{
    public partial class timesheet2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TimesheetComment",
                table: "timesheets",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimesheetComment",
                table: "timesheets");
        }
    }
}
