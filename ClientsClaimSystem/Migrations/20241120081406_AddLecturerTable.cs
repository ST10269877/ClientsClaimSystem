using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClientsClaimSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddLecturerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Lecturers",
                newName: "Username");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Lecturers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminID",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$XsOrSNSSfsl3GlarcROuW.wVXQ/mI1UZZ9qNLa6uoCz8KQH72G1qO");

            migrationBuilder.InsertData(
                table: "Lecturers",
                columns: new[] { "LecturerID", "Email", "Password", "Username" },
                values: new object[] { 1, "lecturer@example.com", "$2a$11$glkRn4q5TSUjkas3oAYmEOgqFJiiRFkTkcshFZXWGxsG6jEptligC", "lecturer" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Lecturers",
                keyColumn: "LecturerID",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Lecturers");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Lecturers",
                newName: "Name");

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminID",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$Nr/E1nBhuTNjej0Tteo3Le9GjdM1C.jQLq.t5zdb3Kx1jl0xjLEUm");
        }
    }
}
