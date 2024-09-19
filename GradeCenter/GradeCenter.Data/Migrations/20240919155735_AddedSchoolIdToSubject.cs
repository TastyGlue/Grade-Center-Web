using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GradeCenter.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedSchoolIdToSubject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SchoolId",
                table: "Subjects",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_SchoolId",
                table: "Subjects",
                column: "SchoolId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Schools_SchoolId",
                table: "Subjects",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Schools_SchoolId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_SchoolId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "SchoolId",
                table: "Subjects");
        }
    }
}
