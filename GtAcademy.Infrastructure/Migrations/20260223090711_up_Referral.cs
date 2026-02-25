using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GtAcademy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class up_Referral : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVerified",
                table: "Referrals",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVerified",
                table: "Referrals");
        }
    }
}
