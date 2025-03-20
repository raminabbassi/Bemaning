using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StaffingSolution.Migrations
{
    /// <inheritdoc />
    public partial class applyform : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CanWorkEvening",
                table: "JobApplications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanWorkFullTime",
                table: "JobApplications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanWorkMorning",
                table: "JobApplications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanWorkNight",
                table: "JobApplications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<byte[]>(
                name: "CvFile",
                table: "JobApplications",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExperienceYears",
                table: "JobApplications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "HasDrivingLicense",
                table: "JobApplications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasPreviousExperience",
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
                name: "PersonalStatement",
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
                name: "CanWorkEvening",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "CanWorkFullTime",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "CanWorkMorning",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "CanWorkNight",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "CvFile",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "ExperienceYears",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "HasDrivingLicense",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "HasPreviousExperience",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "IsStudying",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "PersonalStatement",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "SwedishSkills",
                table: "JobApplications");
        }
    }
}
