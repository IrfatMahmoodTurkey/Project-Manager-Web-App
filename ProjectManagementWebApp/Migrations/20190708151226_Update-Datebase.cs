using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectManagementWebApp.Migrations
{
    public partial class UpdateDatebase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Mension",
                table: "Comments",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Mension",
                table: "Comments",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
