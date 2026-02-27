using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalEstudos.API.Migrations
{
    /// <inheritdoc />
    public partial class AddUserTopicInterestAndActivity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserTopicActivities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    TopicId = table.Column<int>(type: "INTEGER", nullable: false),
                    UltimoAcesso = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TotalAcessos = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTopicActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTopicActivities_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTopicActivities_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTopicInterests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    TopicId = table.Column<int>(type: "INTEGER", nullable: false),
                    DataMarcacao = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTopicInterests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTopicInterests_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTopicInterests_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserTopicActivities_TopicId",
                table: "UserTopicActivities",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTopicActivities_UserId",
                table: "UserTopicActivities",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTopicInterests_TopicId",
                table: "UserTopicInterests",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTopicInterests_UserId",
                table: "UserTopicInterests",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserTopicActivities");

            migrationBuilder.DropTable(
                name: "UserTopicInterests");
        }
    }
}
