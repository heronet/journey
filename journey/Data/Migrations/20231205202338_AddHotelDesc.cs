using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace journey.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddHotelDesc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Hotels",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Hotels");
        }
    }
}
