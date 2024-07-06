using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace the_movie_hub.Migrations
{
    /// <inheritdoc />
    public partial class thrid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_ShowTimes_ShowtimeId1",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "ShowtimeId1",
                table: "Tickets",
                newName: "TicketTypeId1");

            migrationBuilder.RenameColumn(
                name: "ShowtimeId",
                table: "Tickets",
                newName: "TicketTypeId");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Tickets",
                newName: "StartAt");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_ShowtimeId1",
                table: "Tickets",
                newName: "IX_Tickets_TicketTypeId1");

            migrationBuilder.AddColumn<string>(
                name: "MovieId",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MovieId1",
                table: "Tickets",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoomId",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RoomId1",
                table: "Tickets",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TheaterId",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TheaterId1",
                table: "Tickets",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Total",
                table: "Tickets",
                type: "real",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TicketTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_MovieId1",
                table: "Tickets",
                column: "MovieId1");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_RoomId1",
                table: "Tickets",
                column: "RoomId1");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_TheaterId1",
                table: "Tickets",
                column: "TheaterId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Movies_MovieId1",
                table: "Tickets",
                column: "MovieId1",
                principalTable: "Movies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Rooms_RoomId1",
                table: "Tickets",
                column: "RoomId1",
                principalTable: "Rooms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Theaters_TheaterId1",
                table: "Tickets",
                column: "TheaterId1",
                principalTable: "Theaters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_TicketTypes_TicketTypeId1",
                table: "Tickets",
                column: "TicketTypeId1",
                principalTable: "TicketTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Movies_MovieId1",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Rooms_RoomId1",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Theaters_TheaterId1",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_TicketTypes_TicketTypeId1",
                table: "Tickets");

            migrationBuilder.DropTable(
                name: "TicketTypes");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_MovieId1",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_RoomId1",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_TheaterId1",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "MovieId1",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "RoomId1",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "TheaterId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "TheaterId1",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "TicketTypeId1",
                table: "Tickets",
                newName: "ShowtimeId1");

            migrationBuilder.RenameColumn(
                name: "TicketTypeId",
                table: "Tickets",
                newName: "ShowtimeId");

            migrationBuilder.RenameColumn(
                name: "StartAt",
                table: "Tickets",
                newName: "CreatedAt");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_TicketTypeId1",
                table: "Tickets",
                newName: "IX_Tickets_ShowtimeId1");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Tickets",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_ShowTimes_ShowtimeId1",
                table: "Tickets",
                column: "ShowtimeId1",
                principalTable: "ShowTimes",
                principalColumn: "Id");
        }
    }
}
