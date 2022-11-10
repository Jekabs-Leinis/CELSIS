using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CELSIS.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlaceRatings",
                columns: table => new
                {
                    GooglePlaceId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RatingCount = table.Column<long>(type: "bigint", nullable: false),
                    Rating = table.Column<float>(type: "real", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaceRatings", x => x.GooglePlaceId);
                });

            migrationBuilder.CreateTable(
                name: "RouteRatings",
                columns: table => new
                {
                    GoogleRouteHash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RatingCount = table.Column<long>(type: "bigint", nullable: false),
                    Rating = table.Column<float>(type: "real", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteRatings", x => x.GoogleRouteHash);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlaceRatings");

            migrationBuilder.DropTable(
                name: "RouteRatings");
        }
    }
}
