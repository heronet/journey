using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace journey.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedPhotos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "AddedBy",
                table: "Hotels",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Hotels",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Photo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    PublicId = table.Column<string>(type: "text", nullable: false),
                    HotelId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photo_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "11ddf460-00aa-4f47-aafc-9c39394517fb", null, "Moderator", "MODERATOR" },
                    { "6c6249f0-567e-4066-9540-e6b8fc753c52", null, "Member", "MEMBER" },
                    { "79223f0f-3dee-4554-a933-ed6a793c20b8", null, "SuperAdmin", "SUPERADMIN" },
                    { "cdaaf9d9-4971-4095-afa3-fff1b975fdb5", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Photo_HotelId",
                table: "Photo",
                column: "HotelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Photo");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "11ddf460-00aa-4f47-aafc-9c39394517fb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6c6249f0-567e-4066-9540-e6b8fc753c52");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "79223f0f-3dee-4554-a933-ed6a793c20b8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cdaaf9d9-4971-4095-afa3-fff1b975fdb5");

            migrationBuilder.DropColumn(
                name: "AddedBy",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Hotels");

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
    }
}
