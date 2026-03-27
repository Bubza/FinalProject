using Microsoft.EntityFrameworkCore;
using Tourism.Data.Models.Entities;

namespace Tourism.Data.Seeding
{
    public static class DataSeeder
    {
        public static void Seed(ModelBuilder builder)
        {
            // ══════════════════════════════
            //  CATEGORIES  (6)
            // ══════════════════════════════
            builder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Cultural & Historical", Description = "Explore ancient ruins, museums, and the stories behind the world's greatest civilisations.", IconClass = "bi-bank" },
                new Category { Id = 2, Name = "Beach & Island", Description = "Sun, sea, and sand — the finest coastal and island escapes in Europe.", IconClass = "bi-water" },
                new Category { Id = 3, Name = "City Break", Description = "Compact, action-packed getaways to Europe's most vibrant and charming cities.", IconClass = "bi-buildings" },
                new Category { Id = 4, Name = "Adventure", Description = "Multi-destination road trips, trekking routes, and experiences off the beaten path.", IconClass = "bi-compass" },
                new Category { Id = 5, Name = "Luxury", Description = "Premium hotels, skip-the-line access, private guides, and unforgettable fine dining.", IconClass = "bi-stars" },
                new Category { Id = 6, Name = "Family", Description = "Kid-friendly itineraries designed to create memories for the whole family.", IconClass = "bi-people" }
            );

            // ══════════════════════════════
            //  DESTINATIONS  (13)
            // ══════════════════════════════
            builder.Entity<Destination>().HasData(
                new Destination { Id = 1, Name = "Rome", Country = "Italy", Description = "The Eternal City with thousands of years of history — the Colosseum, the Vatican, and world-renowned Italian cuisine.", ImageUrl = "https://images.unsplash.com/photo-1552832230-c0197dd311b5?w=800&q=80" },
                new Destination { Id = 2, Name = "Paris", Country = "France", Description = "The City of Love — the Eiffel Tower, the Louvre, and an unforgettable romantic atmosphere.", ImageUrl = "https://images.unsplash.com/photo-1502602898657-3e91760cbb34?w=800&q=80" },
                new Destination { Id = 3, Name = "Barcelona", Country = "Spain", Description = "A sun-drenched coastal city with Gaudí's iconic architecture, vibrant Las Ramblas, and beautiful beaches.", ImageUrl = "https://images.unsplash.com/photo-1539037116277-4db20889f2d4?w=800&q=80" },
                new Destination { Id = 4, Name = "Dubrovnik", Country = "Croatia", Description = "The Pearl of the Adriatic — medieval walls, crystal-clear sea, and picturesque cobblestone streets.", ImageUrl = "https://images.unsplash.com/photo-1555990793-da11153b2473?w=800&q=80" },
                new Destination { Id = 5, Name = "Athens", Country = "Greece", Description = "The cradle of civilization — the Acropolis, the Parthenon, and rich Mediterranean cuisine.", ImageUrl = "https://images.unsplash.com/photo-1555993539-1732b0258235?w=800&q=80" },
                new Destination { Id = 6, Name = "Prague", Country = "Czech Republic", Description = "The City of a Hundred Spires — fairy-tale medieval architecture, Charles Bridge, and a bohemian atmosphere.", ImageUrl = "https://images.unsplash.com/photo-1519677100203-a0e668c92439?w=800&q=80" },
                new Destination { Id = 7, Name = "Istanbul", Country = "Turkey", Description = "The bridge between Europe and Asia — Hagia Sophia, the Grand Bazaar, and the flavors of the Orient.", ImageUrl = "https://images.unsplash.com/photo-1527838832700-5059252407fa?w=800&q=80" },
                new Destination { Id = 8, Name = "Amsterdam", Country = "Netherlands", Description = "The City of Canals — bicycles, tulips, world-class museums, and unique Dutch architecture.", ImageUrl = "https://images.unsplash.com/photo-1534351590666-13e3e96b5017?w=800&q=80" },
                new Destination { Id = 9, Name = "Vienna", Country = "Austria", Description = "The Imperial City — grand palaces, opera houses, famous Viennese cafés, and classical music.", ImageUrl = "https://images.unsplash.com/photo-1516550893923-42d28e5677af?w=800&q=80" },
                new Destination { Id = 10, Name = "Santorini", Country = "Greece", Description = "A volcanic island with whitewashed buildings, blue domes, and some of the most beautiful sunsets in the world.", ImageUrl = "https://images.unsplash.com/photo-1613395877344-13d4a8e0d49e?w=800&q=80" },
                new Destination { Id = 11, Name = "Lisbon", Country = "Portugal", Description = "A city of seven hills — historic trams, Fado music, Moorish castles, and the world's finest custard tarts.", ImageUrl = "https://images.unsplash.com/photo-1558370781-d6196949e317?w=800&q=80" },
                new Destination { Id = 12, Name = "Budapest", Country = "Hungary", Description = "The Pearl of the Danube — grand thermal baths, stunning Parliament, ruin bars, and a vibrant nightlife scene.", ImageUrl = "https://images.unsplash.com/photo-1541343672885-9be56236302a?w=800&q=80" },
                new Destination { Id = 13, Name = "Mykonos", Country = "Greece", Description = "The jewel of the Cyclades — windmills, whitewashed alleys, vibrant beach clubs, and crystal-clear Aegean waters.", ImageUrl = "https://images.unsplash.com/photo-1601581875309-fafbf2d3ed3a?w=800&q=80" }
            );

