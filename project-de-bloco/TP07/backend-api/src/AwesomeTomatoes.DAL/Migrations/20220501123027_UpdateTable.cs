using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AwesomeTomatoes.DAL.Migrations
{
    public partial class UpdateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "reviwerSatisfaction",
                table: "Reviews",
                newName: "ReviwerSatisfaction");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReviwerSatisfaction",
                table: "Reviews",
                newName: "reviwerSatisfaction");
        }
    }
}
