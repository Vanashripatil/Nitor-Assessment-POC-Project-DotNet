using Microsoft.EntityFrameworkCore.Migrations;

namespace Organization_Assessment_Project_NEW.Data.Migrations
{
    public partial class CreateLeaveRequestSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "createLeaveRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeaveTypeId = table.Column<int>(type: "int", nullable: false),
                    Levae_Request_Comments = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    StartDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndDate = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_createLeaveRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_createLeaveRequests_LeaveTypes_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalTable: "LeaveTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_createLeaveRequests_LeaveTypeId",
                table: "createLeaveRequests",
                column: "LeaveTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "createLeaveRequests");
        }
    }
}