            // ══════════════════════════════
            //  TOUR OPERATORS  (5)
            // ══════════════════════════════
            builder.Entity<TourOperator>().HasData(
                new TourOperator { Id = 1, Name = "Balkan Travel", Description = "A leading travel agency with over 20 years of experience organizing group and individual tours.", Email = "info@balkantravel.bg", PhoneNumber = "+359 2 123 4567", LogoUrl = "", CreatedAt = new DateTime(2020, 1, 1) },
                new TourOperator { Id = 2, Name = "Sun Tourism", Description = "Specialists in Mediterranean and island tourism — sea, sun, and unforgettable experiences.", Email = "contact@suntourism.bg", PhoneNumber = "+359 2 987 6543", LogoUrl = "", CreatedAt = new DateTime(2018, 6, 1) },
                new TourOperator { Id = 3, Name = "Europa Explorer", Description = "Group and individual tours to all European destinations with local expert guides.", Email = "hello@europaexplorer.bg", PhoneNumber = "+359 2 555 1234", LogoUrl = "", CreatedAt = new DateTime(2015, 3, 15) },
                new TourOperator { Id = 4, Name = "Orient Tours", Description = "Exotic destinations, crossroads routes, and unforgettable adventures across Asia and the Middle East.", Email = "orient@orienttours.bg", PhoneNumber = "+359 2 444 5678", LogoUrl = "", CreatedAt = new DateTime(2017, 9, 10) },
                new TourOperator { Id = 5, Name = "Alpine Paths", Description = "Specialists in mountain and adventure tourism — trekking, skiing, and eco-travel.", Email = "info@alpinepaths.bg", PhoneNumber = "+359 2 777 9900", LogoUrl = "", CreatedAt = new DateTime(2019, 4, 20) }
            );

