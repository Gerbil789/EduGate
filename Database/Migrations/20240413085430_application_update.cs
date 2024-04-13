using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    /// <inheritdoc />
    public partial class application_update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Students_StudentId",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_StudyPrograms_Applications_ApplicationId",
                table: "StudyPrograms");

            migrationBuilder.DropIndex(
                name: "IX_StudyPrograms_ApplicationId",
                table: "StudyPrograms");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "StudyPrograms");

            migrationBuilder.RenameColumn(
                name: "IsAccepted",
                table: "Applications",
                newName: "StudyProgramId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SubmissionDate",
                table: "Applications",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "Applications",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_StudyProgramId",
                table: "Applications",
                column: "StudyProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Students_StudentId",
                table: "Applications",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_StudyPrograms_StudyProgramId",
                table: "Applications",
                column: "StudyProgramId",
                principalTable: "StudyPrograms",
                principalColumn: "StudyProgramId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Students_StudentId",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Applications_StudyPrograms_StudyProgramId",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_StudyProgramId",
                table: "Applications");

            migrationBuilder.RenameColumn(
                name: "StudyProgramId",
                table: "Applications",
                newName: "IsAccepted");

            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                table: "StudyPrograms",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "SubmissionDate",
                table: "Applications",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "Applications",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudyPrograms_ApplicationId",
                table: "StudyPrograms",
                column: "ApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Students_StudentId",
                table: "Applications",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudyPrograms_Applications_ApplicationId",
                table: "StudyPrograms",
                column: "ApplicationId",
                principalTable: "Applications",
                principalColumn: "ApplicationId");
        }
    }
}
