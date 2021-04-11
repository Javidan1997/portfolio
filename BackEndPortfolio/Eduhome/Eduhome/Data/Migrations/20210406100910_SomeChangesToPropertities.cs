using Microsoft.EntityFrameworkCore.Migrations;

namespace Eduhome.Migrations
{
    public partial class SomeChangesToPropertities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventTeacher_Events_EventId",
                table: "EventTeacher");

            migrationBuilder.DropForeignKey(
                name: "FK_EventTeacher_Teachers_TeacherId",
                table: "EventTeacher");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventTeacher",
                table: "EventTeacher");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "EventTeacher");

            migrationBuilder.DropColumn(
                name: "TagId",
                table: "EventTeacher");

            migrationBuilder.RenameTable(
                name: "EventTeacher",
                newName: "EventTeachers");

            migrationBuilder.RenameIndex(
                name: "IX_EventTeacher_TeacherId",
                table: "EventTeachers",
                newName: "IX_EventTeachers_TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_EventTeacher_EventId",
                table: "EventTeachers",
                newName: "IX_EventTeachers_EventId");

            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "EventTags",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "TeacherId",
                table: "EventTeachers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                table: "EventTeachers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventTeachers",
                table: "EventTeachers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventTeachers_Events_EventId",
                table: "EventTeachers",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventTeachers_Teachers_TeacherId",
                table: "EventTeachers",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventTeachers_Events_EventId",
                table: "EventTeachers");

            migrationBuilder.DropForeignKey(
                name: "FK_EventTeachers_Teachers_TeacherId",
                table: "EventTeachers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventTeachers",
                table: "EventTeachers");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "EventTags");

            migrationBuilder.RenameTable(
                name: "EventTeachers",
                newName: "EventTeacher");

            migrationBuilder.RenameIndex(
                name: "IX_EventTeachers_TeacherId",
                table: "EventTeacher",
                newName: "IX_EventTeacher_TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_EventTeachers_EventId",
                table: "EventTeacher",
                newName: "IX_EventTeacher_EventId");

            migrationBuilder.AlterColumn<int>(
                name: "TeacherId",
                table: "EventTeacher",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                table: "EventTeacher",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "EventTeacher",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TagId",
                table: "EventTeacher",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventTeacher",
                table: "EventTeacher",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventTeacher_Events_EventId",
                table: "EventTeacher",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EventTeacher_Teachers_TeacherId",
                table: "EventTeacher",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
