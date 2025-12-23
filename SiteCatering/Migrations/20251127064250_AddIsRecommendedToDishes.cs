using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SiteCatering.Migrations
{
    /// <inheritdoc />
    public partial class AddIsRecommendedToDishes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "7b0011d3-8f20-4684-9958-28ca21a185e2", "8eee69f3-3d09-4d8d-8830-1c9b8595376e" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7b0011d3-8f20-4684-9958-28ca21a185e2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8eee69f3-3d09-4d8d-8830-1c9b8595376e");

            migrationBuilder.AddColumn<bool>(
                name: "IsRecommended",
                table: "Dishes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ac36ccdf-1670-4d49-ada6-390276bf9e4c", "68cc997d-7daf-4a48-92a9-67b9674610ed", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "36237faa-c14a-46f9-8361-81a9b864578d", 0, "e1802247-a27b-4dc1-a7b4-3a0e253bfe27", "belyanko97@yandex.ru", true, false, null, "belyanko97@yandex.ru", "ADMIN", "AQAAAAIAAYagAAAAEOraMpv30Nzug+cmz9F0uPBZNS8Vs53Qx8f0Mi6Vvg7rAmp+hRH0eSh8va5dVhhRmg==", null, true, "", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "ac36ccdf-1670-4d49-ada6-390276bf9e4c", "36237faa-c14a-46f9-8361-81a9b864578d" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "ac36ccdf-1670-4d49-ada6-390276bf9e4c", "36237faa-c14a-46f9-8361-81a9b864578d" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ac36ccdf-1670-4d49-ada6-390276bf9e4c");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "36237faa-c14a-46f9-8361-81a9b864578d");

            migrationBuilder.DropColumn(
                name: "IsRecommended",
                table: "Dishes");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7b0011d3-8f20-4684-9958-28ca21a185e2", null, "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "8eee69f3-3d09-4d8d-8830-1c9b8595376e", 0, "8c52de01-6396-45cb-88a3-9fdb0e53bb27", "belyanko97@yandex.ru", true, false, null, "belyanko97@yandex.ru", "ADMIN", "AQAAAAIAAYagAAAAEDckCSZ6QI5WHX0AU8ZVr4BVAGXJQEmpNLHkHlDjc0DQiQ5X1LzxGMtUDxpx+vUhEg==", null, true, "", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "7b0011d3-8f20-4684-9958-28ca21a185e2", "8eee69f3-3d09-4d8d-8830-1c9b8595376e" });
        }
    }
}