            // ══════════════════════════════
            //  TOURS  (25)
            // ══════════════════════════════
            builder.Entity<Tour>().HasData(
                // Rome
                new Tour { Id = 1, Title = "Classic Rome", DestinationId = 1, TourOperatorId = 1, CategoryId = 1, Description = "A 5-day journey to the Eternal City. Visit the Colosseum, the Roman Forum, the Vatican, and toss a coin in the Trevi Fountain. Includes expert guide, 4* hotel, and breakfast.", PricePerPerson = 899, DurationDays = 5, MaxParticipants = 20, ImageUrl = "https://images.unsplash.com/photo-1552832230-c0197dd311b5?w=800&q=80", StartDate = new DateTime(2026, 4, 10), EndDate = new DateTime(2026, 4, 15), CreatedAt = new DateTime(2026, 1, 1) },
                new Tour { Id = 2, Title = "Rome & Vatican — VIP Tour", DestinationId = 1, TourOperatorId = 3, CategoryId = 5, Description = "An exclusive 3-day weekend tour with skip-the-line entry to the Vatican Museums, a private dinner in Trastevere, and nights in a boutique hotel in the heart of Rome.", PricePerPerson = 1250, DurationDays = 3, MaxParticipants = 10, ImageUrl = "https://images.unsplash.com/photo-1531572753322-ad063cecc140?w=800&q=80", StartDate = new DateTime(2026, 5, 22), EndDate = new DateTime(2026, 5, 25), CreatedAt = new DateTime(2026, 1, 3) },
                // Paris
                new Tour { Id = 3, Title = "Romantic Paris", DestinationId = 2, TourOperatorId = 2, CategoryId = 5, Description = "7 days in the City of Light — the Eiffel Tower, the Louvre, Montmartre, and a cruise along the Seine. Perfect for couples and art lovers.", PricePerPerson = 1199, DurationDays = 7, MaxParticipants = 15, ImageUrl = "https://images.unsplash.com/photo-1502602898657-3e91760cbb34?w=800&q=80", StartDate = new DateTime(2026, 5, 1), EndDate = new DateTime(2026, 5, 8), CreatedAt = new DateTime(2026, 1, 5) },
                new Tour { Id = 4, Title = "Paris Weekend", DestinationId = 2, TourOperatorId = 1, CategoryId = 3, Description = "A short 3-day Paris weekend — ideal for those with limited time off. Flights, hotel, and all major highlights included.", PricePerPerson = 649, DurationDays = 3, MaxParticipants = 20, ImageUrl = "https://images.unsplash.com/photo-1499856871958-5b9627545d1a?w=800&q=80", StartDate = new DateTime(2026, 6, 5), EndDate = new DateTime(2026, 6, 8), CreatedAt = new DateTime(2026, 1, 6) },
                // Barcelona
                new Tour { Id = 5, Title = "Barcelona & Gaudí", DestinationId = 3, TourOperatorId = 3, CategoryId = 1, Description = "Discover the magic of Barcelona with visits to the Sagrada Família, Park Güell, Casa Batlló, and the vibrant Boqueria market.", PricePerPerson = 799, DurationDays = 4, MaxParticipants = 25, ImageUrl = "https://images.unsplash.com/photo-1539037116277-4db20889f2d4?w=800&q=80", StartDate = new DateTime(2026, 6, 15), EndDate = new DateTime(2026, 6, 19), CreatedAt = new DateTime(2026, 1, 10) },
                // Dubrovnik
                new Tour { Id = 6, Title = "Adriatic Pearl", DestinationId = 4, TourOperatorId = 1, CategoryId = 2, Description = "6 days in Dubrovnik and along the Croatian coast. Walk the ancient city walls, take a day trip to Lokrum Island, and relax on the beaches of Budva.", PricePerPerson = 699, DurationDays = 6, MaxParticipants = 18, ImageUrl = "https://images.unsplash.com/photo-1555990793-da11153b2473?w=800&q=80", StartDate = new DateTime(2026, 7, 1), EndDate = new DateTime(2026, 7, 7), CreatedAt = new DateTime(2026, 1, 15) },
                // Athens
                new Tour { Id = 7, Title = "Athens Adventure", DestinationId = 5, TourOperatorId = 2, CategoryId = 1, Description = "Immerse yourself in the history of Ancient Greece — the Acropolis, the National Museum, the Monastiraki district, and a traditional Greek dinner with live music.", PricePerPerson = 649, DurationDays = 5, MaxParticipants = 22, ImageUrl = "https://images.unsplash.com/photo-1555993539-1732b0258235?w=800&q=80", StartDate = new DateTime(2026, 8, 10), EndDate = new DateTime(2026, 8, 15), CreatedAt = new DateTime(2026, 1, 20) },
                // Prague
                new Tour { Id = 8, Title = "Magic of Prague", DestinationId = 6, TourOperatorId = 3, CategoryId = 3, Description = "4 days in fairy-tale Prague — Charles Bridge, Prague Castle, the Old Town Square, and legendary Czech beers in historic pubs.", PricePerPerson = 549, DurationDays = 4, MaxParticipants = 30, ImageUrl = "https://images.unsplash.com/photo-1519677100203-a0e668c92439?w=800&q=80", StartDate = new DateTime(2026, 4, 18), EndDate = new DateTime(2026, 4, 22), CreatedAt = new DateTime(2026, 1, 22) },
                new Tour { Id = 9, Title = "Prague & Vienna — 8 Days", DestinationId = 6, TourOperatorId = 1, CategoryId = 3, Description = "A combined tour to two of Europe's most beautiful imperial capitals — 4 days in Prague and 4 days in Vienna, with coach transfer and 3* hotels.", PricePerPerson = 980, DurationDays = 8, MaxParticipants = 25, ImageUrl = "https://images.unsplash.com/photo-1467269204594-9661b134dd2b?w=800&q=80", StartDate = new DateTime(2026, 9, 5), EndDate = new DateTime(2026, 9, 13), CreatedAt = new DateTime(2026, 1, 25) },
                // Istanbul
                new Tour { Id = 10, Title = "Istanbul — Bridge of Worlds", DestinationId = 7, TourOperatorId = 4, CategoryId = 1, Description = "5 unforgettable days in Istanbul — Hagia Sophia, the Blue Mosque, the Grand Bazaar, a Bosphorus cruise, and an authentic Turkish dinner with folk dancers.", PricePerPerson = 729, DurationDays = 5, MaxParticipants = 20, ImageUrl = "https://images.unsplash.com/photo-1527838832700-5059252407fa?w=800&q=80", StartDate = new DateTime(2026, 5, 12), EndDate = new DateTime(2026, 5, 17), CreatedAt = new DateTime(2026, 2, 1) },
                // Amsterdam
                new Tour { Id = 11, Title = "Amsterdam — City of Canals", DestinationId = 8, TourOperatorId = 3, CategoryId = 3, Description = "A 4-day tour with a canal cruise, visits to the Van Gogh Museum and Rijksmuseum, a bicycle ride through the city, and a flower market.", PricePerPerson = 819, DurationDays = 4, MaxParticipants = 18, ImageUrl = "https://images.unsplash.com/photo-1534351590666-13e3e96b5017?w=800&q=80", StartDate = new DateTime(2026, 4, 24), EndDate = new DateTime(2026, 4, 28), CreatedAt = new DateTime(2026, 2, 3) },
                // Vienna
                new Tour { Id = 12, Title = "Imperial Vienna", DestinationId = 9, TourOperatorId = 1, CategoryId = 1, Description = "5 days in the Habsburg capital — Schönbrunn Palace, the State Opera, Belvedere, Viennese cafés, and a classical music concert.", PricePerPerson = 899, DurationDays = 5, MaxParticipants = 20, ImageUrl = "https://images.unsplash.com/photo-1516550893923-42d28e5677af?w=800&q=80", StartDate = new DateTime(2026, 10, 3), EndDate = new DateTime(2026, 10, 8), CreatedAt = new DateTime(2026, 2, 5) },
                // Santorini
                new Tour { Id = 13, Title = "Santorini — Island of Dreams", DestinationId = 10, TourOperatorId = 2, CategoryId = 2, Description = "7 days on the volcanic island of Santorini — sunsets from Oia, volcanic beaches, local wine tasting, and a cruise around the island.", PricePerPerson = 1399, DurationDays = 7, MaxParticipants = 14, ImageUrl = "https://images.unsplash.com/photo-1613395877344-13d4a8e0d49e?w=800&q=80", StartDate = new DateTime(2026, 7, 15), EndDate = new DateTime(2026, 7, 22), CreatedAt = new DateTime(2026, 2, 8) },
                new Tour { Id = 14, Title = "Santorini Weekend", DestinationId = 10, TourOperatorId = 4, CategoryId = 5, Description = "A 3-day weekend flight to Santorini — nights in a traditional Cycladic villa overlooking the caldera, and a private yacht tour.", PricePerPerson = 850, DurationDays = 3, MaxParticipants = 8, ImageUrl = "https://images.unsplash.com/photo-1570077188670-e3a8d69ac5ff?w=800&q=80", StartDate = new DateTime(2026, 8, 28), EndDate = new DateTime(2026, 8, 31), CreatedAt = new DateTime(2026, 2, 10) },
                // Balkan
                new Tour { Id = 15, Title = "Balkan Express — 10 Days", DestinationId = 5, TourOperatorId = 5, CategoryId = 4, Description = "An epic 10-day tour across the Balkans — Athens, Thessaloniki, Ohrid, and Budva. A blend of history, nature, and authentic Balkan cuisine.", PricePerPerson = 1100, DurationDays = 10, MaxParticipants = 28, ImageUrl = "https://images.unsplash.com/photo-1590147534648-1ac5cca59621?w=800&q=80", StartDate = new DateTime(2026, 9, 20), EndDate = new DateTime(2026, 9, 30), CreatedAt = new DateTime(2026, 2, 15) },
                // Lisbon
                new Tour { Id = 16, Title = "Lisbon Highlights", DestinationId = 11, TourOperatorId = 2, CategoryId = 3, Description = "5 magical days in Lisbon — ride the iconic Tram 28 through Alfama, explore Belém Tower, taste pastel de nata in historic cafés, and watch the sun set over the Tagus river.", PricePerPerson = 749, DiscountPercent = 10, DurationDays = 5, MaxParticipants = 20, ImageUrl = "https://images.unsplash.com/photo-1558370781-d6196949e317?w=800&q=80", StartDate = new DateTime(2026, 5, 5), EndDate = new DateTime(2026, 5, 10), CreatedAt = new DateTime(2026, 2, 18) },
                new Tour { Id = 17, Title = "Lisbon & Porto — Atlantic Coast", DestinationId = 11, TourOperatorId = 3, CategoryId = 3, Description = "A 7-day journey through Portugal's two most beloved cities — cobblestone Lisbon and the port wine cellars of Porto, with a day trip to Sintra's fairy-tale palaces.", PricePerPerson = 999, DurationDays = 7, MaxParticipants = 18, ImageUrl = "https://images.unsplash.com/photo-1555881400-74d7acaacd8b?w=800&q=80", StartDate = new DateTime(2026, 6, 12), EndDate = new DateTime(2026, 6, 19), CreatedAt = new DateTime(2026, 2, 20) },
                // Budapest
                new Tour { Id = 18, Title = "Budapest Royal", DestinationId = 12, TourOperatorId = 1, CategoryId = 3, Description = "4 days in one of Europe's most breathtaking capitals — Buda Castle, the Parliament building, a relaxing soak in Széchenyi Baths, and an evening cruise on the glittering Danube.", PricePerPerson = 649, DurationDays = 4, MaxParticipants = 22, ImageUrl = "https://images.unsplash.com/photo-1541343672885-9be56236302a?w=800&q=80", StartDate = new DateTime(2026, 4, 25), EndDate = new DateTime(2026, 4, 29), CreatedAt = new DateTime(2026, 2, 22) },
                new Tour { Id = 19, Title = "Budapest & Lake Balaton", DestinationId = 12, TourOperatorId = 5, CategoryId = 4, Description = "6 days combining the grandeur of Budapest with a relaxing escape to Lake Balaton — Central Europe's largest lake. Wine tastings, spa villages, and stunning lakeside sunsets included.", DiscountPercent = 12, PricePerPerson = 849, DurationDays = 6, MaxParticipants = 16, ImageUrl = "https://images.unsplash.com/photo-1549555340-9f9e3b0c5b0e?w=800&q=80", StartDate = new DateTime(2026, 7, 10), EndDate = new DateTime(2026, 7, 16), CreatedAt = new DateTime(2026, 2, 25) },
                // Mykonos
                new Tour { Id = 20, Title = "Mykonos Escape", DestinationId = 13, TourOperatorId = 2, CategoryId = 2, Description = "7 sun-soaked days on the most glamorous island in the Cyclades — party beaches, windmill sunsets, fresh seafood by the water, and day trips to the sacred island of Delos.", PricePerPerson = 1299, DurationDays = 7, MaxParticipants = 12, ImageUrl = "https://images.unsplash.com/photo-1601581875309-fafbf2d3ed3a?w=800&q=80", StartDate = new DateTime(2026, 7, 20), EndDate = new DateTime(2026, 7, 27), CreatedAt = new DateTime(2026, 3, 1) },
                new Tour { Id = 21, Title = "Mykonos & Santorini — Island Duo", DestinationId = 13, TourOperatorId = 4, CategoryId = 5, Description = "The ultimate Greek island experience — 4 nights on vibrant Mykonos followed by 4 nights on dreamy Santorini. Ferry included, boutique hotel, private sunset tour.", DiscountPercent = 8, PricePerPerson = 1799, DurationDays = 8, MaxParticipants = 10, ImageUrl = "https://images.unsplash.com/photo-1570077188670-e3a8d69ac5ff?w=800&q=80", StartDate = new DateTime(2026, 8, 5), EndDate = new DateTime(2026, 8, 13), CreatedAt = new DateTime(2026, 3, 2) },
                // More Rome
                new Tour { Id = 22, Title = "Rome Budget Explorer", DestinationId = 1, TourOperatorId = 1, CategoryId = 1, Description = "See all the must-see sights of Rome without breaking the bank — Colosseum, Pantheon, Piazza Navona, and the Vatican, with comfortable 3* accommodation and daily breakfast.", DiscountPercent = 20, PricePerPerson = 549, DurationDays = 4, MaxParticipants = 30, ImageUrl = "https://images.unsplash.com/photo-1515542622106-78bda8ba0e5b?w=800&q=80", StartDate = new DateTime(2026, 5, 15), EndDate = new DateTime(2026, 5, 19), CreatedAt = new DateTime(2026, 3, 3) },
                // Greek islands
                new Tour { Id = 23, Title = "Greek Island Hopping", DestinationId = 5, TourOperatorId = 2, CategoryId = 2, Description = "An epic 10-day island-hopping adventure — Athens, Mykonos, Paros, and Santorini. Ferry passes, guided excursions, and handpicked boutique hotels all included.", PricePerPerson = 1599, DurationDays = 10, MaxParticipants = 14, ImageUrl = "https://images.unsplash.com/photo-1533105079780-92b9be482077?w=800&q=80", StartDate = new DateTime(2026, 9, 8), EndDate = new DateTime(2026, 9, 18), CreatedAt = new DateTime(2026, 3, 4) },
                // Iberian
                new Tour { Id = 24, Title = "Spain & Portugal — Iberian Grand Tour", DestinationId = 3, TourOperatorId = 3, CategoryId = 4, Description = "8 days across the Iberian Peninsula — Barcelona, Madrid, Seville, and Lisbon. High-speed trains, flamenco show, wine tasting, and a blend of Moorish and modern Europe.", PricePerPerson = 1249, DurationDays = 8, MaxParticipants = 20, ImageUrl = "https://images.unsplash.com/photo-1558642452-9d2a7deb7f62?w=800&q=80", StartDate = new DateTime(2026, 10, 10), EndDate = new DateTime(2026, 10, 18), CreatedAt = new DateTime(2026, 3, 5) },
                // Winter Prague
                new Tour { Id = 25, Title = "Winter Magic in Prague", DestinationId = 6, TourOperatorId = 3, CategoryId = 3, Description = "Experience Prague in its most enchanting season — Christmas markets, steaming mulled wine, snow-covered rooftops, and the Old Town Square glowing in festive lights.", DiscountPercent = 15, PricePerPerson = 499, DurationDays = 4, MaxParticipants = 30, ImageUrl = "https://images.unsplash.com/photo-1467269204594-9661b134dd2b?w=800&q=80", StartDate = new DateTime(2026, 12, 5), EndDate = new DateTime(2026, 12, 9), CreatedAt = new DateTime(2026, 3, 6) }
            );

