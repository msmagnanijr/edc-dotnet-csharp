using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AwesomeTomatoes.DAL.Migrations
{
    public partial class FixMovieBlob : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_MovieBlobs_MovieBlobId",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_MovieBlobId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "MovieBlobId",
                table: "Movies");

            migrationBuilder.AddColumn<string>(
                name: "BlobUrl",
                table: "MovieBlobs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "MovieBlobs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MovieId",
                table: "MovieBlobs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MovieBlobs_MovieId",
                table: "MovieBlobs",
                column: "MovieId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieBlobs_Movies_MovieId",
                table: "MovieBlobs",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieBlobs_Movies_MovieId",
                table: "MovieBlobs");

            migrationBuilder.DropIndex(
                name: "IX_MovieBlobs_MovieId",
                table: "MovieBlobs");

            migrationBuilder.DropColumn(
                name: "BlobUrl",
                table: "MovieBlobs");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "MovieBlobs");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "MovieBlobs");

            migrationBuilder.AddColumn<int>(
                name: "MovieBlobId",
                table: "Movies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movies_MovieBlobId",
                table: "Movies",
                column: "MovieBlobId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_MovieBlobs_MovieBlobId",
                table: "Movies",
                column: "MovieBlobId",
                principalTable: "MovieBlobs",
                principalColumn: "Id");
        }
    }
}
