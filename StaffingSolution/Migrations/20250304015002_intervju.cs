using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StaffingSolution.Migrations
{
    /// <inheritdoc />
    public partial class intervju : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CandidateId",
                table: "InterviewRequests");

            migrationBuilder.DropColumn(
                name: "InterviewDate",
                table: "InterviewRequests");

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "InterviewRequests");

            migrationBuilder.RenameColumn(
                name: "RecruiterEmail",
                table: "InterviewRequests",
                newName: "Message");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "InterviewRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "InterviewRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "JobTitle",
                table: "InterviewRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "InterviewRequests");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "InterviewRequests");

            migrationBuilder.DropColumn(
                name: "JobTitle",
                table: "InterviewRequests");

            migrationBuilder.RenameColumn(
                name: "Message",
                table: "InterviewRequests",
                newName: "RecruiterEmail");

            migrationBuilder.AddColumn<int>(
                name: "CandidateId",
                table: "InterviewRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "InterviewDate",
                table: "InterviewRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "JobId",
                table: "InterviewRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
