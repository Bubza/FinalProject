using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tourism.Data.Data.Migrations
{
    /// <inheritdoc />
    public partial class EnglishSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Country", "Description", "Name" },
                values: new object[] { "Italy", "The Eternal City with thousands of years of history — the Colosseum, the Vatican, and world-renowned Italian cuisine.", "Rome" });

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Country", "Description", "Name" },
                values: new object[] { "France", "The City of Love — the Eiffel Tower, the Louvre, and an unforgettable romantic atmosphere.", "Paris" });

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Country", "Description", "Name" },
                values: new object[] { "Spain", "A sun-drenched coastal city with Gaudí's iconic architecture, vibrant Las Ramblas, and beautiful beaches.", "Barcelona" });

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Country", "Description", "Name" },
                values: new object[] { "Croatia", "The Pearl of the Adriatic — medieval walls, crystal-clear sea, and picturesque cobblestone streets.", "Dubrovnik" });

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Country", "Description", "Name" },
                values: new object[] { "Greece", "The cradle of civilization — the Acropolis, the Parthenon, and rich Mediterranean cuisine.", "Athens" });

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Country", "Description", "Name" },
                values: new object[] { "Czech Republic", "The City of a Hundred Spires — fairy-tale medieval architecture, Charles Bridge, and a bohemian atmosphere.", "Prague" });

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Country", "Description", "Name" },
                values: new object[] { "Turkey", "The bridge between Europe and Asia — Hagia Sophia, the Grand Bazaar, and the flavors of the Orient.", "Istanbul" });

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Country", "Description", "Name" },
                values: new object[] { "Netherlands", "The City of Canals — bicycles, tulips, world-class museums, and unique Dutch architecture.", "Amsterdam" });

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Country", "Description", "Name" },
                values: new object[] { "Austria", "The Imperial City — grand palaces, opera houses, famous Viennese cafés, and classical music.", "Vienna" });

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Country", "Description", "Name" },
                values: new object[] { "Greece", "A volcanic island with whitewashed buildings, blue domes, and some of the most beautiful sunsets in the world.", "Santorini" });

            migrationBuilder.UpdateData(
                table: "TourOperators",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Name" },
                values: new object[] { "A leading travel agency with over 20 years of experience organizing group and individual tours.", "Balkan Travel" });

            migrationBuilder.UpdateData(
                table: "TourOperators",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Specialists in Mediterranean and island tourism — sea, sun, and unforgettable experiences.", "Sun Tourism" });

            migrationBuilder.UpdateData(
                table: "TourOperators",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Group and individual tours to all European destinations with local expert guides.", "Europa Explorer" });

            migrationBuilder.UpdateData(
                table: "TourOperators",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Exotic destinations, crossroads routes, and unforgettable adventures across Asia and the Middle East.", "Orient Tours" });

            migrationBuilder.UpdateData(
                table: "TourOperators",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Specialists in mountain and adventure tourism — trekking, skiing, and eco-travel.", "Alpine Paths" });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Title" },
                values: new object[] { "A 5-day journey to the Eternal City. Visit the Colosseum, the Roman Forum, the Vatican, and toss a coin in the Trevi Fountain. Includes expert guide, 4* hotel, and breakfast.", "Classic Rome" });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Title" },
                values: new object[] { "An exclusive 3-day weekend tour with skip-the-line entry to the Vatican Museums, a private dinner in Trastevere, and nights in a boutique hotel in the heart of Rome.", "Rome & Vatican — VIP Tour" });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Title" },
                values: new object[] { "7 days in the City of Light — the Eiffel Tower, the Louvre, Montmartre, and a cruise along the Seine. Perfect for couples and art lovers.", "Romantic Paris" });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "Title" },
                values: new object[] { "A short 3-day Paris weekend — ideal for those with limited time off. Flights, hotel, and all major highlights included.", "Paris Weekend" });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "Title" },
                values: new object[] { "Discover the magic of Barcelona with visits to the Sagrada Família, Park Güell, Casa Batlló, and the vibrant Boqueria market.", "Barcelona & Gaudí" });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Description", "Title" },
                values: new object[] { "6 days in Dubrovnik and along the Croatian coast. Walk the ancient city walls, take a day trip to Lokrum Island, and relax on the beaches of Budva.", "Adriatic Pearl" });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Description", "Title" },
                values: new object[] { "Immerse yourself in the history of Ancient Greece — the Acropolis, the National Museum, the Monastiraki district, and a traditional Greek dinner with live music.", "Athens Adventure" });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Description", "Title" },
                values: new object[] { "4 days in fairy-tale Prague — Charles Bridge, Prague Castle, the Old Town Square, and legendary Czech beers in historic pubs.", "Magic of Prague" });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Description", "Title" },
                values: new object[] { "A combined tour to two of Europe's most beautiful imperial capitals — 4 days in Prague and 4 days in Vienna, with coach transfer and 3* hotels.", "Prague & Vienna — 8 Days" });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Description", "Title" },
                values: new object[] { "5 unforgettable days in Istanbul — Hagia Sophia, the Blue Mosque, the Grand Bazaar, a Bosphorus cruise, and an authentic Turkish dinner with folk dancers.", "Istanbul — Bridge of Worlds" });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Description", "Title" },
                values: new object[] { "A 4-day tour with a canal cruise, visits to the Van Gogh Museum and Rijksmuseum, a bicycle ride through the city, and a flower market.", "Amsterdam — City of Canals" });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Description", "Title" },
                values: new object[] { "5 days in the Habsburg capital — Schönbrunn Palace, the State Opera, Belvedere, Viennese cafés, and a classical music concert.", "Imperial Vienna" });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Description", "Title" },
                values: new object[] { "7 days on the volcanic island of Santorini — sunsets from Oia, volcanic beaches, local wine tasting, and a cruise around the island.", "Santorini — Island of Dreams" });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Description", "Title" },
                values: new object[] { "A 3-day weekend flight to Santorini — nights in a traditional Cycladic villa overlooking the caldera, and a private yacht tour.", "Santorini Weekend" });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "Description", "Title" },
                values: new object[] { "An epic 10-day tour across the Balkans — Athens, Thessaloniki, Ohrid, and Budva. A blend of history, nature, and authentic Balkan cuisine.", "Balkan Express — 10 Days" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Country", "Description", "Name" },
                values: new object[] { "Италия", "Вечният град с хиляди години история — Колизеума, Ватикана и прочутата италианска кухня.", "Рим" });

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Country", "Description", "Name" },
                values: new object[] { "Франция", "Градът на любовта с Айфеловата кула, Лувъра и незабравимата романтична атмосфера.", "Париж" });

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Country", "Description", "Name" },
                values: new object[] { "Испания", "Слънчев крайбрежен град с архитектурата на Гауди, оживения Лас Рамблас и прекрасни плажове.", "Барселона" });

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Country", "Description", "Name" },
                values: new object[] { "Хърватия", "Перлата на Адриатика — средновековни стени, кристално синьо море и живописни улички.", "Дубровник" });

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Country", "Description", "Name" },
                values: new object[] { "Гърция", "Люлката на цивилизацията — Акрополът, Партенонът и богатата средиземноморска кухня.", "Атина" });

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Country", "Description", "Name" },
                values: new object[] { "Чехия", "Градът на сто кули — приказна средновековна архитектура, Карловият мост и бохемска атмосфера.", "Прага" });

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Country", "Description", "Name" },
                values: new object[] { "Турция", "Мостът между Европа и Азия — Света София, Гранд базарът и вкусовете на Ориента.", "Истанбул" });

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Country", "Description", "Name" },
                values: new object[] { "Нидерландия", "Градът на каналите — велосипеди, лалета, музеи и неповторима холандска архитектура.", "Амстердам" });

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Country", "Description", "Name" },
                values: new object[] { "Австрия", "Имперският град — дворци, опера, прочутите виенски кафенета и класическа музика.", "Виена" });

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Country", "Description", "Name" },
                values: new object[] { "Гърция", "Вулканичен остров с бели сгради, сини куполи и едни от най-красивите залези в света.", "Санторини" });

            migrationBuilder.UpdateData(
                table: "TourOperators",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Водеща туристическа агенция с над 20 години опит в организирането на групови и индивидуални пътувания.", "Балкан Травел" });

            migrationBuilder.UpdateData(
                table: "TourOperators",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Специалисти в средиземноморския и островен туризъм — море, слънце и незабравими преживявания.", "Сън Туризъм" });

            migrationBuilder.UpdateData(
                table: "TourOperators",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Групови и индивидуални пътувания до всички европейски дестинации с местни водачи.", "Европа Експлорър" });

            migrationBuilder.UpdateData(
                table: "TourOperators",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Екзотични дестинации, кръстопътни маршрути и незабравими приключения в Азия и Близкия изток.", "Ориент Турс" });

            migrationBuilder.UpdateData(
                table: "TourOperators",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Специалисти в планински и приключенски туризъм — трекинг, ски и еко пътувания.", "Алпийски Преходи" });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Title" },
                values: new object[] { "5-дневно пътуване до Вечния град. Посетете Колизеума, Форума, Ватикана и хвърлете монета в Фонтана ди Треви. Включени са квалифициран водач, хотел 4* и закуски.", "Класически Рим" });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Title" },
                values: new object[] { "Ексклузивен 3-дневен уикенд тур с влизане без опашка в Ватиканските музеи, частна вечеря в Трастевере и нощувки в бутиков хотел в центъра.", "Рим и Ватикана — VIP тур" });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Title" },
                values: new object[] { "7 дни в Града на светлините — Айфеловата кула, Лувъра, Монмартър и круиз по Сена. Перфектен за двойки и любители на изкуството.", "Романтичен Париж" });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "Title" },
                values: new object[] { "Кратък 3-дневен уикенд в Париж — идеален за тези с малко отпуска. Самолет, хотел и основните забележителности включени.", "Париж Уикенд" });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "Title" },
                values: new object[] { "Открийте магията на Барселона с посещения на Саграда Фамилия, парк Гуел, Каса Батло и оживения пазар Бокерия.", "Барселона и Гауди" });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Description", "Title" },
                values: new object[] { "6 дни в Дубровник и по хърватското крайбрежие. Разходка по крепостните стени, екскурзия до остров Локрум и плаж на Будва.", "Адриатическа перла" });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Description", "Title" },
                values: new object[] { "Потопете се в историята на Древна Гърция — Акропол, Националният музей, квартал Монастираки и вкусна гръцка вечеря с музика.", "Атинско приключение" });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Description", "Title" },
                values: new object[] { "4 дни в приказната Прага — Карловият мост, Пражкият замък, Старото градско ядро и легендарните чешки бири в исторически пивници.", "Магията на Прага" });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Description", "Title" },
                values: new object[] { "Комбиниран тур до две от най-красивите имперски столици — 4 дни в Прага и 4 дни във Виена с автобусен преход и хотели 3*.", "Прага и Виена — 8 дни" });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Description", "Title" },
                values: new object[] { "5 незабравими дни в Истанбул — Света София, Синята джамия, Гранд базарът, круиз по Босфора и автентична турска вечеря с танцьори.", "Истанбул — Мостът на световете" });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Description", "Title" },
                values: new object[] { "4-дневен тур с круиз по каналите, посещение на музея на Ван Гог и Рийксмузеум, разходка с велосипеди и пазар за цветя.", "Амстердам — Градът на каналите" });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Description", "Title" },
                values: new object[] { "5 дни в столицата на Хабсбургите — Дворецът Шьонбрун, Операта, Белведере, виенски кафенета и концерт с класическа музика.", "Имперска Виена" });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Description", "Title" },
                values: new object[] { "7 дни на вулканичния остров Санторини — залезите от Ия, вулканичните плажове, дегустация на местни вина и круиз около острова.", "Санторини — Остров на мечтите" });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Description", "Title" },
                values: new object[] { "3-дневен уикенд полет до Санторини — нощувки в традиционна кикладска вила, гледка към калдерата и частна яхтена обиколка.", "Санторини Уикенд" });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "Description", "Title" },
                values: new object[] { "Епичен 10-дневен тур из Балканите — Атина, Солун, Охрид и Будва. Смесица от история, природа и автентична балканска кухня.", "Балкански Експрес — 10 дни" });
        }
    }
}
