using Microsoft.EntityFrameworkCore;
using Tourism.Data.Models.Entities;
namespace Tourism.Data.Seeding
{
    public static class DataSeeder
    {
        public static void Seed(ModelBuilder builder)
        {
            builder.Entity<Destination>().HasData(
                new Destination { Id = 1, Name = "Рим", Country = "Италия", Description = "Вечният град с хиляди години история, включително Колизеума, Ватикана и Трефонтана.", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c0/Rome_Skyline.jpg/1200px-Rome_Skyline.jpg" },
                new Destination { Id = 2, Name = "Париж", Country = "Франция", Description = "Градът на любовта с Айфеловата кула, Лувъра и романтичната атмосфера.", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/4b/La_Tour_Eiffel_vue_de_la_Tour_Saint-Jacques%2C_Paris_August_2014_%282%29.jpg/800px-La_Tour_Eiffel_vue_de_la_Tour_Saint-Jacques%2C_Paris_August_2014_%282%29.jpg" },
                new Destination { Id = 3, Name = "Барселона", Country = "Испания", Description = "Слънчев крайбрежен град с архитектурата на Гауди и оживения Лас Рамблас.", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1f/Sagrada_Familia_01.jpg/800px-Sagrada_Familia_01.jpg" },
                new Destination { Id = 4, Name = "Дубровник", Country = "Хърватия", Description = "Перлата на Адриатика с невероятни стари стени и кристално море.", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/6/6e/Dubrovnik_-_Old_City_Walls.jpg/1200px-Dubrovnik_-_Old_City_Walls.jpg" },
                new Destination { Id = 5, Name = "Атина", Country = "Гърция", Description = "Люлката на цивилизацията с Акропола, Партенона и вкусната средиземноморска кухня.", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/da/The_Parthenon_in_Athens.jpg/1200px-The_Parthenon_in_Athens.jpg" }
            );

            builder.Entity<TourOperator>().HasData(
                new TourOperator { Id = 1, Name = "Балкан Травел", Description = "Водеща туристическа агенция с над 20 години опит.", Email = "info@balkantravel.bg", PhoneNumber = "+359 2 123 4567", LogoUrl = "", CreatedAt = new DateTime(2020, 1, 1) },
                new TourOperator { Id = 2, Name = "Сън Туризъм", Description = "Специалисти в средиземноморския туризъм.", Email = "contact@suntourism.bg", PhoneNumber = "+359 2 987 6543", LogoUrl = "", CreatedAt = new DateTime(2018, 6, 1) },
                new TourOperator { Id = 3, Name = "Европа Експлорър", Description = "Групови и индивидуални пътувания до Европа.", Email = "hello@europaexplorer.bg", PhoneNumber = "+359 2 555 1234", LogoUrl = "", CreatedAt = new DateTime(2015, 3, 15) }
            );

            builder.Entity<Tour>().HasData(
                new Tour { Id = 1, Title = "Класически Рим", Description = "Разгледайте Вечния град с нашия 5-дневен тур.", PricePerPerson = 899, DurationDays = 5, MaxParticipants = 20, ImageUrl = "", StartDate = new DateTime(2026, 4, 10), EndDate = new DateTime(2026, 4, 15), CreatedAt = new DateTime(2026, 1, 1), DestinationId = 1, TourOperatorId = 1 },
                new Tour { Id = 2, Title = "Романтичен Париж", Description = "7 дни в Града на светлините.", PricePerPerson = 1199, DurationDays = 7, MaxParticipants = 15, ImageUrl = "", StartDate = new DateTime(2026, 5, 1), EndDate = new DateTime(2026, 5, 8), CreatedAt = new DateTime(2026, 1, 5), DestinationId = 2, TourOperatorId = 2 },
                new Tour { Id = 3, Title = "Барселона и Гауди", Description = "Открийте магията на Барселона.", PricePerPerson = 799, DurationDays = 4, MaxParticipants = 25, ImageUrl = "", StartDate = new DateTime(2026, 6, 15), EndDate = new DateTime(2026, 6, 19), CreatedAt = new DateTime(2026, 1, 10), DestinationId = 3, TourOperatorId = 3 },
                new Tour { Id = 4, Title = "Адриатическа перла", Description = "Дубровник и хърватското крайбрежие.", PricePerPerson = 699, DurationDays = 6, MaxParticipants = 18, ImageUrl = "", StartDate = new DateTime(2026, 7, 1), EndDate = new DateTime(2026, 7, 7), CreatedAt = new DateTime(2026, 1, 15), DestinationId = 4, TourOperatorId = 1 },
                new Tour { Id = 5, Title = "Атинско приключение", Description = "Потопете се в историята на Древна Гърция.", PricePerPerson = 649, DurationDays = 5, MaxParticipants = 22, ImageUrl = "", StartDate = new DateTime(2026, 8, 10), EndDate = new DateTime(2026, 8, 15), CreatedAt = new DateTime(2026, 1, 20), DestinationId = 5, TourOperatorId = 2 }
            );
        }
    }
}
