using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KhoaHocOnline.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEnrollmentInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Enrollments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Enrollments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Enrollments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Enrollments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PreferredTime",
                table: "Enrollments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "PreferredTime",
                table: "Enrollments");
        }
    }
}
