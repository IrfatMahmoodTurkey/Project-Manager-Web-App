using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectManagementWebApp.Migrations
{
    public partial class UpdateTaskTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Seen",
                table: "Tasks",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Seen",
                table: "Tasks");
        }
    }
}
