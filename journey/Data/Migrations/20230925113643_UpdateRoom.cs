using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace journey.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photo_Hotels_HotelId",
                table: "Photo");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Hotels");

            migrationBuilder.RenameColumn(
                name: "Catrgory",
                table: "Room",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "HotelId",
                table: "Photo",
                newName: "RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_Photo_HotelId",
                table: "Photo",
                newName: "IX_Photo_RoomId");

            migrationBuilder.AddColumn<bool>(
                name: "AC",
                table: "Room",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Room",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Photo_Room_RoomId",
                table: "Photo",
                column: "RoomId",
                principalTable: "Room",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photo_Room_RoomId",
                table: "Photo");

            migrationBuilder.DropColumn(
                name: "AC",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Room");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Room",
                newName: "Catrgory");

            migrationBuilder.RenameColumn(
                name: "RoomId",
                table: "Photo",
                newName: "HotelId");

            migrationBuilder.RenameIndex(
                name: "IX_Photo_RoomId",
                table: "Photo",
                newName: "IX_Photo_HotelId");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Hotels",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Photo_Hotels_HotelId",
                table: "Photo",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id");
        }
    }
}
