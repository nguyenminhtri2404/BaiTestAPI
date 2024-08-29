using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_TEST.Migrations
{
    public partial class DB_Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Formats",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FormatType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Formats", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenreName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Recordings",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Artist = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GenreID = table.Column<int>(type: "int", nullable: false),
                    FormatID = table.Column<int>(type: "int", nullable: false),
                    Composer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                    Thumbnail = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recordings", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Recordings_Formats_FormatID",
                        column: x => x.FormatID,
                        principalTable: "Formats",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recordings_Genres_GenreID",
                        column: x => x.GenreID,
                        principalTable: "Genres",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlaybackSchedules",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordingID = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    RepeatStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RepeatEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RepeatDays = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaybackSchedules", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PlaybackSchedules_Recordings_RecordingID",
                        column: x => x.RecordingID,
                        principalTable: "Recordings",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlaybackSchedules_RecordingID",
                table: "PlaybackSchedules",
                column: "RecordingID");

            migrationBuilder.CreateIndex(
                name: "IX_Recordings_FormatID",
                table: "Recordings",
                column: "FormatID");

            migrationBuilder.CreateIndex(
                name: "IX_Recordings_GenreID",
                table: "Recordings",
                column: "GenreID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlaybackSchedules");

            migrationBuilder.DropTable(
                name: "Recordings");

            migrationBuilder.DropTable(
                name: "Formats");

            migrationBuilder.DropTable(
                name: "Genres");
        }
    }
}
