using Microsoft.EntityFrameworkCore.Migrations;

namespace Eduhome.Migrations
{
    public partial class Noticeadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoticeDate",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "NoticeDesc",
                table: "Settings");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NoticeDate",
                table: "Settings",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NoticeDesc",
                table: "Settings",
                type: "nvarchar(1500)",
                maxLength: 1500,
                nullable: true);
        }
    }
}
