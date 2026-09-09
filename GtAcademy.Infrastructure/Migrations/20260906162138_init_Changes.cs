using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GtAcademy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init_Changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "AvatarName", "Biography", "BirthDate", "EmailAddress", "HomeAddress", "IsActive", "IsDeleted", "Job", "PhoneNumber", "ReferralCode", "ReferralId", "RegisterDate", "UserName", "VerifyToken" },
                values: new object[] { new Guid("6f9619ff-8b86-d011-b42d-00c04fc964ff"), "default.jpg", null, null, null, null, false, false, null, "00000000000", "ADMINREF", null, new DateTime(2026, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "1111" });

            migrationBuilder.InsertData(
                table: "RoleUser",
                columns: new[] { "RolesRoleId", "UsersUserId" },
                values: new object[] { 1, new Guid("6f9619ff-8b86-d011-b42d-00c04fc964ff") });

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "WalletId", "LastChargeDate", "UserId", "WalletBalance" },
                values: new object[] { new Guid("6f9619ff-8b86-d011-b42d-00c04fc964ff"), null, new Guid("6f9619ff-8b86-d011-b42d-00c04fc964ff"), 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesRoleId", "UsersUserId" },
                keyValues: new object[] { 1, new Guid("6f9619ff-8b86-d011-b42d-00c04fc964ff") });

            migrationBuilder.DeleteData(
                table: "Wallets",
                keyColumn: "WalletId",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00c04fc964ff"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00c04fc964ff"));
        }
    }
}
