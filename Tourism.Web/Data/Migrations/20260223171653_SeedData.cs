using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Tourism.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Destinations",
                columns: new[] { "Id", "Country", "Description", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { 1, "Италия", "Вечният град с хиляди години история, включително Колизеума, Ватикана и Трефонтана.", "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c0/Rome_Skyline.jpg/1200px-Rome_Skyline.jpg", "Рим" },
                    { 2, "Франция", "Градът на любовта с Айфеловата кула, Лувъра и романтичната атмосфера.", "https://upload.wikimedia.org/wikipedia/commons/thumb/4/4b/La_Tour_Eiffel_vue_de_la_Tour_Saint-Jacques%2C_Paris_August_2014_%282%29.jpg/800px-La_Tour_Eiffel_vue_de_la_Tour_Saint-Jacques%2C_Paris_August_2014_%282%29.jpg", "Париж" },
                    { 3, "Испания", "Слънчев крайбрежен град с архитектурата на Гауди и оживения Лас Рамблас.", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1f/Sagrada_Familia_01.jpg/800px-Sagrada_Familia_01.jpg", "Барселона" },
                    { 4, "Хърватия", "Перлата на Адриатика с невероятни стари стени и кристално море.", "https://upload.wikimedia.org/wikipedia/commons/thumb/6/6e/Dubrovnik_-_Old_City_Walls.jpg/1200px-Dubrovnik_-_Old_City_Walls.jpg", "Дубровник" },
                    { 5, "Гърция", "Люлката на цивилизацията с Акропола, Партенона и вкусната средиземноморска кухня.", "https://upload.wikimedia.org/wikipedia/commons/thumb/d/da/The_Parthenon_in_Athens.jpg/1200px-The_Parthenon_in_Athens.jpg", "Атина" }
                });

            migrationBuilder.InsertData(
                table: "TourOperators",
                columns: new[] { "Id", "CreatedAt", "Description", "Email", "LogoUrl", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Водеща туристическа агенция с над 20 години опит в организирането на незабравими европейски турове.", "info@balkantravel.bg", "", "Балкан Травел", "+359 2 123 4567" },
                    { 2, new DateTime(2018, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Специалисти в средиземноморския туризъм с индивидуален подход към всеки клиент.", "contact@suntourism.bg", "", "Сън Туризъм", "+359 2 987 6543" },
                    { 3, new DateTime(2015, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Организираме групови и индивидуални пътувания до най-красивите градове на Европа.", "hello@europaexplorer.bg", "", "Европа Експлорър", "+359 2 555 1234" }
                });

            migrationBuilder.InsertData(
                table: "Tours",
                columns: new[] { "Id", "CreatedAt", "Description", "DestinationId", "DurationDays", "EndDate", "ImageUrl", "MaxParticipants", "PricePerPerson", "StartDate", "Title", "TourOperatorId" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Разгледайте Вечния град с нашия 5-дневен тур включващ Колизеума, Ватикана, Трефонтана и много повече.", 1, 5, new DateTime(2026, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 20, 899m, new DateTime(2026, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Класически Рим", 1 },
                    { 2, new DateTime(2026, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "7 дни в Града на светлините — Айфелова кула, Лувър, Монмартър и круиз по Сена.", 2, 7, new DateTime(2026, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 15, 1199m, new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Романтичен Париж", 2 },
                    { 3, new DateTime(2026, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Открийте магията на Барселона — Саграда Фамилия, Парк Гуел, Лас Рамблас и плажовете.", 3, 4, new DateTime(2026, 6, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 25, 799m, new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Барселона и Гауди", 3 },
                    { 4, new DateTime(2026, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Дубровник и хърватското крайбрежие — стари стени, островчета и кристално море.", 4, 6, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 18, 699m, new DateTime(2026, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Адриатическа перла", 1 },
                    { 5, new DateTime(2026, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Потопете се в историята на Древна Гърция — Акропол, Национален музей, Плака и о-в Хидра.", 5, 5, new DateTime(2026, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 22, 649m, new DateTime(2026, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Атинско приключение", 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "TourOperators",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TourOperators",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TourOperators",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
