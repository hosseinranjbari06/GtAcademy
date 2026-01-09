using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GtAcademy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init_Topics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Episode_Courses_CourseId",
                table: "Episode");

            migrationBuilder.DropIndex(
                name: "IX_Episode_CourseId",
                table: "Episode");

            migrationBuilder.AddColumn<int>(
                name: "TopicId",
                table: "Episode",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Topic",
                columns: table => new
                {
                    TopicId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topic", x => x.TopicId);
                    table.ForeignKey(
                        name: "FK_Topic_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Episode_TopicId",
                table: "Episode",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_Topic_CourseId",
                table: "Topic",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Episode_Topic_TopicId",
                table: "Episode",
                column: "TopicId",
                principalTable: "Topic",
                principalColumn: "TopicId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Episode_Topic_TopicId",
                table: "Episode");

            migrationBuilder.DropTable(
                name: "Topic");

            migrationBuilder.DropIndex(
                name: "IX_Episode_TopicId",
                table: "Episode");

            migrationBuilder.DropColumn(
                name: "TopicId",
                table: "Episode");

            migrationBuilder.CreateIndex(
                name: "IX_Episode_CourseId",
                table: "Episode",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Episode_Courses_CourseId",
                table: "Episode",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
