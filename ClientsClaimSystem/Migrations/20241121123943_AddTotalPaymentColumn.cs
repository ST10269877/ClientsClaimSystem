using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClientsClaimSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddTotalPaymentColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalPayment",
                table: "Claims",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminID",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$DzebHdgUZCcZQlWVMyiXzutrw5O2SRtOKHeWSpp0246b5H04491Qe");

            migrationBuilder.UpdateData(
                table: "Lecturers",
                keyColumn: "LecturerID",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$IG8jri7ZWMVmWda8YBOCZ.Le5iQU365eRDf4QEFAn.3QtmLnQcgOK");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPayment",
                table: "Claims");

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminID",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$4MKVIHdvLpWstuQjYuY.GuhbdXtsJ5K5fv9GyAdbcV/rqeNFlSdue");

            migrationBuilder.UpdateData(
                table: "Lecturers",
                keyColumn: "LecturerID",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$VVQCzoz7VJ2jJtVSonB0bugZ6Yib6pjkoSYq27FzAsu9jXWIUpzDe");
        }
    }
}
