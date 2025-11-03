using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaBooking.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Halls",
                columns: table => new
                {
                    HallID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rows = table.Column<int>(type: "int", nullable: false),
                    SeatsPerRow = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Halls", x => x.HallID);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    MovieID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.MovieID);
                });

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    SeatID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Row = table.Column<int>(type: "int", nullable: false),
                    SeatNumber = table.Column<int>(type: "int", nullable: false),
                    HallID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.SeatID);
                    table.ForeignKey(
                        name: "FK_Seats_Halls_HallID",
                        column: x => x.HallID,
                        principalTable: "Halls",
                        principalColumn: "HallID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieSessions",
                columns: table => new
                {
                    MovieSessionID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HallID = table.Column<long>(type: "bigint", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    SessionTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MovieID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieSessions", x => x.MovieSessionID);
                    table.ForeignKey(
                        name: "FK_MovieSessions_Halls_HallID",
                        column: x => x.HallID,
                        principalTable: "Halls",
                        principalColumn: "HallID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieSessions_Movies_MovieID",
                        column: x => x.MovieID,
                        principalTable: "Movies",
                        principalColumn: "MovieID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeatStatuses",
                columns: table => new
                {
                    SeatStatusID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieSessionID = table.Column<long>(type: "bigint", nullable: false),
                    SeatID = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    LockedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LockedUntil = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatStatuses", x => x.SeatStatusID);
                    table.ForeignKey(
                        name: "FK_SeatStatuses_MovieSessions_MovieSessionID",
                        column: x => x.MovieSessionID,
                        principalTable: "MovieSessions",
                        principalColumn: "MovieSessionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeatStatuses_Seats_SeatID",
                        column: x => x.SeatID,
                        principalTable: "Seats",
                        principalColumn: "SeatID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieSessions_HallID",
                table: "MovieSessions",
                column: "HallID");

            migrationBuilder.CreateIndex(
                name: "IX_MovieSessions_MovieID",
                table: "MovieSessions",
                column: "MovieID");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_HallID",
                table: "Seats",
                column: "HallID");

            migrationBuilder.CreateIndex(
                name: "IX_SeatStatuses_MovieSessionID",
                table: "SeatStatuses",
                column: "MovieSessionID");

            migrationBuilder.CreateIndex(
                name: "IX_SeatStatuses_SeatID",
                table: "SeatStatuses",
                column: "SeatID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SeatStatuses");

            migrationBuilder.DropTable(
                name: "MovieSessions");

            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Halls");
        }
    }
}
