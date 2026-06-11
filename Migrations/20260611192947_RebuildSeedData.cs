using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HillarysHairCare.Migrations
{
    /// <inheritdoc />
    public partial class RebuildSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checkouts_Materials_CustomerId",
                table: "Checkouts");

            migrationBuilder.DropForeignKey(
                name: "FK_Checkouts_Patrons_StylistId",
                table: "Checkouts");

            migrationBuilder.DropForeignKey(
                name: "FK_Genres_Checkouts_AppointmentId",
                table: "Genres");

            migrationBuilder.DropForeignKey(
                name: "FK_Genres_MaterialTypes_ServiceId",
                table: "Genres");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Patrons",
                table: "Patrons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MaterialTypes",
                table: "MaterialTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Materials",
                table: "Materials");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Genres",
                table: "Genres");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Checkouts",
                table: "Checkouts");

            migrationBuilder.RenameTable(
                name: "Patrons",
                newName: "Stylists");

            migrationBuilder.RenameTable(
                name: "MaterialTypes",
                newName: "Services");

            migrationBuilder.RenameTable(
                name: "Materials",
                newName: "Customers");

            migrationBuilder.RenameTable(
                name: "Genres",
                newName: "AppointmentServices");

            migrationBuilder.RenameTable(
                name: "Checkouts",
                newName: "Appointments");

            migrationBuilder.RenameIndex(
                name: "IX_Genres_ServiceId",
                table: "AppointmentServices",
                newName: "IX_AppointmentServices_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Genres_AppointmentId",
                table: "AppointmentServices",
                newName: "IX_AppointmentServices_AppointmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Checkouts_StylistId",
                table: "Appointments",
                newName: "IX_Appointments_StylistId");

            migrationBuilder.RenameIndex(
                name: "IX_Checkouts_CustomerId",
                table: "Appointments",
                newName: "IX_Appointments_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stylists",
                table: "Stylists",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Services",
                table: "Services",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppointmentServices",
                table: "AppointmentServices",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Appointments",
                table: "Appointments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Customers_CustomerId",
                table: "Appointments",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Stylists_StylistId",
                table: "Appointments",
                column: "StylistId",
                principalTable: "Stylists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentServices_Appointments_AppointmentId",
                table: "AppointmentServices",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentServices_Services_ServiceId",
                table: "AppointmentServices",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Customers_CustomerId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Stylists_StylistId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentServices_Appointments_AppointmentId",
                table: "AppointmentServices");

            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentServices_Services_ServiceId",
                table: "AppointmentServices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stylists",
                table: "Stylists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Services",
                table: "Services");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppointmentServices",
                table: "AppointmentServices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Appointments",
                table: "Appointments");

            migrationBuilder.RenameTable(
                name: "Stylists",
                newName: "Patrons");

            migrationBuilder.RenameTable(
                name: "Services",
                newName: "MaterialTypes");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "Materials");

            migrationBuilder.RenameTable(
                name: "AppointmentServices",
                newName: "Genres");

            migrationBuilder.RenameTable(
                name: "Appointments",
                newName: "Checkouts");

            migrationBuilder.RenameIndex(
                name: "IX_AppointmentServices_ServiceId",
                table: "Genres",
                newName: "IX_Genres_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_AppointmentServices_AppointmentId",
                table: "Genres",
                newName: "IX_Genres_AppointmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_StylistId",
                table: "Checkouts",
                newName: "IX_Checkouts_StylistId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_CustomerId",
                table: "Checkouts",
                newName: "IX_Checkouts_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Patrons",
                table: "Patrons",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MaterialTypes",
                table: "MaterialTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Materials",
                table: "Materials",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Genres",
                table: "Genres",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Checkouts",
                table: "Checkouts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Checkouts_Materials_CustomerId",
                table: "Checkouts",
                column: "CustomerId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Checkouts_Patrons_StylistId",
                table: "Checkouts",
                column: "StylistId",
                principalTable: "Patrons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Genres_Checkouts_AppointmentId",
                table: "Genres",
                column: "AppointmentId",
                principalTable: "Checkouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Genres_MaterialTypes_ServiceId",
                table: "Genres",
                column: "ServiceId",
                principalTable: "MaterialTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
