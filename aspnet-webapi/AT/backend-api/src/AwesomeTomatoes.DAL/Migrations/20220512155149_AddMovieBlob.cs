using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AwesomeTomatoes.DAL.Migrations
{
    public partial class AddMovieBlob : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MovieBlobId",
                table: "Movies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MovieBlobs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieBlobs", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_MovieBlobs_MovieBlobId",
                table: "Movies");

            migrationBuilder.DropTable(
                name: "MovieBlobs");

            migrationBuilder.DropIndex(
                name: "IX_Movies_MovieBlobId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "MovieBlobId",
                table: "Movies");
        }
    }
}
