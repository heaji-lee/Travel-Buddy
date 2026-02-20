using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TravelBuddy.Migrations {
    /// <inheritdoc />
    public partial class AddTripItineraries : Migration {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.CreateTable(
                name: "trip_itineraries",
                columns: table => new {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    trip_id = table.Column<int>(type: "integer", nullable: false),
                    day_number = table.Column<int>(type: "integer", nullable: false),
                    notes = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table => {
                    table.PrimaryKey("PK_trip_itineraries", x => x.id);
                    table.ForeignKey(
                        name: "FK_trip_itineraries_trips_trip_id",
                        column: x => x.trip_id,
                        principalTable: "trips",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_trip_itineraries_trip_id",
                table: "trip_itineraries",
                column: "trip_id");

            migrationBuilder.CreateIndex(
                name: "IX_trip_itineraries_trip_id_day_number",
                table: "trip_itineraries",
                columns: new[] { "trip_id", "day_number" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropTable(
                name: "trip_itineraries");
        }
    }
}
