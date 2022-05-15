using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AwesomeTomatoes.DAL.Migrations
{
    public partial class Refactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "score",
                table: "Reviews",
                newName: "ReviwerSatisfaction");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReviwerSatisfaction",
                table: "Reviews",
                newName: "score");
        }
    }
}
