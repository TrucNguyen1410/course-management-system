using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KhoaHocOnline.API.Migrations
{
    /// <inheritdoc />
    public partial class AddVoucherAndEnrollment_Fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_AspNetUsers_StudentID",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Courses_CourseID",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "DiscountAmount",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "DiscountPercentage",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "ValidFrom",
                table: "Vouchers");

            migrationBuilder.RenameColumn(
                name: "ValidTo",
                table: "Vouchers",
                newName: "ExpiryDate");

            migrationBuilder.RenameColumn(
                name: "StudentID",
                table: "Enrollments",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "PurchasePrice",
                table: "Enrollments",
                newName: "PriceAtEnrollment");

            migrationBuilder.RenameColumn(
                name: "PurchaseDate",
                table: "Enrollments",
                newName: "EnrollmentDate");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_StudentID",
                table: "Enrollments",
                newName: "IX_Enrollments_UserID");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Vouchers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountValue",
                table: "Vouchers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Vouchers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPercentage",
                table: "Vouchers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "CourseID",
                table: "Enrollments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "Enrollments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_AspNetUsers_UserID",
                table: "Enrollments",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Courses_CourseID",
                table: "Enrollments",
                column: "CourseID",
                principalTable: "Courses",
                principalColumn: "CourseID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_AspNetUsers_UserID",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Courses_CourseID",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "DiscountValue",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "IsPercentage",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "Enrollments");

            migrationBuilder.RenameColumn(
                name: "ExpiryDate",
                table: "Vouchers",
                newName: "ValidTo");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Enrollments",
                newName: "StudentID");

            migrationBuilder.RenameColumn(
                name: "PriceAtEnrollment",
                table: "Enrollments",
                newName: "PurchasePrice");

            migrationBuilder.RenameColumn(
                name: "EnrollmentDate",
                table: "Enrollments",
                newName: "PurchaseDate");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_UserID",
                table: "Enrollments",
                newName: "IX_Enrollments_StudentID");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Vouchers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Vouchers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountAmount",
                table: "Vouchers",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DiscountPercentage",
                table: "Vouchers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Vouchers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Vouchers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ValidFrom",
                table: "Vouchers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "CourseID",
                table: "Enrollments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_AspNetUsers_StudentID",
                table: "Enrollments",
                column: "StudentID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Courses_CourseID",
                table: "Enrollments",
                column: "CourseID",
                principalTable: "Courses",
                principalColumn: "CourseID");
        }
    }
}
