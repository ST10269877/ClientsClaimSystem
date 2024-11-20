using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClientsClaimSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddIsApprovedToClaim : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Claims",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Claims");
        }
    }
}
