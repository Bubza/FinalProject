using Microsoft.EntityFrameworkCore;
using Tourism.Data.Models.Entities;

namespace Tourism.Data.Seeding
{
    public static class DataSeeder
    {
        public static void Seed(ModelBuilder builder)
        {
            // ══════════════════════════════
            //  DESTINATIONS  (10)
            // ══════════════════════════════
            builder.Entity<Destination>().HasData(
                new Destination
                {
                    Id = 1,
                    Name = "Рим",
                    Country = "Италия",
                    Description = "Вечният град с хиляди години история — Колизеума, Ватикана и прочутата италианска кухня.",
                    ImageUrl = "https://images.unsplash.com/photo-1552832230-c0197dd311b5?w=800&q=80"
                },
                new Destination
                {
                    Id = 2,
                    Name = "Париж",
                    Country = "Франция",
                    Description = "Градът на любовта с Айфеловата кула, Лувъра и незабравимата романтична атмосфера.",
                    ImageUrl = "https://images.unsplash.com/photo-1502602898657-3e91760cbb34?w=800&q=80"
                },
                new Destination
                {
                    Id = 3,
                    Name = "Барселона",
                    Country = "Испания",
                    Description = "Слънчев крайбрежен град с архитектурата на Гауди, оживения Лас Рамблас и прекрасни плажове.",
                    ImageUrl = "https://images.unsplash.com/photo-1539037116277-4db20889f2d4?w=800&q=80"
                },
                new Destination
                {
                    Id = 4,
                    Name = "Дубровник",
                    Country = "Хърватия",
                    Description = "Перлата на Адриатика — средновековни стени, кристално синьо море и живописни улички.",
                    ImageUrl = "https://images.unsplash.com/photo-1555990793-da11153b2473?w=800&q=80"
                },
                new Destination
                {
                    Id = 5,
                    Name = "Атина",
                    Country = "Гърция",
                    Description = "Люлката на цивилизацията — Акрополът, Партенонът и богатата средиземноморска кухня.",
                    ImageUrl = "https://images.unsplash.com/photo-1555993539-1732b0258235?w=800&q=80"
                },
                new Destination
                {
                    Id = 6,
                    Name = "Прага",
                    Country = "Чехия",
                    Description = "Градът на сто кули — приказна средновековна архитектура, Карловият мост и бохемска атмосфера.",
                    ImageUrl = "https://images.unsplash.com/photo-1519677100203-a0e668c92439?w=800&q=80"
                },
                new Destination
                {
                    Id = 7,
                    Name = "Истанбул",
                    Country = "Турция",
                    Description = "Мостът между Европа и Азия — Света София, Гранд базарът и вкусовете на Ориента.",
                    ImageUrl = "https://images.unsplash.com/photo-1527838832700-5059252407fa?w=800&q=80"
                },
                new Destination
                {
                    Id = 8,
                    Name = "Амстердам",
                    Country = "Нидерландия",
                    Description = "Градът на каналите — велосипеди, лалета, музеи и неповторима холандска архитектура.",
                    ImageUrl = "https://images.unsplash.com/photo-1534351590666-13e3e96b5017?w=800&q=80"
                },
                new Destination
                {
                    Id = 9,
                    Name = "Виена",
                    Country = "Австрия",
                    Description = "Имперският град — дворци, опера, прочутите виенски кафенета и класическа музика.",
                    ImageUrl = "https://images.unsplash.com/photo-1516550893923-42d28e5677af?w=800&q=80"
                },
                new Destination
                {
                    Id = 10,
                    Name = "Санторини",
                    Country = "Гърция",
                    Description = "Вулканичен остров с бели сгради, сини куполи и едни от най-красивите залези в света.",
                    ImageUrl = "https://images.unsplash.com/photo-1613395877344-13d4a8e0d49e?w=800&q=80"
                }
            );

            // ══════════════════════════════
            //  TOUR OPERATORS  (5)
            // ══════════════════════════════
            builder.Entity<TourOperator>().HasData(
                new TourOperator
                {
                    Id = 1,
                    Name = "Балкан Травел",
                    Description = "Водеща туристическа агенция с над 20 години опит в организирането на групови и индивидуални пътувания.",
                    Email = "info@balkantravel.bg",
                    PhoneNumber = "+359 2 123 4567",
                    LogoUrl = "",
                    CreatedAt = new DateTime(2020, 1, 1)
                },
                new TourOperator
                {
                    Id = 2,
                    Name = "Сън Туризъм",
                    Description = "Специалисти в средиземноморския и островен туризъм — море, слънце и незабравими преживявания.",
                    Email = "contact@suntourism.bg",
                    PhoneNumber = "+359 2 987 6543",
                    LogoUrl = "",
                    CreatedAt = new DateTime(2018, 6, 1)
                },
                new TourOperator
                {
                    Id = 3,
                    Name = "Европа Експлорър",
                    Description = "Групови и индивидуални пътувания до всички европейски дестинации с местни водачи.",
                    Email = "hello@europaexplorer.bg",
                    PhoneNumber = "+359 2 555 1234",
                    LogoUrl = "",
                    CreatedAt = new DateTime(2015, 3, 15)
                },
                new TourOperator
                {
                    Id = 4,
                    Name = "Ориент Турс",
                    Description = "Екзотични дестинации, кръстопътни маршрути и незабравими приключения в Азия и Близкия изток.",
                    Email = "orient@orienttours.bg",
                    PhoneNumber = "+359 2 444 5678",
                    LogoUrl = "",
                    CreatedAt = new DateTime(2017, 9, 10)
                },
                new TourOperator
                {
                    Id = 5,
                    Name = "Алпийски Преходи",
                    Description = "Специалисти в планински и приключенски туризъм — трекинг, ски и еко пътувания.",
                    Email = "info@alpinepaths.bg",
                    PhoneNumber = "+359 2 777 9900",
                    LogoUrl = "",
                    CreatedAt = new DateTime(2019, 4, 20)
                }
            );

            // ══════════════════════════════
            //  TOURS  (15)
            // ══════════════════════════════
            builder.Entity<Tour>().HasData(

                // ── Рим ──
                new Tour
                {
                    Id = 1,
                    Title = "Класически Рим",
                    DestinationId = 1,
                    TourOperatorId = 1,
                    Description = "5-дневно пътуване до Вечния град. Посетете Колизеума, Форума, Ватикана и хвърлете монета в Фонтана ди Треви. Включени са квалифициран водач, хотел 4* и закуски.",
                    PricePerPerson = 899,
                    DurationDays = 5,
                    MaxParticipants = 20,
                    ImageUrl = "https://images.unsplash.com/photo-1552832230-c0197dd311b5?w=800&q=80",
                    StartDate = new DateTime(2026, 4, 10),
                    EndDate = new DateTime(2026, 4, 15),
                    CreatedAt = new DateTime(2026, 1, 1)
                },
                new Tour
                {
                    Id = 2,
                    Title = "Рим и Ватикана — VIP тур",
                    DestinationId = 1,
                    TourOperatorId = 3,
                    Description = "Ексклузивен 3-дневен уикенд тур с влизане без опашка в Ватиканските музеи, частна вечеря в Трастевере и нощувки в бутиков хотел в центъра.",
                    PricePerPerson = 1250,
                    DurationDays = 3,
                    MaxParticipants = 10,
                    ImageUrl = "https://images.unsplash.com/photo-1568797629192-789acf8e4df3?w=800&q=80",
                    StartDate = new DateTime(2026, 5, 22),
                    EndDate = new DateTime(2026, 5, 25),
                    CreatedAt = new DateTime(2026, 1, 3)
                },

                // ── Париж ──
                new Tour
                {
                    Id = 3,
                    Title = "Романтичен Париж",
                    DestinationId = 2,
                    TourOperatorId = 2,
                    Description = "7 дни в Града на светлините — Айфеловата кула, Лувъра, Монмартър и круиз по Сена. Перфектен за двойки и любители на изкуството.",
                    PricePerPerson = 1199,
                    DurationDays = 7,
                    MaxParticipants = 15,
                    ImageUrl = "https://images.unsplash.com/photo-1502602898657-3e91760cbb34?w=800&q=80",
                    StartDate = new DateTime(2026, 5, 1),
                    EndDate = new DateTime(2026, 5, 8),
                    CreatedAt = new DateTime(2026, 1, 5)
                },
                new Tour
                {
                    Id = 4,
                    Title = "Париж Уикенд",
                    DestinationId = 2,
                    TourOperatorId = 1,
                    Description = "Кратък 3-дневен уикенд в Париж — идеален за тези с малко отпуска. Самолет, хотел и основните забележителности включени.",
                    PricePerPerson = 649,
                    DurationDays = 3,
                    MaxParticipants = 20,
                    ImageUrl = "https://images.unsplash.com/photo-1499856871958-5b9627545d1a?w=800&q=80",
                    StartDate = new DateTime(2026, 6, 5),
                    EndDate = new DateTime(2026, 6, 8),
                    CreatedAt = new DateTime(2026, 1, 6)
                },

                // ── Барселона ──
                new Tour
                {
                    Id = 5,
                    Title = "Барселона и Гауди",
                    DestinationId = 3,
                    TourOperatorId = 3,
                    Description = "Открийте магията на Барселона с посещения на Саграда Фамилия, парк Гуел, Каса Батло и оживения пазар Бокерия.",
                    PricePerPerson = 799,
                    DurationDays = 4,
                    MaxParticipants = 25,
                    ImageUrl = "https://images.unsplash.com/photo-1539037116277-4db20889f2d4?w=800&q=80",
                    StartDate = new DateTime(2026, 6, 15),
                    EndDate = new DateTime(2026, 6, 19),
                    CreatedAt = new DateTime(2026, 1, 10)
                },

                // ── Дубровник ──
                new Tour
                {
                    Id = 6,
                    Title = "Адриатическа перла",
                    DestinationId = 4,
                    TourOperatorId = 1,
                    Description = "6 дни в Дубровник и по хърватското крайбрежие. Разходка по крепостните стени, екскурзия до остров Локрум и плаж на Будва.",
                    PricePerPerson = 699,
                    DurationDays = 6,
                    MaxParticipants = 18,
                    ImageUrl = "https://images.unsplash.com/photo-1555990793-da11153b2473?w=800&q=80",
                    StartDate = new DateTime(2026, 7, 1),
                    EndDate = new DateTime(2026, 7, 7),
                    CreatedAt = new DateTime(2026, 1, 15)
                },

                // ── Атина ──
                new Tour
                {
                    Id = 7,
                    Title = "Атинско приключение",
                    DestinationId = 5,
                    TourOperatorId = 2,
                    Description = "Потопете се в историята на Древна Гърция — Акропол, Националният музей, квартал Монастираки и вкусна гръцка вечеря с музика.",
                    PricePerPerson = 649,
                    DurationDays = 5,
                    MaxParticipants = 22,
                    ImageUrl = "https://images.unsplash.com/photo-1555993539-1732b0258235?w=800&q=80",
                    StartDate = new DateTime(2026, 8, 10),
                    EndDate = new DateTime(2026, 8, 15),
                    CreatedAt = new DateTime(2026, 1, 20)
                },

                // ── Прага ──
                new Tour
                {
                    Id = 8,
                    Title = "Магията на Прага",
                    DestinationId = 6,
                    TourOperatorId = 3,
                    Description = "4 дни в приказната Прага — Карловият мост, Пражкият замък, Старото градско ядро и легендарните чешки бири в исторически пивници.",
                    PricePerPerson = 549,
                    DurationDays = 4,
                    MaxParticipants = 30,
                    ImageUrl = "https://images.unsplash.com/photo-1519677100203-a0e668c92439?w=800&q=80",
                    StartDate = new DateTime(2026, 4, 18),
                    EndDate = new DateTime(2026, 4, 22),
                    CreatedAt = new DateTime(2026, 1, 22)
                },
                new Tour
                {
                    Id = 9,
                    Title = "Прага и Виена — 8 дни",
                    DestinationId = 6,
                    TourOperatorId = 1,
                    Description = "Комбиниран тур до две от най-красивите имперски столици — 4 дни в Прага и 4 дни във Виена с автобусен преход и хотели 3*.",
                    PricePerPerson = 980,
                    DurationDays = 8,
                    MaxParticipants = 25,
                    ImageUrl = "https://images.unsplash.com/photo-1555576208-f2598fdc0639?w=800&q=80",
                    StartDate = new DateTime(2026, 9, 5),
                    EndDate = new DateTime(2026, 9, 13),
                    CreatedAt = new DateTime(2026, 1, 25)
                },

                // ── Истанбул ──
                new Tour
                {
                    Id = 10,
                    Title = "Истанбул — Мостът на световете",
                    DestinationId = 7,
                    TourOperatorId = 4,
                    Description = "5 незабравими дни в Истанбул — Света София, Синята джамия, Гранд базарът, круиз по Босфора и автентична турска вечеря с танцьори.",
                    PricePerPerson = 729,
                    DurationDays = 5,
                    MaxParticipants = 20,
                    ImageUrl = "https://images.unsplash.com/photo-1527838832700-5059252407fa?w=800&q=80",
                    StartDate = new DateTime(2026, 5, 12),
                    EndDate = new DateTime(2026, 5, 17),
                    CreatedAt = new DateTime(2026, 2, 1)
                },

                // ── Амстердам ──
                new Tour
                {
                    Id = 11,
                    Title = "Амстердам — Градът на каналите",
                    DestinationId = 8,
                    TourOperatorId = 3,
                    Description = "4-дневен тур с круиз по каналите, посещение на музея на Ван Гог и Рийксмузеум, разходка с велосипеди и пазар за цветя.",
                    PricePerPerson = 819,
                    DurationDays = 4,
                    MaxParticipants = 18,
                    ImageUrl = "https://images.unsplash.com/photo-1534351590666-13e3e96b5017?w=800&q=80",
                    StartDate = new DateTime(2026, 4, 24),
                    EndDate = new DateTime(2026, 4, 28),
                    CreatedAt = new DateTime(2026, 2, 3)
                },

                // ── Виена ──
                new Tour
                {
                    Id = 12,
                    Title = "Имперска Виена",
                    DestinationId = 9,
                    TourOperatorId = 1,
                    Description = "5 дни в столицата на Хабсбургите — Дворецът Шьонбрун, Операта, Белведере, виенски кафенета и концерт с класическа музика.",
                    PricePerPerson = 899,
                    DurationDays = 5,
                    MaxParticipants = 20,
                    ImageUrl = "https://images.unsplash.com/photo-1516550893923-42d28e5677af?w=800&q=80",
                    StartDate = new DateTime(2026, 10, 3),
                    EndDate = new DateTime(2026, 10, 8),
                    CreatedAt = new DateTime(2026, 2, 5)
                },

                // ── Санторини ──
                new Tour
                {
                    Id = 13,
                    Title = "Санторини — Остров на мечтите",
                    DestinationId = 10,
                    TourOperatorId = 2,
                    Description = "7 дни на вулканичния остров Санторини — залезите от Ия, вулканичните плажове, дегустация на местни вина и круиз около острова.",
                    PricePerPerson = 1399,
                    DurationDays = 7,
                    MaxParticipants = 14,
                    ImageUrl = "https://images.unsplash.com/photo-1613395877344-13d4a8e0d49e?w=800&q=80",
                    StartDate = new DateTime(2026, 7, 15),
                    EndDate = new DateTime(2026, 7, 22),
                    CreatedAt = new DateTime(2026, 2, 8)
                },
                new Tour
                {
                    Id = 14,
                    Title = "Санторини Уикенд",
                    DestinationId = 10,
                    TourOperatorId = 4,
                    Description = "3-дневен уикенд полет до Санторини — нощувки в традиционна кикладска вила, гледка към калдерата и частна яхтена обиколка.",
                    PricePerPerson = 850,
                    DurationDays = 3,
                    MaxParticipants = 8,
                    ImageUrl = "https://images.unsplash.com/photo-1570077188670-e3a8d69ac5ff?w=800&q=80",
                    StartDate = new DateTime(2026, 8, 28),
                    EndDate = new DateTime(2026, 8, 31),
                    CreatedAt = new DateTime(2026, 2, 10)
                },

                // ── Бонус: Комбиниран балкански тур ──
                new Tour
                {
                    Id = 15,
                    Title = "Балкански Експрес — 10 дни",
                    DestinationId = 5,
                    TourOperatorId = 5,
                    Description = "Епичен 10-дневен тур из Балканите — Атина, Солун, Охрид и Будва. Смесица от история, природа и автентична балканска кухня.",
                    PricePerPerson = 1100,
                    DurationDays = 10,
                    MaxParticipants = 28,
                    ImageUrl = "https://images.unsplash.com/photo-1601599561213-832382fd07ba?w=800&q=80",
                    StartDate = new DateTime(2026, 9, 20),
                    EndDate = new DateTime(2026, 9, 30),
                    CreatedAt = new DateTime(2026, 2, 15)
                }
            );
        }
    }
}
