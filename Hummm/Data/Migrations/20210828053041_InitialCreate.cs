using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hummm.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Consumers",
                columns: table => new
                {
                    ConsumerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SearchString = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SearchDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PostID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consumers", x => x.ConsumerID);
                });

            migrationBuilder.CreateTable(
                name: "Posters",
                columns: table => new
                {
                    PosterID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PosterName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posters", x => x.PosterID);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    PostID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    MP3 = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PostDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PosterID = table.Column<int>(type: "int", nullable: true),
                    ConsumerID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.PostID);
                    table.ForeignKey(
                        name: "FK_Posts_Consumers_ConsumerID",
                        column: x => x.ConsumerID,
                        principalTable: "Consumers",
                        principalColumn: "ConsumerID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Posts_Posters_PosterID",
                        column: x => x.PosterID,
                        principalTable: "Posters",
                        principalColumn: "PosterID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_ConsumerID",
                table: "Posts",
                column: "ConsumerID");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_PosterID",
                table: "Posts",
                column: "PosterID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Consumers");

            migrationBuilder.DropTable(
                name: "Posters");
        }
    }
}
