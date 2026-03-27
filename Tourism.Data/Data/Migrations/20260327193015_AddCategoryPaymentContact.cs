using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Tourism.Data.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryPaymentContact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Tours",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IconClass = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Method = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransactionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaidAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "IconClass", "Name" },
                values: new object[,]
                {
                    { 1, "Explore ancient ruins, museums, and the stories behind the world's greatest civilisations.", "bi-bank", "Cultural & Historical" },
                    { 2, "Sun, sea, and sand — the finest coastal and island escapes in Europe.", "bi-water", "Beach & Island" },
                    { 3, "Compact, action-packed getaways to Europe's most vibrant and charming cities.", "bi-buildings", "City Break" },
                    { 4, "Multi-destination road trips, trekking routes, and experiences off the beaten path.", "bi-compass", "Adventure" },
                    { 5, "Premium hotels, skip-the-line access, private guides, and unforgettable fine dining.", "bi-stars", "Luxury" },
                    { 6, "Kid-friendly itineraries designed to create memories for the whole family.", "bi-people", "Family" }
                });

            migrationBuilder.InsertData(
                table: "Destinations",
                columns: new[] { "Id", "Country", "Description", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { 11, "Portugal", "A city of seven hills — historic trams, Fado music, Moorish castles, and the world's finest custard tarts.", "https://images.unsplash.com/photo-1558370781-d6196949e317?w=800&q=80", "Lisbon" },
                    { 12, "Hungary", "The Pearl of the Danube — grand thermal baths, stunning Parliament, ruin bars, and a vibrant nightlife scene.", "https://images.unsplash.com/photo-1541343672885-9be56236302a?w=800&q=80", "Budapest" },
                    { 13, "Greece", "The jewel of the Cyclades — windmills, whitewashed alleys, vibrant beach clubs, and crystal-clear Aegean waters.", "https://images.unsplash.com/photo-1601581875309-fafbf2d3ed3a?w=800&q=80", "Mykonos" }
                });

            migrationBuilder.UpdateData(
                table: "TourImages",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1552832230-c0197dd411b5?w=1200&q=80");

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 1,
                column: "CategoryId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 2,
                column: "CategoryId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 3,
                column: "CategoryId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 4,
                column: "CategoryId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 5,
                column: "CategoryId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 6,
                column: "CategoryId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 7,
                column: "CategoryId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 8,
                column: "CategoryId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 9,
                column: "CategoryId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 10,
                column: "CategoryId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 11,
                column: "CategoryId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 12,
                column: "CategoryId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 13,
                column: "CategoryId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 14,
                column: "CategoryId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CategoryId", "ImageUrl" },
                values: new object[] { 4, "https://images.unsplash.com/photo-1590147534648-1ac5cca59621?w=800&q=80" });

            migrationBuilder.InsertData(
                table: "Tours",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "DestinationId", "DiscountPercent", "DurationDays", "EndDate", "ImageUrl", "MaxParticipants", "PricePerPerson", "StartDate", "Title", "TourOperatorId" },
                values: new object[,]
                {
                    { 16, 3, new DateTime(2026, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "5 magical days in Lisbon — ride the iconic Tram 28 through Alfama, explore Belém Tower, taste pastel de nata in historic cafés, and watch the sun set over the Tagus river.", 11, 10m, 5, new DateTime(2026, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://images.unsplash.com/photo-1558370781-d6196949e317?w=800&q=80", 20, 749m, new DateTime(2026, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lisbon Highlights", 2 },
                    { 17, 3, new DateTime(2026, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "A 7-day journey through Portugal's two most beloved cities — cobblestone Lisbon and the port wine cellars of Porto, with a day trip to Sintra's fairy-tale palaces.", 11, 0m, 7, new DateTime(2026, 6, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://images.unsplash.com/photo-1555881400-74d7acaacd8b?w=800&q=80", 18, 999m, new DateTime(2026, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lisbon & Porto — Atlantic Coast", 3 },
                    { 18, 3, new DateTime(2026, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "4 days in one of Europe's most breathtaking capitals — Buda Castle, the Parliament building, a relaxing soak in Széchenyi Baths, and an evening cruise on the glittering Danube.", 12, 0m, 4, new DateTime(2026, 4, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://images.unsplash.com/photo-1541343672885-9be56236302a?w=800&q=80", 22, 649m, new DateTime(2026, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Budapest Royal", 1 },
                    { 19, 4, new DateTime(2026, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "6 days combining the grandeur of Budapest with a relaxing escape to Lake Balaton — Central Europe's largest lake. Wine tastings, spa villages, and stunning lakeside sunsets included.", 12, 12m, 6, new DateTime(2026, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://images.unsplash.com/photo-1549555340-9f9e3b0c5b0e?w=800&q=80", 16, 849m, new DateTime(2026, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Budapest & Lake Balaton", 5 },
                    { 20, 2, new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "7 sun-soaked days on the most glamorous island in the Cyclades — party beaches, windmill sunsets, fresh seafood by the water, and day trips to the sacred island of Delos.", 13, 0m, 7, new DateTime(2026, 7, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://images.unsplash.com/photo-1601581875309-fafbf2d3ed3a?w=800&q=80", 12, 1299m, new DateTime(2026, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mykonos Escape", 2 },
                    { 21, 5, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "The ultimate Greek island experience — 4 nights on vibrant Mykonos followed by 4 nights on dreamy Santorini. Ferry included, boutique hotel, private sunset tour.", 13, 8m, 8, new DateTime(2026, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://images.unsplash.com/photo-1570077188670-e3a8d69ac5ff?w=800&q=80", 10, 1799m, new DateTime(2026, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mykonos & Santorini — Island Duo", 4 },
                    { 22, 1, new DateTime(2026, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "See all the must-see sights of Rome without breaking the bank — Colosseum, Pantheon, Piazza Navona, and the Vatican, with comfortable 3* accommodation and daily breakfast.", 1, 20m, 4, new DateTime(2026, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://images.unsplash.com/photo-1515542622106-78bda8ba0e5b?w=800&q=80", 30, 549m, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rome Budget Explorer", 1 },
                    { 23, 2, new DateTime(2026, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "An epic 10-day island-hopping adventure — Athens, Mykonos, Paros, and Santorini. Ferry passes, guided excursions, and handpicked boutique hotels all included.", 5, 0m, 10, new DateTime(2026, 9, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://images.unsplash.com/photo-1533105079780-92b9be482077?w=800&q=80", 14, 1599m, new DateTime(2026, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Greek Island Hopping", 2 },
                    { 24, 4, new DateTime(2026, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "8 days across the Iberian Peninsula — Barcelona, Madrid, Seville, and Lisbon. High-speed trains, flamenco show, wine tasting, and a blend of Moorish and modern Europe.", 3, 0m, 8, new DateTime(2026, 10, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://images.unsplash.com/photo-1558642452-9d2a7deb7f62?w=800&q=80", 20, 1249m, new DateTime(2026, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Spain & Portugal — Iberian Grand Tour", 3 },
                    { 25, 3, new DateTime(2026, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Experience Prague in its most enchanting season — Christmas markets, steaming mulled wine, snow-covered rooftops, and the Old Town Square glowing in festive lights.", 6, 15m, 4, new DateTime(2026, 12, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://images.unsplash.com/photo-1467269204594-9661b134dd2b?w=800&q=80", 30, 499m, new DateTime(2026, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Winter Magic in Prague", 3 }
                });

            migrationBuilder.InsertData(
                table: "TourImages",
                columns: new[] { "Id", "ImageUrl", "SortOrder", "TourId" },
                values: new object[,]
                {
                    { 53, "https://images.unsplash.com/photo-1558370781-d6196949e317?w=1200&q=80", 1, 16 },
                    { 54, "https://images.unsplash.com/photo-1555881400-74d7acaacd8b?w=1200&q=80", 2, 16 },
                    { 55, "https://images.unsplash.com/photo-1513735492246-483525079686?w=1200&q=80", 3, 16 },
                    { 56, "https://images.unsplash.com/photo-1555881400-74d7acaacd8b?w=1200&q=80", 1, 17 },
                    { 57, "https://images.unsplash.com/photo-1558370781-d6196949e317?w=1200&q=80", 2, 17 },
                    { 58, "https://images.unsplash.com/photo-1513735492246-483525079686?w=1200&q=80", 3, 17 },
                    { 59, "https://images.unsplash.com/photo-1541343672885-9be56236302a?w=1200&q=80", 1, 18 },
                    { 60, "https://images.unsplash.com/photo-1565426873118-a17ed65d74b9?w=1200&q=80", 2, 18 },
                    { 61, "https://images.unsplash.com/photo-1507525428034-b723cf961d3e?w=1200&q=80", 3, 18 },
                    { 62, "https://images.unsplash.com/photo-1541343672885-9be56236302a?w=1200&q=80", 1, 19 },
                    { 63, "https://images.unsplash.com/photo-1549555340-9f9e3b0c5b0e?w=1200&q=80", 2, 19 },
                    { 64, "https://images.unsplash.com/photo-1516496636080-14fb876e029d?w=1200&q=80", 3, 19 },
                    { 65, "https://images.unsplash.com/photo-1601581875309-fafbf2d3ed3a?w=1200&q=80", 1, 20 },
                    { 66, "https://images.unsplash.com/photo-1533105079780-92b9be482077?w=1200&q=80", 2, 20 },
                    { 67, "https://images.unsplash.com/photo-1613395877344-13d4a8e0d49e?w=1200&q=80", 3, 20 },
                    { 68, "https://images.unsplash.com/photo-1570077188670-e3a8d69ac5ff?w=1200&q=80", 1, 21 },
                    { 69, "https://images.unsplash.com/photo-1601581875309-fafbf2d3ed3a?w=1200&q=80", 2, 21 },
                    { 70, "https://images.unsplash.com/photo-1613395877344-13d4a8e0d49e?w=1200&q=80", 3, 21 },
                    { 71, "https://images.unsplash.com/photo-1515542622106-78bda8ba0e5b?w=1200&q=80", 1, 22 },
                    { 72, "https://images.unsplash.com/photo-1552832230-c0197dd311b5?w=1200&q=80", 2, 22 },
                    { 73, "https://images.unsplash.com/photo-1571366343168-631c5bcca7a4?w=1200&q=80", 3, 22 },
                    { 74, "https://images.unsplash.com/photo-1533105079780-92b9be482077?w=1200&q=80", 1, 23 },
                    { 75, "https://images.unsplash.com/photo-1555993539-1732b0258235?w=1200&q=80", 2, 23 },
                    { 76, "https://images.unsplash.com/photo-1613395877344-13d4a8e0d49e?w=1200&q=80", 3, 23 },
                    { 77, "https://images.unsplash.com/photo-1558642452-9d2a7deb7f62?w=1200&q=80", 1, 24 },
                    { 78, "https://images.unsplash.com/photo-1539037116277-4db20889f2d4?w=1200&q=80", 2, 24 },
                    { 79, "https://images.unsplash.com/photo-1558370781-d6196949e317?w=1200&q=80", 3, 24 },
                    { 80, "https://images.unsplash.com/photo-1467269204594-9661b134dd2b?w=1200&q=80", 1, 25 },
                    { 81, "https://images.unsplash.com/photo-1519677100203-a0e668c92439?w=1200&q=80", 2, 25 },
                    { 82, "https://images.unsplash.com/photo-1541849546-216549ae216d?w=1200&q=80", 3, 25 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tours_CategoryId",
                table: "Tours",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_BookingId",
                table: "Payments",
                column: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_Categories_CategoryId",
                table: "Tours",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tours_Categories_CategoryId",
                table: "Tours");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "ContactMessages");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Tours_CategoryId",
                table: "Tours");

            migrationBuilder.DeleteData(
                table: "TourImages",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "TourImages",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "TourImages",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "TourImages",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "TourImages",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "TourImages",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "TourImages",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "TourImages",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "TourImages",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "TourImages",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "TourImages",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "TourImages",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "TourImages",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "TourImages",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "TourImages",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "TourImages",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "TourImages",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "TourImages",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "TourImages",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "TourImages",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "TourImages",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "TourImages",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "TourImages",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "TourImages",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "TourImages",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "TourImages",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "TourImages",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "TourImages",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "TourImages",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "TourImages",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Tours");

            migrationBuilder.UpdateData(
                table: "TourImages",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1552832230-c0197dd311b5?w=1200&q=80");

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 15,
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1590147534648-1ac5cca59621?q=80&w=1374&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D");
        }
    }
}