            // ══════════════════════════════
            //  TOUR IMAGES
            // ══════════════════════════════
            builder.Entity<TourImage>().HasData(
                new TourImage { Id = 1, TourId = 1, SortOrder = 1, ImageUrl = "https://images.unsplash.com/photo-1552832230-c0197dd311b5?w=1200&q=80" },
                new TourImage { Id = 2, TourId = 1, SortOrder = 2, ImageUrl = "https://images.unsplash.com/photo-1531572753322-ad063cecc140?w=1200&q=80" },
                new TourImage { Id = 3, TourId = 1, SortOrder = 3, ImageUrl = "https://images.unsplash.com/photo-1515542622106-78bda8ba0e5b?w=1200&q=80" },
                new TourImage { Id = 4, TourId = 1, SortOrder = 4, ImageUrl = "https://images.unsplash.com/photo-1571366343168-631c5bcca7a4?w=1200&q=80" },
                new TourImage { Id = 5, TourId = 2, SortOrder = 1, ImageUrl = "https://images.unsplash.com/photo-1531572753322-ad063cecc140?w=1200&q=80" },
                new TourImage { Id = 6, TourId = 2, SortOrder = 2, ImageUrl = "https://images.unsplash.com/photo-1552832230-c0197dd411b5?w=1200&q=80" },
                new TourImage { Id = 7, TourId = 2, SortOrder = 3, ImageUrl = "https://images.unsplash.com/photo-1490644658840-3f2e3f8c5625?w=1200&q=80" },
                new TourImage { Id = 8, TourId = 3, SortOrder = 1, ImageUrl = "https://images.unsplash.com/photo-1502602898657-3e91760cbb34?w=1200&q=80" },
                new TourImage { Id = 9, TourId = 3, SortOrder = 2, ImageUrl = "https://images.unsplash.com/photo-1499856871958-5b9627545d1a?w=1200&q=80" },
                new TourImage { Id = 10, TourId = 3, SortOrder = 3, ImageUrl = "https://images.unsplash.com/photo-1541171418693-a55571e5c25f?w=1200&q=80" },
                new TourImage { Id = 11, TourId = 3, SortOrder = 4, ImageUrl = "https://images.unsplash.com/photo-1467269204594-9661b134dd2b?w=1200&q=80" },
                new TourImage { Id = 12, TourId = 4, SortOrder = 1, ImageUrl = "https://images.unsplash.com/photo-1499856871958-5b9627545d1a?w=1200&q=80" },
                new TourImage { Id = 13, TourId = 4, SortOrder = 2, ImageUrl = "https://images.unsplash.com/photo-1502602898657-3e91760cbb34?w=1200&q=80" },
                new TourImage { Id = 14, TourId = 4, SortOrder = 3, ImageUrl = "https://images.unsplash.com/photo-1520939817895-060bdaf4fe1b?w=1200&q=80" },
                new TourImage { Id = 15, TourId = 5, SortOrder = 1, ImageUrl = "https://images.unsplash.com/photo-1539037116277-4db20889f2d4?w=1200&q=80" },
                new TourImage { Id = 16, TourId = 5, SortOrder = 2, ImageUrl = "https://images.unsplash.com/photo-1583422409516-2895a77efded?w=1200&q=80" },
                new TourImage { Id = 17, TourId = 5, SortOrder = 3, ImageUrl = "https://images.unsplash.com/photo-1561518776-e76a5e48f731?w=1200&q=80" },
                new TourImage { Id = 18, TourId = 5, SortOrder = 4, ImageUrl = "https://images.unsplash.com/photo-1558642452-9d2a7deb7f62?w=1200&q=80" },
                new TourImage { Id = 19, TourId = 6, SortOrder = 1, ImageUrl = "https://images.unsplash.com/photo-1555990793-da11153b2473?w=1200&q=80" },
                new TourImage { Id = 20, TourId = 6, SortOrder = 2, ImageUrl = "https://images.unsplash.com/photo-1600240644455-3edc55c375fe?w=1200&q=80" },
                new TourImage { Id = 21, TourId = 6, SortOrder = 3, ImageUrl = "https://images.unsplash.com/photo-1548625149-720834e8fa96?w=1200&q=80" },
                new TourImage { Id = 22, TourId = 7, SortOrder = 1, ImageUrl = "https://images.unsplash.com/photo-1555993539-1732b0258235?w=1200&q=80" },
                new TourImage { Id = 23, TourId = 7, SortOrder = 2, ImageUrl = "https://images.unsplash.com/photo-1603565816030-6b389eeb23cb?w=1200&q=80" },
                new TourImage { Id = 24, TourId = 7, SortOrder = 3, ImageUrl = "https://images.unsplash.com/photo-1568515387631-8b650bbcdb90?w=1200&q=80" },
                new TourImage { Id = 25, TourId = 7, SortOrder = 4, ImageUrl = "https://images.unsplash.com/photo-1541726260-e6b6a6a1b27e?w=1200&q=80" },
                new TourImage { Id = 26, TourId = 8, SortOrder = 1, ImageUrl = "https://images.unsplash.com/photo-1519677100203-a0e668c92439?w=1200&q=80" },
                new TourImage { Id = 27, TourId = 8, SortOrder = 2, ImageUrl = "https://images.unsplash.com/photo-1541849546-216549ae216d?w=1200&q=80" },
                new TourImage { Id = 28, TourId = 8, SortOrder = 3, ImageUrl = "https://images.unsplash.com/photo-1592906209472-a36b1f3782ef?w=1200&q=80" },
                new TourImage { Id = 29, TourId = 9, SortOrder = 1, ImageUrl = "https://images.unsplash.com/photo-1516550893923-42d28e5677af?w=1200&q=80" },
                new TourImage { Id = 30, TourId = 9, SortOrder = 2, ImageUrl = "https://images.unsplash.com/photo-1519677100203-a0e668c92439?w=1200&q=80" },
                new TourImage { Id = 31, TourId = 9, SortOrder = 3, ImageUrl = "https://images.unsplash.com/photo-1523731407965-2430cd12f5e4?w=1200&q=80" },
                new TourImage { Id = 32, TourId = 10, SortOrder = 1, ImageUrl = "https://images.unsplash.com/photo-1527838832700-5059252407fa?w=1200&q=80" },
                new TourImage { Id = 33, TourId = 10, SortOrder = 2, ImageUrl = "https://images.unsplash.com/photo-1541432901042-2d8bd64b4a9b?w=1200&q=80" },
                new TourImage { Id = 34, TourId = 10, SortOrder = 3, ImageUrl = "https://images.unsplash.com/photo-1524231757912-21f4fe3a7200?w=1200&q=80" },
                new TourImage { Id = 35, TourId = 10, SortOrder = 4, ImageUrl = "https://images.unsplash.com/photo-1570939274717-7eda259b50ed?w=1200&q=80" },
                new TourImage { Id = 36, TourId = 11, SortOrder = 1, ImageUrl = "https://images.unsplash.com/photo-1534351590666-13e3e96b5017?w=1200&q=80" },
                new TourImage { Id = 37, TourId = 11, SortOrder = 2, ImageUrl = "https://images.unsplash.com/photo-1512470876302-972faa2aa9a4?w=1200&q=80" },
                new TourImage { Id = 38, TourId = 11, SortOrder = 3, ImageUrl = "https://images.unsplash.com/photo-1576924542622-772281b13e94?w=1200&q=80" },
                new TourImage { Id = 39, TourId = 12, SortOrder = 1, ImageUrl = "https://images.unsplash.com/photo-1516550893923-42d28e5677af?w=1200&q=80" },
                new TourImage { Id = 40, TourId = 12, SortOrder = 2, ImageUrl = "https://images.unsplash.com/photo-1523731407965-2430cd12f5e4?w=1200&q=80" },
                new TourImage { Id = 41, TourId = 12, SortOrder = 3, ImageUrl = "https://images.unsplash.com/photo-1551882547-ff40c63fe5fa?w=1200&q=80" },
                new TourImage { Id = 42, TourId = 12, SortOrder = 4, ImageUrl = "https://images.unsplash.com/photo-1609767791926-c9e2e7ca33ac?w=1200&q=80" },
                new TourImage { Id = 43, TourId = 13, SortOrder = 1, ImageUrl = "https://images.unsplash.com/photo-1613395877344-13d4a8e0d49e?w=1200&q=80" },
                new TourImage { Id = 44, TourId = 13, SortOrder = 2, ImageUrl = "https://images.unsplash.com/photo-1570077188670-e3a8d69ac5ff?w=1200&q=80" },
                new TourImage { Id = 45, TourId = 13, SortOrder = 3, ImageUrl = "https://images.unsplash.com/photo-1533105079780-92b9be482077?w=1200&q=80" },
                new TourImage { Id = 46, TourId = 13, SortOrder = 4, ImageUrl = "https://images.unsplash.com/photo-1601581875309-fafbf2d3ed3a?w=1200&q=80" },
                new TourImage { Id = 47, TourId = 14, SortOrder = 1, ImageUrl = "https://images.unsplash.com/photo-1570077188670-e3a8d69ac5ff?w=1200&q=80" },
                new TourImage { Id = 48, TourId = 14, SortOrder = 2, ImageUrl = "https://images.unsplash.com/photo-1613395877344-13d4a8e0d49e?w=1200&q=80" },
                new TourImage { Id = 49, TourId = 14, SortOrder = 3, ImageUrl = "https://images.unsplash.com/photo-1551918120-9739cb430c6d?w=1200&q=80" },
                new TourImage { Id = 50, TourId = 15, SortOrder = 1, ImageUrl = "https://images.unsplash.com/photo-1590147534648-1ac5cca59621?w=1200&q=80" },
                new TourImage { Id = 51, TourId = 15, SortOrder = 2, ImageUrl = "https://images.unsplash.com/photo-1555993539-1732b0258235?w=1200&q=80" },
                new TourImage { Id = 52, TourId = 15, SortOrder = 3, ImageUrl = "https://images.unsplash.com/photo-1555990793-da11153b2473?w=1200&q=80" },
                new TourImage { Id = 53, TourId = 16, SortOrder = 1, ImageUrl = "https://images.unsplash.com/photo-1558370781-d6196949e317?w=1200&q=80" },
                new TourImage { Id = 54, TourId = 16, SortOrder = 2, ImageUrl = "https://images.unsplash.com/photo-1555881400-74d7acaacd8b?w=1200&q=80" },
                new TourImage { Id = 55, TourId = 16, SortOrder = 3, ImageUrl = "https://images.unsplash.com/photo-1513735492246-483525079686?w=1200&q=80" },
                new TourImage { Id = 56, TourId = 17, SortOrder = 1, ImageUrl = "https://images.unsplash.com/photo-1555881400-74d7acaacd8b?w=1200&q=80" },
                new TourImage { Id = 57, TourId = 17, SortOrder = 2, ImageUrl = "https://images.unsplash.com/photo-1558370781-d6196949e317?w=1200&q=80" },
                new TourImage { Id = 58, TourId = 17, SortOrder = 3, ImageUrl = "https://images.unsplash.com/photo-1513735492246-483525079686?w=1200&q=80" },
                new TourImage { Id = 59, TourId = 18, SortOrder = 1, ImageUrl = "https://images.unsplash.com/photo-1541343672885-9be56236302a?w=1200&q=80" },
                new TourImage { Id = 60, TourId = 18, SortOrder = 2, ImageUrl = "https://images.unsplash.com/photo-1565426873118-a17ed65d74b9?w=1200&q=80" },
                new TourImage { Id = 61, TourId = 18, SortOrder = 3, ImageUrl = "https://images.unsplash.com/photo-1507525428034-b723cf961d3e?w=1200&q=80" },
                new TourImage { Id = 62, TourId = 19, SortOrder = 1, ImageUrl = "https://images.unsplash.com/photo-1541343672885-9be56236302a?w=1200&q=80" },
                new TourImage { Id = 63, TourId = 19, SortOrder = 2, ImageUrl = "https://images.unsplash.com/photo-1549555340-9f9e3b0c5b0e?w=1200&q=80" },
                new TourImage { Id = 64, TourId = 19, SortOrder = 3, ImageUrl = "https://images.unsplash.com/photo-1516496636080-14fb876e029d?w=1200&q=80" },
                new TourImage { Id = 65, TourId = 20, SortOrder = 1, ImageUrl = "https://images.unsplash.com/photo-1601581875309-fafbf2d3ed3a?w=1200&q=80" },
                new TourImage { Id = 66, TourId = 20, SortOrder = 2, ImageUrl = "https://images.unsplash.com/photo-1533105079780-92b9be482077?w=1200&q=80" },
                new TourImage { Id = 67, TourId = 20, SortOrder = 3, ImageUrl = "https://images.unsplash.com/photo-1613395877344-13d4a8e0d49e?w=1200&q=80" },
                new TourImage { Id = 68, TourId = 21, SortOrder = 1, ImageUrl = "https://images.unsplash.com/photo-1570077188670-e3a8d69ac5ff?w=1200&q=80" },
                new TourImage { Id = 69, TourId = 21, SortOrder = 2, ImageUrl = "https://images.unsplash.com/photo-1601581875309-fafbf2d3ed3a?w=1200&q=80" },
                new TourImage { Id = 70, TourId = 21, SortOrder = 3, ImageUrl = "https://images.unsplash.com/photo-1613395877344-13d4a8e0d49e?w=1200&q=80" },
                new TourImage { Id = 71, TourId = 22, SortOrder = 1, ImageUrl = "https://images.unsplash.com/photo-1515542622106-78bda8ba0e5b?w=1200&q=80" },
                new TourImage { Id = 72, TourId = 22, SortOrder = 2, ImageUrl = "https://images.unsplash.com/photo-1552832230-c0197dd311b5?w=1200&q=80" },
                new TourImage { Id = 73, TourId = 22, SortOrder = 3, ImageUrl = "https://images.unsplash.com/photo-1571366343168-631c5bcca7a4?w=1200&q=80" },
                new TourImage { Id = 74, TourId = 23, SortOrder = 1, ImageUrl = "https://images.unsplash.com/photo-1533105079780-92b9be482077?w=1200&q=80" },
                new TourImage { Id = 75, TourId = 23, SortOrder = 2, ImageUrl = "https://images.unsplash.com/photo-1555993539-1732b0258235?w=1200&q=80" },
                new TourImage { Id = 76, TourId = 23, SortOrder = 3, ImageUrl = "https://images.unsplash.com/photo-1613395877344-13d4a8e0d49e?w=1200&q=80" },
                new TourImage { Id = 77, TourId = 24, SortOrder = 1, ImageUrl = "https://images.unsplash.com/photo-1558642452-9d2a7deb7f62?w=1200&q=80" },
                new TourImage { Id = 78, TourId = 24, SortOrder = 2, ImageUrl = "https://images.unsplash.com/photo-1539037116277-4db20889f2d4?w=1200&q=80" },
                new TourImage { Id = 79, TourId = 24, SortOrder = 3, ImageUrl = "https://images.unsplash.com/photo-1558370781-d6196949e317?w=1200&q=80" },
                new TourImage { Id = 80, TourId = 25, SortOrder = 1, ImageUrl = "https://images.unsplash.com/photo-1467269204594-9661b134dd2b?w=1200&q=80" },
                new TourImage { Id = 81, TourId = 25, SortOrder = 2, ImageUrl = "https://images.unsplash.com/photo-1519677100203-a0e668c92439?w=1200&q=80" },
                new TourImage { Id = 82, TourId = 25, SortOrder = 3, ImageUrl = "https://images.unsplash.com/photo-1541849546-216549ae216d?w=1200&q=80" }
            );
        }
    }
}