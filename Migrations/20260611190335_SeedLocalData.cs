using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HillarysHairCare.Migrations
{
    /// <inheritdoc />
    public partial class SeedLocalData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaterialTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Patrons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    isActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patrons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Checkouts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerId = table.Column<int>(type: "integer", nullable: false),
                    StylistId = table.Column<int>(type: "integer", nullable: false),
                    AppointmentTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsCancelled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checkouts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Checkouts_Materials_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Checkouts_Patrons_StylistId",
                        column: x => x.StylistId,
                        principalTable: "Patrons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AppointmentId = table.Column<int>(type: "integer", nullable: false),
                    ServiceId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Genres_Checkouts_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Checkouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Genres_MaterialTypes_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "MaterialTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "MaterialTypes",
                columns: new[] { "Id", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Basic cut and trim", "Haircut", 25.00m },
                    { 2, "Full single-color treatment", "Color Treatment", 75.00m },
                    { 3, "Permanent wave styling", "Perm", 60.00m },
                    { 4, "Partial or full highlights", "Highlights", 90.00m },
                    { 5, "Wash and blowout style", "Blowout", 35.00m },
                    { 6, "Shampoo, condition, and style", "Shampoo & Style", 45.00m },
                    { 7, "Beard shaping and trim", "Beard Trim", 15.00m },
                    { 8, "Haircut for ages 12 and under", "Kids Cut", 18.00m },
                    { 9, "Color, highlights, cut, and style package", "Full Makeover", 200.00m }
                });

            migrationBuilder.InsertData(
                table: "Materials",
                columns: new[] { "Id", "Email", "Name", "Phone" },
                values: new object[,]
                {
                    { 1, "homer@springfield.net", "Homer Simpson", "555-0101" },
                    { 2, "bart@springfield.net", "Bart Simpson", "555-0102" },
                    { 3, "lisa@springfield.net", "Lisa Simpson", "555-0103" },
                    { 4, "ned@leftorium.com", "Ned Flanders", "555-0104" },
                    { 5, "mburns@burnscorp.com", "Montgomery Burns", "555-0105" },
                    { 6, "apu@kwikmart.com", "Apu Nahasapeemapetilon", "555-0106" },
                    { 7, "cwiggum@spd.gov", "Chief Clancy Wiggum", "555-0107" },
                    { 8, "krusty@krustyco.com", "Krusty the Clown", "555-0108" },
                    { 9, "ralph@spd.gov", "Ralph Wiggum", "555-0109" },
                    { 10, "", "Hans Moleman", "555-0110" }
                });

            migrationBuilder.InsertData(
                table: "Patrons",
                columns: new[] { "Id", "Name", "isActive" },
                values: new object[,]
                {
                    { 1, "Marge Simpson", true },
                    { 2, "Selma Bouvier", true },
                    { 3, "Patty Bouvier", true },
                    { 4, "Lindsey Naegle", true },
                    { 5, "Helen Lovejoy", true },
                    { 6, "Sideshow Bob", false },
                    { 7, "Snake Jailbird", false }
                });

            migrationBuilder.InsertData(
                table: "Checkouts",
                columns: new[] { "Id", "AppointmentTime", "CustomerId", "IsCancelled", "StylistId" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 5, 10, 10, 0, 0, 0, DateTimeKind.Unspecified), 1, false, 1 },
                    { 2, new DateTime(2026, 5, 16, 14, 0, 0, 0, DateTimeKind.Unspecified), 2, false, 2 },
                    { 3, new DateTime(2026, 6, 1, 13, 0, 0, 0, DateTimeKind.Unspecified), 3, false, 1 },
                    { 4, new DateTime(2026, 6, 5, 9, 0, 0, 0, DateTimeKind.Unspecified), 5, false, 4 },
                    { 5, new DateTime(2026, 6, 18, 10, 30, 0, 0, DateTimeKind.Unspecified), 6, false, 5 },
                    { 6, new DateTime(2026, 6, 25, 11, 0, 0, 0, DateTimeKind.Unspecified), 9, false, 1 },
                    { 7, new DateTime(2026, 5, 20, 11, 0, 0, 0, DateTimeKind.Unspecified), 4, true, 3 },
                    { 8, new DateTime(2026, 6, 10, 15, 0, 0, 0, DateTimeKind.Unspecified), 1, true, 2 },
                    { 9, new DateTime(2026, 5, 28, 16, 0, 0, 0, DateTimeKind.Unspecified), 8, false, 6 },
                    { 10, null, 10, false, 3 }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "AppointmentId", "ServiceId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 7 },
                    { 3, 2, 8 },
                    { 4, 3, 4 },
                    { 5, 4, 9 },
                    { 6, 5, 1 },
                    { 7, 6, 8 },
                    { 8, 7, 6 },
                    { 9, 8, 1 },
                    { 10, 9, 3 },
                    { 11, 9, 2 },
                    { 12, 10, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Checkouts_CustomerId",
                table: "Checkouts",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Checkouts_StylistId",
                table: "Checkouts",
                column: "StylistId");

            migrationBuilder.CreateIndex(
                name: "IX_Genres_AppointmentId",
                table: "Genres",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Genres_ServiceId",
                table: "Genres",
                column: "ServiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Checkouts");

            migrationBuilder.DropTable(
                name: "MaterialTypes");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "Patrons");
        }
    }
}
