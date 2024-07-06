using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace the_movie_hub.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShowTimes_RoomTypeId",
                table: "ShowTimes",
                column: "RoomTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShowTimes_RoomTypes_RoomTypeId",
                table: "ShowTimes",
                column: "RoomTypeId",
                principalTable: "RoomTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShowTimes_RoomTypes_RoomTypeId",
                table: "ShowTimes");

            migrationBuilder.DropIndex(
                name: "IX_ShowTimes_RoomTypeId",
                table: "ShowTimes");

            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "Users");
        }
    }
}
