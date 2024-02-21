using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TutorialWebApp.Migrations
{
    /// <inheritdoc />
    public partial class Addinglikefunctionality : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TutorialPostLike",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    BlogPostId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TutorialPostId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutorialPostLike", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TutorialPostLike_TutorialPosts_TutorialPostId",
                        column: x => x.TutorialPostId,
                        principalTable: "TutorialPosts",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_TutorialPostLike_TutorialPostId",
                table: "TutorialPostLike",
                column: "TutorialPostId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TutorialPostLike");
        }
    }
}
