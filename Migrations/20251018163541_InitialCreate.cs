using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HillarysHairCare.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
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
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stylists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stylists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerId = table.Column<int>(type: "integer", nullable: false),
                    StylistId = table.Column<int>(type: "integer", nullable: false),
                    AppointmentDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsCanceled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_Stylists_StylistId",
                        column: x => x.StylistId,
                        principalTable: "Stylists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppointmentServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AppointmentId = table.Column<int>(type: "integer", nullable: false),
                    ServiceId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppointmentServices_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppointmentServices_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "john.smith@example.com", "John", "Smith", "615-555-0101" },
                    { 2, "emma.davis@example.com", "Emma", "Davis", "615-555-0102" },
                    { 3, "michael.brown@example.com", "Michael", "Brown", "615-555-0103" },
                    { 4, "sophia.garcia@example.com", "Sophia", "Garcia", "615-555-0104" },
                    { 5, "william.miller@example.com", "William", "Miller", "615-555-0105" },
                    { 6, "olivia.taylor@example.com", "Olivia", "Taylor", "615-555-0106" },
                    { 7, "james.anderson@example.com", "James", "Anderson", "615-555-0107" },
                    { 8, "isabella.thomas@example.com", "Isabella", "Thomas", "615-555-0108" },
                    { 9, "benjamin.jackson@example.com", "Benjamin", "Jackson", "615-555-0109" },
                    { 10, "mia.white@example.com", "Mia", "White", "615-555-0110" },
                    { 11, "lucas.harris@example.com", "Lucas", "Harris", "615-555-0111" },
                    { 12, "charlotte.martin@example.com", "Charlotte", "Martin", "615-555-0112" },
                    { 13, "henry.thompson@example.com", "Henry", "Thompson", "615-555-0113" },
                    { 14, "amelia.moore@example.com", "Amelia", "Moore", "615-555-0114" },
                    { 15, "alexander.lee@example.com", "Alexander", "Lee", "615-555-0115" }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Basic haircut and style", "Haircut", 45.00m },
                    { 2, "Full hair coloring service", "Color", 120.00m },
                    { 3, "Partial highlights", "Highlights", 85.00m },
                    { 4, "Beard shaping and trim", "Beard Trim", 25.00m },
                    { 5, "Deep conditioning treatment", "Deep Conditioning", 35.00m }
                });

            migrationBuilder.InsertData(
                table: "Stylists",
                columns: new[] { "Id", "FirstName", "IsActive", "LastName" },
                values: new object[,]
                {
                    { 1, "Sarah", true, "Johnson" },
                    { 2, "Mike", true, "Chen" },
                    { 3, "Emily", false, "Rodriguez" },
                    { 4, "Jessica", true, "Martinez" },
                    { 5, "David", true, "Thompson" },
                    { 6, "Amanda", true, "Wilson" },
                    { 7, "Kevin", true, "Anderson" }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "AppointmentDate", "CustomerId", "IsCanceled", "StylistId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 20, 10, 0, 0, 0, DateTimeKind.Unspecified), 1, false, 1 },
                    { 2, new DateTime(2025, 10, 20, 14, 0, 0, 0, DateTimeKind.Unspecified), 2, false, 2 },
                    { 3, new DateTime(2025, 10, 21, 11, 0, 0, 0, DateTimeKind.Unspecified), 3, true, 1 },
                    { 4, new DateTime(2025, 10, 22, 9, 0, 0, 0, DateTimeKind.Unspecified), 4, false, 4 },
                    { 5, new DateTime(2025, 10, 22, 13, 0, 0, 0, DateTimeKind.Unspecified), 5, false, 5 },
                    { 6, new DateTime(2025, 10, 23, 10, 0, 0, 0, DateTimeKind.Unspecified), 6, false, 6 },
                    { 7, new DateTime(2025, 10, 23, 15, 0, 0, 0, DateTimeKind.Unspecified), 7, false, 7 },
                    { 8, new DateTime(2025, 10, 24, 11, 0, 0, 0, DateTimeKind.Unspecified), 8, true, 1 }
                });

            migrationBuilder.InsertData(
                table: "AppointmentServices",
                columns: new[] { "Id", "AppointmentId", "ServiceId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 4 },
                    { 3, 2, 2 },
                    { 4, 2, 5 },
                    { 5, 3, 1 },
                    { 6, 4, 3 },
                    { 7, 4, 5 },
                    { 8, 5, 1 },
                    { 9, 5, 4 },
                    { 10, 6, 2 },
                    { 11, 7, 1 },
                    { 12, 8, 2 },
                    { 13, 8, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_CustomerId",
                table: "Appointments",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_StylistId",
                table: "Appointments",
                column: "StylistId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentServices_AppointmentId",
                table: "AppointmentServices",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentServices_ServiceId",
                table: "AppointmentServices",
                column: "ServiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointmentServices");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Stylists");
        }
    }
}
