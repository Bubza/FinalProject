using Microsoft.EntityFrameworkCore;
using Tourism.Data.Models.Entities;

namespace Tourism.Data.Seeding
{
    public static class DataSeeder
    {
        public static void Seed(ModelBuilder builder)
        {
            //  DESTINATIONS  (10)
            builder.Entity<Destination>().HasData(
                new Destination
                {
                    Id = 1,
                    Name = "Rome",
                    Country = "Italy",
                    Description = "The Eternal City with thousands of years of history — the Colosseum, the Vatican, and world-renowned Italian cuisine.",
                    ImageUrl = "https://images.unsplash.com/photo-1552832230-c0197dd311b5?w=800&q=80"
                },
                new Destination
                {
                    Id = 2,
                    Name = "Paris",
                    Country = "France",
                    Description = "The City of Love — the Eiffel Tower, the Louvre, and an unforgettable romantic atmosphere.",
                    ImageUrl = "https://images.unsplash.com/photo-1502602898657-3e91760cbb34?w=800&q=80"
                },
                new Destination
                {
                    Id = 3,
                    Name = "Barcelona",
                    Country = "Spain",
                    Description = "A sun-drenched coastal city with Gaudí's iconic architecture, vibrant Las Ramblas, and beautiful beaches.",
                    ImageUrl = "https://images.unsplash.com/photo-1539037116277-4db20889f2d4?w=800&q=80"
                },
                new Destination
                {
                    Id = 4,
                    Name = "Dubrovnik",
                    Country = "Croatia",
                    Description = "The Pearl of the Adriatic — medieval walls, crystal-clear sea, and picturesque cobblestone streets.",
                    ImageUrl = "https://images.unsplash.com/photo-1555990793-da11153b2473?w=800&q=80"
                },
                new Destination
                {
                    Id = 5,
                    Name = "Athens",
                    Country = "Greece",
                    Description = "The cradle of civilization — the Acropolis, the Parthenon, and rich Mediterranean cuisine.",
                    ImageUrl = "https://images.unsplash.com/photo-1555993539-1732b0258235?w=800&q=80"
                },
                new Destination
                {
                    Id = 6,
                    Name = "Prague",
                    Country = "Czech Republic",
                    Description = "The City of a Hundred Spires — fairy-tale medieval architecture, Charles Bridge, and a bohemian atmosphere.",
                    ImageUrl = "https://images.unsplash.com/photo-1519677100203-a0e668c92439?w=800&q=80"
                },
                new Destination
                {
                    Id = 7,
                    Name = "Istanbul",
                    Country = "Turkey",
                    Description = "The bridge between Europe and Asia — Hagia Sophia, the Grand Bazaar, and the flavors of the Orient.",
                    ImageUrl = "https://images.unsplash.com/photo-1527838832700-5059252407fa?w=800&q=80"
                },
                new Destination
                {
                    Id = 8,
                    Name = "Amsterdam",
                    Country = "Netherlands",
                    Description = "The City of Canals — bicycles, tulips, world-class museums, and unique Dutch architecture.",
                    ImageUrl = "https://images.unsplash.com/photo-1534351590666-13e3e96b5017?w=800&q=80"
                },
                new Destination
                {
                    Id = 9,
                    Name = "Vienna",
                    Country = "Austria",
                    Description = "The Imperial City — grand palaces, opera houses, famous Viennese cafés, and classical music.",
                    ImageUrl = "https://images.unsplash.com/photo-1516550893923-42d28e5677af?w=800&q=80"
                },
                new Destination
                {
                    Id = 10,
                    Name = "Santorini",
                    Country = "Greece",
                    Description = "A volcanic island with whitewashed buildings, blue domes, and some of the most beautiful sunsets in the world.",
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
                    Name = "Balkan Travel",
                    Description = "A leading travel agency with over 20 years of experience organizing group and individual tours.",
                    Email = "info@balkantravel.bg",
                    PhoneNumber = "+359 2 123 4567",
                    LogoUrl = "",
                    CreatedAt = new DateTime(2020, 1, 1)
                },
                new TourOperator
                {
                    Id = 2,
                    Name = "Sun Tourism",
                    Description = "Specialists in Mediterranean and island tourism — sea, sun, and unforgettable experiences.",
                    Email = "contact@suntourism.bg",
                    PhoneNumber = "+359 2 987 6543",
                    LogoUrl = "",
                    CreatedAt = new DateTime(2018, 6, 1)
                },
                new TourOperator
                {
                    Id = 3,
                    Name = "Europa Explorer",
                    Description = "Group and individual tours to all European destinations with local expert guides.",
                    Email = "hello@europaexplorer.bg",
                    PhoneNumber = "+359 2 555 1234",
                    LogoUrl = "",
                    CreatedAt = new DateTime(2015, 3, 15)
                },
                new TourOperator
                {
                    Id = 4,
                    Name = "Orient Tours",
                    Description = "Exotic destinations, crossroads routes, and unforgettable adventures across Asia and the Middle East.",
                    Email = "orient@orienttours.bg",
                    PhoneNumber = "+359 2 444 5678",
                    LogoUrl = "",
                    CreatedAt = new DateTime(2017, 9, 10)
                },
                new TourOperator
                {
                    Id = 5,
                    Name = "Alpine Paths",
                    Description = "Specialists in mountain and adventure tourism — trekking, skiing, and eco-travel.",
                    Email = "info@alpinepaths.bg",
                    PhoneNumber = "+359 2 777 9900",
                    LogoUrl = "",
                    CreatedAt = new DateTime(2019, 4, 20)
                }
            );

            // TOURS (15)
            builder.Entity<Tour>().HasData(

                // Rome 
                new Tour
                {
                    Id = 1,
                    Title = "Classic Rome",
                    DestinationId = 1,
                    TourOperatorId = 1,
                    Description = "A 5-day journey to the Eternal City. Visit the Colosseum, the Roman Forum, the Vatican, and toss a coin in the Trevi Fountain. Includes expert guide, 4* hotel, and breakfast.",
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
                    Title = "Rome & Vatican — VIP Tour",
                    DestinationId = 1,
                    TourOperatorId = 3,
                    Description = "An exclusive 3-day weekend tour with skip-the-line entry to the Vatican Museums, a private dinner in Trastevere, and nights in a boutique hotel in the heart of Rome.",
                    PricePerPerson = 1250,
                    DurationDays = 3,
                    MaxParticipants = 10,
                    ImageUrl = "https://images.unsplash.com/photo-1531572753322-ad063cecc140?w=800&q=80",
                    StartDate = new DateTime(2026, 5, 22),
                    EndDate = new DateTime(2026, 5, 25),
                    CreatedAt = new DateTime(2026, 1, 3)
                },

                // Paris 
                new Tour
                {
                    Id = 3,
                    Title = "Romantic Paris",
                    DestinationId = 2,
                    TourOperatorId = 2,
                    Description = "7 days in the City of Light — the Eiffel Tower, the Louvre, Montmartre, and a cruise along the Seine. Perfect for couples and art lovers.",
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
                    Title = "Paris Weekend",
                    DestinationId = 2,
                    TourOperatorId = 1,
                    Description = "A short 3-day Paris weekend — ideal for those with limited time off. Flights, hotel, and all major highlights included.",
                    PricePerPerson = 649,
                    DurationDays = 3,
                    MaxParticipants = 20,
                    ImageUrl = "https://images.unsplash.com/photo-1499856871958-5b9627545d1a?w=800&q=80",
                    StartDate = new DateTime(2026, 6, 5),
                    EndDate = new DateTime(2026, 6, 8),
                    CreatedAt = new DateTime(2026, 1, 6)
                },

                // Barcelona
                new Tour
                {
                    Id = 5,
                    Title = "Barcelona & Gaudí",
                    DestinationId = 3,
                    TourOperatorId = 3,
                    Description = "Discover the magic of Barcelona with visits to the Sagrada Família, Park Güell, Casa Batlló, and the vibrant Boqueria market.",
                    PricePerPerson = 799,
                    DurationDays = 4,
                    MaxParticipants = 25,
                    ImageUrl = "https://images.unsplash.com/photo-1539037116277-4db20889f2d4?w=800&q=80",
                    StartDate = new DateTime(2026, 6, 15),
                    EndDate = new DateTime(2026, 6, 19),
                    CreatedAt = new DateTime(2026, 1, 10)
                },

                // Dubrovnik
                new Tour
                {
                    Id = 6,
                    Title = "Adriatic Pearl",
                    DestinationId = 4,
                    TourOperatorId = 1,
                    Description = "6 days in Dubrovnik and along the Croatian coast. Walk the ancient city walls, take a day trip to Lokrum Island, and relax on the beaches of Budva.",
                    PricePerPerson = 699,
                    DurationDays = 6,
                    MaxParticipants = 18,
                    ImageUrl = "https://images.unsplash.com/photo-1555990793-da11153b2473?w=800&q=80",
                    StartDate = new DateTime(2026, 7, 1),
                    EndDate = new DateTime(2026, 7, 7),
                    CreatedAt = new DateTime(2026, 1, 15)
                },

                // Athens
                new Tour
                {
                    Id = 7,
                    Title = "Athens Adventure",
                    DestinationId = 5,
                    TourOperatorId = 2,
                    Description = "Immerse yourself in the history of Ancient Greece — the Acropolis, the National Museum, the Monastiraki district, and a traditional Greek dinner with live music.",
                    PricePerPerson = 649,
                    DurationDays = 5,
                    MaxParticipants = 22,
                    ImageUrl = "https://images.unsplash.com/photo-1555993539-1732b0258235?w=800&q=80",
                    StartDate = new DateTime(2026, 8, 10),
                    EndDate = new DateTime(2026, 8, 15),
                    CreatedAt = new DateTime(2026, 1, 20)
                },

                // Prague
                new Tour
                {
                    Id = 8,
                    Title = "Magic of Prague",
                    DestinationId = 6,
                    TourOperatorId = 3,
                    Description = "4 days in fairy-tale Prague — Charles Bridge, Prague Castle, the Old Town Square, and legendary Czech beers in historic pubs.",
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
                    Title = "Prague & Vienna — 8 Days",
                    DestinationId = 6,
                    TourOperatorId = 1,
                    Description = "A combined tour to two of Europe's most beautiful imperial capitals — 4 days in Prague and 4 days in Vienna, with coach transfer and 3* hotels.",
                    PricePerPerson = 980,
                    DurationDays = 8,
                    MaxParticipants = 25,
                    ImageUrl = "https://images.unsplash.com/photo-1467269204594-9661b134dd2b?w=800&q=80",
                    StartDate = new DateTime(2026, 9, 5),
                    EndDate = new DateTime(2026, 9, 13),
                    CreatedAt = new DateTime(2026, 1, 25)
                },

                // Istanbul
                new Tour
                {
                    Id = 10,
                    Title = "Istanbul — Bridge of Worlds",
                    DestinationId = 7,
                    TourOperatorId = 4,
                    Description = "5 unforgettable days in Istanbul — Hagia Sophia, the Blue Mosque, the Grand Bazaar, a Bosphorus cruise, and an authentic Turkish dinner with folk dancers.",
                    PricePerPerson = 729,
                    DurationDays = 5,
                    MaxParticipants = 20,
                    ImageUrl = "https://images.unsplash.com/photo-1527838832700-5059252407fa?w=800&q=80",
                    StartDate = new DateTime(2026, 5, 12),
                    EndDate = new DateTime(2026, 5, 17),
                    CreatedAt = new DateTime(2026, 2, 1)
                },

                // Amsterdam
                new Tour
                {
                    Id = 11,
                    Title = "Amsterdam — City of Canals",
                    DestinationId = 8,
                    TourOperatorId = 3,
                    Description = "A 4-day tour with a canal cruise, visits to the Van Gogh Museum and Rijksmuseum, a bicycle ride through the city, and a flower market.",
                    PricePerPerson = 819,
                    DurationDays = 4,
                    MaxParticipants = 18,
                    ImageUrl = "https://images.unsplash.com/photo-1534351590666-13e3e96b5017?w=800&q=80",
                    StartDate = new DateTime(2026, 4, 24),
                    EndDate = new DateTime(2026, 4, 28),
                    CreatedAt = new DateTime(2026, 2, 3)
                },

                // Vienna
                new Tour
                {
                    Id = 12,
                    Title = "Imperial Vienna",
                    DestinationId = 9,
                    TourOperatorId = 1,
                    Description = "5 days in the Habsburg capital — Schönbrunn Palace, the State Opera, Belvedere, Viennese cafés, and a classical music concert.",
                    PricePerPerson = 899,
                    DurationDays = 5,
                    MaxParticipants = 20,
                    ImageUrl = "https://images.unsplash.com/photo-1516550893923-42d28e5677af?w=800&q=80",
                    StartDate = new DateTime(2026, 10, 3),
                    EndDate = new DateTime(2026, 10, 8),
                    CreatedAt = new DateTime(2026, 2, 5)
                },

                // Santorini
                new Tour
                {
                    Id = 13,
                    Title = "Santorini — Island of Dreams",
                    DestinationId = 10,
                    TourOperatorId = 2,
                    Description = "7 days on the volcanic island of Santorini — sunsets from Oia, volcanic beaches, local wine tasting, and a cruise around the island.",
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
                    Title = "Santorini Weekend",
                    DestinationId = 10,
                    TourOperatorId = 4,
                    Description = "A 3-day weekend flight to Santorini — nights in a traditional Cycladic villa overlooking the caldera, and a private yacht tour.",
                    PricePerPerson = 850,
                    DurationDays = 3,
                    MaxParticipants = 8,
                    ImageUrl = "https://images.unsplash.com/photo-1570077188670-e3a8d69ac5ff?w=800&q=80",
                    StartDate = new DateTime(2026, 8, 28),
                    EndDate = new DateTime(2026, 8, 31),
                    CreatedAt = new DateTime(2026, 2, 10)
                },

                // Balkan tour
                new Tour
                {
                    Id = 15,
                    Title = "Balkan Express — 10 Days",
                    DestinationId = 5,
                    TourOperatorId = 5,
                    Description = "An epic 10-day tour across the Balkans — Athens, Thessaloniki, Ohrid, and Budva. A blend of history, nature, and authentic Balkan cuisine.",
                    PricePerPerson = 1100,
                    DurationDays = 10,
                    MaxParticipants = 28,
                    ImageUrl = "https://images.unsplash.com/photo-1590147534648-1ac5cca59621?q=80&w=1374&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                    StartDate = new DateTime(2026, 9, 20),
                    EndDate = new DateTime(2026, 9, 30),
                    CreatedAt = new DateTime(2026, 2, 15)
                }
            );
        }
    }
}