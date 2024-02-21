using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TutorialWebApp.Migrations
{
    /// <inheritdoc />
    public partial class commentfunctionality : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        { 

            migrationBuilder.CreateTable(
                name: "TutorialComment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Content = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TutorialPostId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DateAdded = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutorialComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TutorialComment_TutorialPosts_TutorialPostId",
                        column: x => x.TutorialPostId,
                        principalTable: "TutorialPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_TutorialComment_TutorialPostId",
                table: "TutorialComment",
                column: "TutorialPostId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "TutorialComment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DateAdded = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TutorialPostId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutorialPostComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TutorialPostComment_TutorialPosts_TutorialPostId",
                        column: x => x.TutorialPostId,
                        principalTable: "TutorialPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_TutorialPostComment_TutorialPostId",
                table: "TutorialPostComment",
                column: "TutorialPostId");
        }
    }
}
