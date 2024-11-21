using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClientsClaimSystem.Migrations
{
    public partial class CreateLecturerTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Create the 'Lecturers' table
            migrationBuilder.CreateTable(
                name: "Lecturers",
                columns: table => new
                {
                    LecturerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lecturers", x => x.LecturerID);
                });

            // Seed data for the 'Lecturers' table (optional)
            migrationBuilder.InsertData(
                table: "Lecturers",
                columns: new[] { "LecturerID", "Username", "Password", "Email" },
                values: new object[] { 1, "lecturer", "$2a$11$kb0eJzofCoz6T0MnrJO.L.zlmVL.vrB39ajnlSgTCJuCI4VtNjPxa", "lecturer@example.com" });

            // Update the password for 'Admins' table
            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminID",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$kb0eJzofCoz6T0MnrJO.L.zlmVL.vrB39ajnlSgTCJuCI4VtNjPxa");

            // Update the password for 'Lecturers' table (if it already exists)
            migrationBuilder.UpdateData(
                table: "Lecturers",
                keyColumn: "LecturerID",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$sGdo3kVjfCQKPIaGUKThA.cBqg.K9BqrMbr55xPt4OB2iE.7E.eq6");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop the 'Lecturers' table if the migration is rolled back
            migrationBuilder.DropTable(name: "Lecturers");

            // Optionally revert changes in other tables (Admins)
            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminID",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$JxVHsDu3d1WqSKYuyAKT2Ov3nRKdCHP7/gxa0.MjEov4GLQeNG2va");

            // Optionally revert password changes in 'Lecturers' table
            migrationBuilder.UpdateData(
                table: "Lecturers",
                keyColumn: "LecturerID",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$ool8xRlUHHegGFkRm/bASesWBXFvu8oiJuxFSx8lO9j2UHLr9J31y");
        }
    }
}
