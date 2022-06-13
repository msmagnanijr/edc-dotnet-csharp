using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    public partial class UpdateReviewComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_ReviewCommentEntity_ReviewCommentId",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReviewCommentEntity",
                table: "ReviewCommentEntity");

            migrationBuilder.RenameTable(
                name: "ReviewCommentEntity",
                newName: "ReviewComments");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReviewComments",
                table: "ReviewComments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_ReviewComments_ReviewCommentId",
                table: "Reviews",
                column: "ReviewCommentId",
                principalTable: "ReviewComments",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_ReviewComments_ReviewCommentId",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReviewComments",
                table: "ReviewComments");

            migrationBuilder.RenameTable(
                name: "ReviewComments",
                newName: "ReviewCommentEntity");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReviewCommentEntity",
                table: "ReviewCommentEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_ReviewCommentEntity_ReviewCommentId",
                table: "Reviews",
                column: "ReviewCommentId",
                principalTable: "ReviewCommentEntity",
                principalColumn: "Id");
        }
    }
}
