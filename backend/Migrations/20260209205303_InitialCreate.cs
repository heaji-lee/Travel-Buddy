using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TravelBuddy.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "companions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_companions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "interests",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_interests", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "travel_styles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_travel_styles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "trips",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    city = table.Column<string>(type: "text", nullable: false),
                    start_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trips", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "trip_companions",
                columns: table => new
                {
                    TripId = table.Column<int>(type: "integer", nullable: false),
                    CompanionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trip_companions", x => new { x.TripId, x.CompanionId });
                    table.ForeignKey(
                        name: "FK_trip_companions_companions_CompanionId",
                        column: x => x.CompanionId,
                        principalTable: "companions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_trip_companions_trips_TripId",
                        column: x => x.TripId,
                        principalTable: "trips",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "trip_interests",
                columns: table => new
                {
                    TripId = table.Column<int>(type: "integer", nullable: false),
                    InterestId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trip_interests", x => new { x.TripId, x.InterestId });
                    table.ForeignKey(
                        name: "FK_trip_interests_interests_InterestId",
                        column: x => x.InterestId,
                        principalTable: "interests",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_trip_interests_trips_TripId",
                        column: x => x.TripId,
                        principalTable: "trips",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "trip_travel_styles",
                columns: table => new
                {
                    TripId = table.Column<int>(type: "integer", nullable: false),
                    TravelStyleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trip_travel_styles", x => new { x.TripId, x.TravelStyleId });
                    table.ForeignKey(
                        name: "FK_trip_travel_styles_travel_styles_TravelStyleId",
                        column: x => x.TravelStyleId,
                        principalTable: "travel_styles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_trip_travel_styles_trips_TripId",
                        column: x => x.TripId,
                        principalTable: "trips",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_trip_companions_CompanionId",
                table: "trip_companions",
                column: "CompanionId");

            migrationBuilder.CreateIndex(
                name: "IX_trip_companions_TripId_CompanionId",
                table: "trip_companions",
                columns: new[] { "TripId", "CompanionId" });

            migrationBuilder.CreateIndex(
                name: "IX_trip_interests_InterestId",
                table: "trip_interests",
                column: "InterestId");

            migrationBuilder.CreateIndex(
                name: "IX_trip_interests_TripId_InterestId",
                table: "trip_interests",
                columns: new[] { "TripId", "InterestId" });

            migrationBuilder.CreateIndex(
                name: "IX_trip_travel_styles_TravelStyleId",
                table: "trip_travel_styles",
                column: "TravelStyleId");

            migrationBuilder.CreateIndex(
                name: "IX_trip_travel_styles_TripId_TravelStyleId",
                table: "trip_travel_styles",
                columns: new[] { "TripId", "TravelStyleId" });

            migrationBuilder.CreateIndex(
                name: "IX_trips_city",
                table: "trips",
                column: "city");

            migrationBuilder.CreateIndex(
                name: "IX_trips_end_at",
                table: "trips",
                column: "end_at");

            migrationBuilder.CreateIndex(
                name: "IX_trips_start_at",
                table: "trips",
                column: "start_at");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "trip_companions");

            migrationBuilder.DropTable(
                name: "trip_interests");

            migrationBuilder.DropTable(
                name: "trip_travel_styles");

            migrationBuilder.DropTable(
                name: "companions");

            migrationBuilder.DropTable(
                name: "interests");

            migrationBuilder.DropTable(
                name: "travel_styles");

            migrationBuilder.DropTable(
                name: "trips");
        }
    }
}
