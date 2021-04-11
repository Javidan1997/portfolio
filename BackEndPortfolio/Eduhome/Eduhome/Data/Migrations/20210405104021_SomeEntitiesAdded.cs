using Microsoft.EntityFrameworkCore.Migrations;

namespace Eduhome.Migrations
{
    public partial class SomeEntitiesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Courses_CoursesId",
                table: "Tags");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Events_EventsId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_CoursesId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_EventsId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "CoursesId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "EventsId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "TagId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "TagId",
                table: "Courses");

            migrationBuilder.CreateTable(
                name: "CourseTag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseTag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseTag_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseTag_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventTag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventTag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventTag_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventTag_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseTag_CourseId",
                table: "CourseTag",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseTag_TagId",
                table: "CourseTag",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_EventTag_EventId",
                table: "EventTag",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventTag_TagId",
                table: "EventTag",
                column: "TagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseTag");

            migrationBuilder.DropTable(
                name: "EventTag");

            migrationBuilder.AddColumn<int>(
                name: "CoursesId",
                table: "Tags",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EventsId",
                table: "Tags",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TagId",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TagId",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_CoursesId",
                table: "Tags",
                column: "CoursesId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_EventsId",
                table: "Tags",
                column: "EventsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Courses_CoursesId",
                table: "Tags",
                column: "CoursesId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Events_EventsId",
                table: "Tags",
                column: "EventsId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
