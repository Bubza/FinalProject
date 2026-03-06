using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Tourism.Data.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Destinations",
                columns: new[] { "Id", "Country", "Description", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { 1, "Италия", "Вечният град с хиляди години история — Колизеума, Ватикана и прочутата италианска кухня.", "https://images.unsplash.com/photo-1552832230-c0197dd311b5?w=800&q=80", "Рим" },
                    { 2, "Франция", "Градът на любовта с Айфеловата кула, Лувъра и незабравимата романтична атмосфера.", "https://images.unsplash.com/photo-1502602898657-3e91760cbb34?w=800&q=80", "Париж" },
                    { 3, "Испания", "Слънчев крайбрежен град с архитектурата на Гауди, оживения Лас Рамблас и прекрасни плажове.", "https://images.unsplash.com/photo-1539037116277-4db20889f2d4?w=800&q=80", "Барселона" },
                    { 4, "Хърватия", "Перлата на Адриатика — средновековни стени, кристално синьо море и живописни улички.", "https://images.unsplash.com/photo-1555990793-da11153b2473?w=800&q=80", "Дубровник" },
                    { 5, "Гърция", "Люлката на цивилизацията — Акрополът, Партенонът и богатата средиземноморска кухня.", "https://images.unsplash.com/photo-1555993539-1732b0258235?w=800&q=80", "Атина" },
                    { 6, "Чехия", "Градът на сто кули — приказна средновековна архитектура, Карловият мост и бохемска атмосфера.", "https://images.unsplash.com/photo-1519677100203-a0e668c92439?w=800&q=80", "Прага" },
                    { 7, "Турция", "Мостът между Европа и Азия — Света София, Гранд базарът и вкусовете на Ориента.", "https://images.unsplash.com/photo-1527838832700-5059252407fa?w=800&q=80", "Истанбул" },
                    { 8, "Нидерландия", "Градът на каналите — велосипеди, лалета, музеи и неповторима холандска архитектура.", "https://images.unsplash.com/photo-1534351590666-13e3e96b5017?w=800&q=80", "Амстердам" },
                    { 9, "Австрия", "Имперският град — дворци, опера, прочутите виенски кафенета и класическа музика.", "https://images.unsplash.com/photo-1516550893923-42d28e5677af?w=800&q=80", "Виена" },
                    { 10, "Гърция", "Вулканичен остров с бели сгради, сини куполи и едни от най-красивите залези в света.", "https://images.unsplash.com/photo-1613395877344-13d4a8e0d49e?w=800&q=80", "Санторини" }
                });

            migrationBuilder.InsertData(
                table: "TourOperators",
                columns: new[] { "Id", "CreatedAt", "Description", "Email", "LogoUrl", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Водеща туристическа агенция с над 20 години опит в организирането на групови и индивидуални пътувания.", "info@balkantravel.bg", "", "Балкан Травел", "+359 2 123 4567" },
                    { 2, new DateTime(2018, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Специалисти в средиземноморския и островен туризъм — море, слънце и незабравими преживявания.", "contact@suntourism.bg", "", "Сън Туризъм", "+359 2 987 6543" },
                    { 3, new DateTime(2015, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Групови и индивидуални пътувания до всички европейски дестинации с местни водачи.", "hello@europaexplorer.bg", "", "Европа Експлорър", "+359 2 555 1234" },
                    { 4, new DateTime(2017, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Екзотични дестинации, кръстопътни маршрути и незабравими приключения в Азия и Близкия изток.", "orient@orienttours.bg", "", "Ориент Турс", "+359 2 444 5678" },
                    { 5, new DateTime(2019, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Специалисти в планински и приключенски туризъм — трекинг, ски и еко пътувания.", "info@alpinepaths.bg", "", "Алпийски Преходи", "+359 2 777 9900" }
                });

            migrationBuilder.InsertData(
                table: "Tours",
                columns: new[] { "Id", "CreatedAt", "Description", "DestinationId", "DurationDays", "EndDate", "ImageUrl", "MaxParticipants", "PricePerPerson", "StartDate", "Title", "TourOperatorId" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "5-дневно пътуване до Вечния град. Посетете Колизеума, Форума, Ватикана и хвърлете монета в Фонтана ди Треви. Включени са квалифициран водач, хотел 4* и закуски.", 1, 5, new DateTime(2026, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://images.unsplash.com/photo-1552832230-c0197dd311b5?w=800&q=80", 20, 899m, new DateTime(2026, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Класически Рим", 1 },
                    { 2, new DateTime(2026, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ексклузивен 3-дневен уикенд тур с влизане без опашка във Ватиканските музеи, частна вечеря в Трастевере и нощувки в бутиков хотел в центъра.", 1, 3, new DateTime(2026, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://images.unsplash.com/photo-1568797629192-789acf8e4df3?w=800&q=80", 10, 1250m, new DateTime(2026, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Рим и Ватикана — VIP тур", 3 },
                    { 3, new DateTime(2026, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "7 дни в Града на светлините — Айфеловата кула, Лувъра, Монмартър и круиз по Сена. Перфектен за двойки и любители на изкуството.", 2, 7, new DateTime(2026, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://images.unsplash.com/photo-1502602898657-3e91760cbb34?w=800&q=80", 15, 1199m, new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Романтичен Париж", 2 },
                    { 4, new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Кратък 3-дневен уикенд в Париж — идеален за тези с малко отпуска. Самолет, хотел и основните забележителности включени.", 2, 3, new DateTime(2026, 6, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://images.unsplash.com/photo-1499856871958-5b9627545d1a?w=800&q=80", 20, 649m, new DateTime(2026, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Париж Уикенд", 1 },
                    { 5, new DateTime(2026, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Открийте магията на Барселона с посещения на Саграда Фамилия, парк Гуел, Каса Батло и оживения пазар Бокерия.", 3, 4, new DateTime(2026, 6, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://images.unsplash.com/photo-1539037116277-4db20889f2d4?w=800&q=80", 25, 799m, new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Барселона и Гауди", 3 },
                    { 6, new DateTime(2026, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "6 дни в Дубровник и по хърватското крайбрежие. Разходка по крепостните стени, екскурзия до остров Локрум и плаж на Будва.", 4, 6, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://images.unsplash.com/photo-1555990793-da11153b2473?w=800&q=80", 18, 699m, new DateTime(2026, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Адриатическа перла", 1 },
                    { 7, new DateTime(2026, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Потопете се в историята на Древна Гърция — Акропол, Националният музей, квартал Монастираки и вкусна гръцка вечеря с музика.", 5, 5, new DateTime(2026, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://images.unsplash.com/photo-1555993539-1732b0258235?w=800&q=80", 22, 649m, new DateTime(2026, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Атинско приключение", 2 },
                    { 8, new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "4 дни в приказната Прага — Карловият мост, Пражкият замък, Старото градско ядро и легендарните чешки бири в исторически пивници.", 6, 4, new DateTime(2026, 4, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://images.unsplash.com/photo-1519677100203-a0e668c92439?w=800&q=80", 30, 549m, new DateTime(2026, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Магията на Прага", 3 },
                    { 9, new DateTime(2026, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Комбиниран тур до две от най-красивите имперски столици — 4 дни в Прага и 4 дни във Виена с автобусен преход и хотели 3*.", 6, 8, new DateTime(2026, 9, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://images.unsplash.com/photo-1555576208-f2598fdc0639?w=800&q=80", 25, 980m, new DateTime(2026, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Прага и Виена — 8 дни", 1 },
                    { 10, new DateTime(2026, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "5 незабравими дни в Истанбул — Света София, Синята джамия, Гранд базарът, круиз по Босфора и автентична турска вечеря с танцьори.", 7, 5, new DateTime(2026, 5, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://images.unsplash.com/photo-1527838832700-5059252407fa?w=800&q=80", 20, 729m, new DateTime(2026, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Истанбул — Мостът на световете", 4 },
                    { 11, new DateTime(2026, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "4-дневен тур с круиз по каналите, посещение на музея на Ван Гог и Рийксмузеум, разходка с велосипеди и пазар за цветя.", 8, 4, new DateTime(2026, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://images.unsplash.com/photo-1534351590666-13e3e96b5017?w=800&q=80", 18, 819m, new DateTime(2026, 4, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Амстердам — Градът на каналите", 3 },
                    { 12, new DateTime(2026, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "5 дни в столицата на Хабсбургите — Дворецът Шьонбрун, Операта, Белведере, виенски кафенета и концерт с класическа музика.", 9, 5, new DateTime(2026, 10, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://images.unsplash.com/photo-1516550893923-42d28e5677af?w=800&q=80", 20, 899m, new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Имперска Виена", 1 },
                    { 13, new DateTime(2026, 2, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "7 дни на вулканичния остров Санторини — залезите от Ия, вулканичните плажове, дегустация на местни вина и круиз около острова.", 10, 7, new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://images.unsplash.com/photo-1613395877344-13d4a8e0d49e?w=800&q=80", 14, 1399m, new DateTime(2026, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Санторини — Остров на мечтите", 2 },
                    { 14, new DateTime(2026, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "3-дневен уикенд полет до Санторини — нощувки в традиционна кикладска вила, гледка към калдерата и частна яхтена обиколка.", 10, 3, new DateTime(2026, 8, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://images.unsplash.com/photo-1570077188670-e3a8d69ac5ff?w=800&q=80", 8, 850m, new DateTime(2026, 8, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Санторини Уикенд", 4 },
                    { 15, new DateTime(2026, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Епичен 10-дневен тур из Балканите — Атина, Солун, Охрид и Будва. Смесица от история, природа и автентична балканска кухня.", 5, 10, new DateTime(2026, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://images.unsplash.com/photo-1601599561213-832382fd07ba?w=800&q=80", 28, 1100m, new DateTime(2026, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Балкански Експрес — 10 дни", 5 }
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
                table: "Tours",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 15);

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
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 10);

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

            migrationBuilder.DeleteData(
                table: "TourOperators",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TourOperators",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
