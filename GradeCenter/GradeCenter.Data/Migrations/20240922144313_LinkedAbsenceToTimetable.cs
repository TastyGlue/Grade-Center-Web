using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GradeCenter.Data.Migrations
{
    /// <inheritdoc />
    public partial class LinkedAbsenceToTimetable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Time",
                table: "Absences");

            migrationBuilder.RenameColumn(
                name: "Day",
                table: "Absences",
                newName: "TimetableId");

            migrationBuilder.AddColumn<DateOnly>(
                name: "Date",
                table: "Absences",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.CreateIndex(
                name: "IX_Absences_TimetableId",
                table: "Absences",
                column: "TimetableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Absences_Timetables_TimetableId",
                table: "Absences",
                column: "TimetableId",
                principalTable: "Timetables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Absences_Timetables_TimetableId",
                table: "Absences");

            migrationBuilder.DropIndex(
                name: "IX_Absences_TimetableId",
                table: "Absences");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Absences");

            migrationBuilder.RenameColumn(
                name: "TimetableId",
                table: "Absences",
                newName: "Day");

            migrationBuilder.AddColumn<DateTime>(
                name: "Time",
                table: "Absences",
                type: "timestamp with time zone",
                nullable: true);
        }
    }
}
