using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_TEST.Migrations
{
    public partial class Fix_DB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VideoPath",
                table: "Recordings",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VideoPath",
                table: "Recordings");
        }
    }
}
