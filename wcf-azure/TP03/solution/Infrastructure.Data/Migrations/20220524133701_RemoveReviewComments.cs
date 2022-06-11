using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    public partial class RemoveReviewComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_ReviewComments_ReviewCommentId",
                table: "Reviews");

            migrationBuilder.DropTable(
                name: "ReviewComments");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_ReviewCommentId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "ReviewCommentId",
                table: "Reviews");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReviewCommentId",
                table: "Reviews",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ReviewComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReviewId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewComments", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ReviewCommentId",
                table: "Reviews",
                column: "ReviewCommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_ReviewComments_ReviewCommentId",
                table: "Reviews",
                column: "ReviewCommentId",
                principalTable: "ReviewComments",
                principalColumn: "Id");
        }
    }
}
