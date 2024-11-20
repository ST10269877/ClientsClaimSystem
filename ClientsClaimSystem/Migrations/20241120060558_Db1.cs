using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClientsClaimSystem.Migrations
{
    /// <inheritdoc />
    public partial class Db1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminID",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$Nr/E1nBhuTNjej0Tteo3Le9GjdM1C.jQLq.t5zdb3Kx1jl0xjLEUm");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminID",
                keyValue: 1,
                column: "Password",
                value: "password123");
        }
    }
}
