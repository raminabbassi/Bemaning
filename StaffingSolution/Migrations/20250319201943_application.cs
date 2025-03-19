using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StaffingSolution.Migrations
{
    /// <inheritdoc />
    public partial class application : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CanWorkFulltime",
                table: "JobApplications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CoverLetter",
                table: "JobApplications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ExperienceYears",
                table: "JobApplications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "JobApplications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "HasDrivingLicense",
                table: "JobApplications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsStudying",
                table: "JobApplications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "JobApplications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "JobApplications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SwedishSkills",
                table: "JobApplications",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanWorkFulltime",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "CoverLetter",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "ExperienceYears",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "HasDrivingLicense",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "IsStudying",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "SwedishSkills",
                table: "JobApplications");
        }
    }
}
