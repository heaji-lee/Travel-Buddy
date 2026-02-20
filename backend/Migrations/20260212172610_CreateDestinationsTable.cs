using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TravelBuddy.Migrations {
    /// <inheritdoc />
    public partial class CreateDestinationsTable : Migration {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.CreateTable(
                name: "destinations",
                columns: table => new {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    city = table.Column<string>(type: "text", nullable: false),
                    country = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_destinations", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_destinations_city",
                table: "destinations",
                column: "city");

            migrationBuilder.CreateIndex(
                name: "IX_destinations_city_country",
                table: "destinations",
                columns: new[] { "city", "country" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_destinations_country",
                table: "destinations",
                column: "country");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropTable(
                name: "destinations");
        }
    }
}
