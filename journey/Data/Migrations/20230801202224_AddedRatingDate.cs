using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace journey.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedRatingDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a0cf36e7-9095-4a74-8fd8-542147ae5c10");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "acb6fd45-aab7-414f-b8fd-ce6c7572ec67");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d83ecd84-23e1-43db-b560-82b5af4c3099");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eba88d36-3ca2-428e-a229-158c2ea9e2fe");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Rating",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4ff1ff5a-1b16-4f17-9948-1d5680322206", null, "Moderator", "MODERATOR" },
                    { "70fc4046-aa99-4241-9b41-4bef425b66aa", null, "Member", "MEMBER" },
                    { "7ad05aae-aff8-485b-a61b-45cd074b2007", null, "Admin", "ADMIN" },
                    { "d72de798-8044-493e-8a0b-e7e0632d44a3", null, "SuperAdmin", "SUPERADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ff1ff5a-1b16-4f17-9948-1d5680322206");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "70fc4046-aa99-4241-9b41-4bef425b66aa");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7ad05aae-aff8-485b-a61b-45cd074b2007");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d72de798-8044-493e-8a0b-e7e0632d44a3");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Rating");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a0cf36e7-9095-4a74-8fd8-542147ae5c10", null, "SuperAdmin", "SUPERADMIN" },
                    { "acb6fd45-aab7-414f-b8fd-ce6c7572ec67", null, "Admin", "ADMIN" },
                    { "d83ecd84-23e1-43db-b560-82b5af4c3099", null, "Moderator", "MODERATOR" },
                    { "eba88d36-3ca2-428e-a229-158c2ea9e2fe", null, "Member", "MEMBER" }
                });
        }
    }
}
