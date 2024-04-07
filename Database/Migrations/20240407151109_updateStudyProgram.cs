using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    /// <inheritdoc />
    public partial class updateStudyProgram : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudyPrograms_Schools_SchoolId",
                table: "StudyPrograms");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Students");

            migrationBuilder.AlterColumn<int>(
                name: "SchoolId",
                table: "StudyPrograms",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_StudyPrograms_Schools_SchoolId",
                table: "StudyPrograms",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "SchoolId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudyPrograms_Schools_SchoolId",
                table: "StudyPrograms");

            migrationBuilder.AlterColumn<int>(
                name: "SchoolId",
                table: "StudyPrograms",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Version",
                table: "Students",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_StudyPrograms_Schools_SchoolId",
                table: "StudyPrograms",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "SchoolId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
