using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tourism.Data.Models.Entities;
using Tourism.Data.Models.Enums;

namespace Tourism.Data.Configurations
{
    public static class DemoDataSeeder
    {
        // Tourist accounts
        private static readonly (string Email, string First, string Last, string Password)[] _users =
        {
            ("maria.ivanova@demo.bg",   "Maria",  "Ivanova",   "Demo123!"),
            ("petar.georgiev@demo.bg",  "Petar",  "Georgiev",  "Demo123!"),
            ("elena.todorova@demo.bg",  "Elena",  "Todorova",  "Demo123!"),
            ("stefan.dimitrov@demo.bg", "Stefan", "Dimitrov",  "Demo123!"),
            ("anna.mileva@demo.bg",   "Anna",   "Mileva",  "Demo123!"),
        };

        // Operator accounts (email, first, last, password, TourOperatorId)
        private static readonly (string Email, string First, string Last, string Password, int OperatorId)[] _operatorUsers =
        {
            ("balkan@operator.bg",  "Ivan",   "Ivanov",     "Operator123!", 1),
            ("sun@operator.bg",     "Sophia", "Dimitrova", "Operator123!", 2),
            ("europa@operator.bg",  "Martin", "Georgiev",   "Operator123!", 3),
            ("orient@operator.bg",  "Yasen",  "Yasenov",  "Operator123!", 4),
            ("alpine@operator.bg",  "Nikola", "Plamenov",  "Operator123!", 5),
        };

        // Correct image URLs per tour (TourId -> list of image URLs in order)
        private static readonly Dictionary<int, string[]> _tourImages = new()
        {
            // Tour 1 - Classic Rome
            [1] = new[]
            {
                "https://images.unsplash.com/photo-1552832230-c0197dd311b5?w=1200&q=80",
                "https://images.unsplash.com/photo-1515542622106-78bda8ba0e5b?w=1200&q=80",
                "https://images.unsplash.com/photo-1529260830199-42c24126f198?w=1200&q=80",
                "https://images.unsplash.com/photo-1524231757912-21f4fe3a7200?w=1200&q=80"
            },
            // Tour 2 - Rome & Vatican VIP
            [2] = new[]
            {
                "https://images.unsplash.com/photo-1531572753322-ad063cecc140?w=1200&q=80",
                "https://images.unsplash.com/photo-1552832230-c0197dd311b5?w=1200&q=80",
                "https://images.unsplash.com/photo-1490644658840-3f2e3f8c5625?w=1200&q=80"
            },
            // Tour 3 - Romantic Paris
            [3] = new[]
            {
                "https://images.unsplash.com/photo-1502602898657-3e91760cbb34?w=1200&q=80",
                "https://images.unsplash.com/photo-1499856871958-5b9627545d1a?w=1200&q=80",
                "https://images.unsplash.com/photo-1564594985565-9e9de4f1e343?w=1200&q=80",
                "https://images.unsplash.com/photo-1467269204594-9661b134dd2b?w=1200&q=80"
            },
            // Tour 4 - Paris Weekend
            [4] = new[]
            {
                "https://images.unsplash.com/photo-1499856871958-5b9627545d1a?w=1200&q=80",
                "https://images.unsplash.com/photo-1502602898657-3e91760cbb34?w=1200&q=80",
                "https://images.unsplash.com/photo-1520939817895-060bdaf4fe1b?w=1200&q=80"
            },
            // Tour 5 - Barcelona & Gaudi
            [5] = new[]
            {
                "https://images.unsplash.com/photo-1539037116277-4db20889f2d4?w=1200&q=80",
                "https://images.unsplash.com/photo-1583422409516-2895a77efded?w=1200&q=80",
                "https://images.unsplash.com/photo-1561518776-e76a5e48f731?w=1200&q=80",
                "https://images.unsplash.com/photo-1558642452-9d2a7deb7f62?w=1200&q=80"
            },
            // Tour 6 - Adriatic Pearl (Dubrovnik)
            [6] = new[]
            {
                "https://images.unsplash.com/photo-1555990793-da11153b2473?w=1200&q=80",
                "https://images.unsplash.com/photo-1600240644455-3edc55c375fe?w=1200&q=80",
                "https://images.unsplash.com/photo-1548625149-720834e8fa96?w=1200&q=80"
            },
            // Tour 7 - Athens Adventure
            [7] = new[]
            {
                "https://images.unsplash.com/photo-1555993539-1732b0258235?w=1200&q=80",
                "https://images.unsplash.com/photo-1603565816030-6b389eeb23cb?w=1200&q=80",
                "https://images.unsplash.com/photo-1568515387631-8b650bbcdb90?w=1200&q=80",
                "https://images.unsplash.com/photo-1512467459616-0f2a0e0e8e41?w=1200&q=80"
            },
            // Tour 8 - Magic of Prague
            [8] = new[]
            {
                "https://images.unsplash.com/photo-1519677100203-a0e668c92439?w=1200&q=80",
                "https://images.unsplash.com/photo-1541849546-216549ae216d?w=1200&q=80",
                "https://images.unsplash.com/photo-1592906209472-a36b1f3782ef?w=1200&q=80"
            },
            // Tour 9 - Prague & Vienna 8 Days
            [9] = new[]
            {
                "https://images.unsplash.com/photo-1516550893923-42d28e5677af?w=1200&q=80",
                "https://images.unsplash.com/photo-1519677100203-a0e668c92439?w=1200&q=80",
                "https://images.unsplash.com/photo-1523731407965-2430cd12f5e4?w=1200&q=80"
            },
            // Tour 10 - Istanbul Bridge of Worlds
            [10] = new[]
            {
                "https://images.unsplash.com/photo-1527838832700-5059252407fa?w=1200&q=80",
                "https://images.unsplash.com/photo-1541432901042-2d8bd64b4a9b?w=1200&q=80",
                "https://images.unsplash.com/photo-1524231757912-21f4fe3a7200?w=1200&q=80",
                "https://images.unsplash.com/photo-1570939274717-7eda259b50ed?w=1200&q=80"
            },
            // Tour 11 - Amsterdam City of Canals
            [11] = new[]
            {
                "https://images.unsplash.com/photo-1534351590666-13e3e96b5017?w=1200&q=80",
                "https://images.unsplash.com/photo-1512470876302-972faa2aa9a4?w=1200&q=80",
                "https://images.unsplash.com/photo-1576924542622-772281b13e94?w=1200&q=80"
            },
            // Tour 12 - Imperial Vienna
            [12] = new[]
            {
                "https://images.unsplash.com/photo-1516550893923-42d28e5677af?w=1200&q=80",
                "https://images.unsplash.com/photo-1523731407965-2430cd12f5e4?w=1200&q=80",
                "https://images.unsplash.com/photo-1551882547-ff40c63fe5fa?w=1200&q=80",
                "https://images.unsplash.com/photo-1609767791926-c9e2e7ca33ac?w=1200&q=80"
            },
            // Tour 13 - Santorini Island of Dreams
            [13] = new[]
            {
                "https://images.unsplash.com/photo-1613395877344-13d4a8e0d49e?w=1200&q=80",
                "https://images.unsplash.com/photo-1570077188670-e3a8d69ac5ff?w=1200&q=80",
                "https://images.unsplash.com/photo-1533105079780-92b9be482077?w=1200&q=80",
                "https://images.unsplash.com/photo-1601581875309-fafbf2d3ed3a?w=1200&q=80"
            },
            // Tour 14 - Santorini Weekend
            [14] = new[]
            {
                "https://images.unsplash.com/photo-1570077188670-e3a8d69ac5ff?w=1200&q=80",
                "https://images.unsplash.com/photo-1613395877344-13d4a8e0d49e?w=1200&q=80",
                "https://images.unsplash.com/photo-1601581875309-fafbf2d3ed3a?w=1200&q=80"
            },
            // Tour 15 - Balkan Express
            [15] = new[]
            {
                "https://images.unsplash.com/photo-1590147534648-1ac5cca59621?w=1200&q=80",
                "https://images.unsplash.com/photo-1555993539-1732b0258235?w=1200&q=80",
                "https://images.unsplash.com/photo-1555990793-da11153b2473?w=1200&q=80"
            },
            // Tour 16 - Lisbon Highlights
            [16] = new[]
            {
                "https://images.unsplash.com/photo-1558370781-d6196949e317?w=1200&q=80",
                "https://images.unsplash.com/photo-1555881400-74d7acaacd8b?w=1200&q=80",
                "https://images.unsplash.com/photo-1513735492246-483525079686?w=1200&q=80"
            },
            // Tour 17 - Lisbon & Porto
            [17] = new[]
            {
                "https://images.unsplash.com/photo-1555881400-74d7acaacd8b?w=1200&q=80",
                "https://images.unsplash.com/photo-1558370781-d6196949e317?w=1200&q=80",
                "https://images.unsplash.com/photo-1513735492246-483525079686?w=1200&q=80"
            },
            // Tour 18 - Budapest Royal
            [18] = new[]
            {
                "https://images.unsplash.com/photo-1541343672885-9be56236302a?w=1200&q=80",
                "https://images.unsplash.com/photo-1565426873118-a17ed65d74b9?w=1200&q=80",
                "https://images.unsplash.com/photo-1507525428034-b723cf961d3e?w=1200&q=80"
            },
            // Tour 19 - Budapest & Lake Balaton
            [19] = new[]
            {
                "https://images.unsplash.com/photo-1541343672885-9be56236302a?w=1200&q=80",
                "https://images.unsplash.com/photo-1549555340-9f9e3b0c5b0e?w=1200&q=80",
                "https://images.unsplash.com/photo-1516496636080-14fb876e029d?w=1200&q=80"
            },
            // Tour 20 - Mykonos Escape
            [20] = new[]
            {
                "https://images.unsplash.com/photo-1601581875309-fafbf2d3ed3a?w=1200&q=80",
                "https://images.unsplash.com/photo-1533105079780-92b9be482077?w=1200&q=80",
                "https://images.unsplash.com/photo-1613395877344-13d4a8e0d49e?w=1200&q=80"
            },
            // Tour 21 - Mykonos & Santorini Island Duo
            [21] = new[]
            {
                "https://images.unsplash.com/photo-1570077188670-e3a8d69ac5ff?w=1200&q=80",
                "https://images.unsplash.com/photo-1601581875309-fafbf2d3ed3a?w=1200&q=80",
                "https://images.unsplash.com/photo-1613395877344-13d4a8e0d49e?w=1200&q=80"
            },
            // Tour 22 - Rome Budget Explorer
            [22] = new[]
            {
                "https://images.unsplash.com/photo-1515542622106-78bda8ba0e5b?w=1200&q=80",
                "https://images.unsplash.com/photo-1552832230-c0197dd311b5?w=1200&q=80",
                "https://images.unsplash.com/photo-1529260830199-42c24126f198?w=1200&q=80"
            },
            // Tour 23 - Greek Island Hopping
            [23] = new[]
            {
                "https://images.unsplash.com/photo-1533105079780-92b9be482077?w=1200&q=80",
                "https://images.unsplash.com/photo-1555993539-1732b0258235?w=1200&q=80",
                "https://images.unsplash.com/photo-1613395877344-13d4a8e0d49e?w=1200&q=80"
            },
            // Tour 24 - Spain & Portugal Iberian Grand Tour
            [24] = new[]
            {
                "https://images.unsplash.com/photo-1558642452-9d2a7deb7f62?w=1200&q=80",
                "https://images.unsplash.com/photo-1539037116277-4db20889f2d4?w=1200&q=80",
                "https://images.unsplash.com/photo-1558370781-d6196949e317?w=1200&q=80"
            },
            // Tour 25 - Winter Magic in Prague
            [25] = new[]
            {
                "https://images.unsplash.com/photo-1467269204594-9661b134dd2b?w=1200&q=80",
                "https://images.unsplash.com/photo-1519677100203-a0e668c92439?w=1200&q=80",
                "https://images.unsplash.com/photo-1541849546-216549ae216d?w=1200&q=80"
            }
        };

        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var db = serviceProvider.GetRequiredService<ApplicationDbContext>();

            // 1. Create tourist users
            var ids = new Dictionary<string, string>();
            foreach (var (email, first, last, password) in _users)
            {
                var existing = await userManager.FindByEmailAsync(email);
                if (existing == null)
                {
                    var user = new ApplicationUser
                    {
                        UserName = email,
                        Email = email,
                        EmailConfirmed = true,
                        FirstName = first,
                        LastName = last,
                    };
                    await userManager.CreateAsync(user, password);
                    existing = await userManager.FindByEmailAsync(email);
                }
                ids[email] = existing!.Id;
            }

            // 2. Create operator users and link to TourOperator records
            foreach (var (email, first, last, password, operatorId) in _operatorUsers)
            {
                var existing = await userManager.FindByEmailAsync(email);
                if (existing == null)
                {
                    var user = new ApplicationUser
                    {
                        UserName = email,
                        Email = email,
                        EmailConfirmed = true,
                        FirstName = first,
                        LastName = last,
                    };
                    await userManager.CreateAsync(user, password);
                    await userManager.AddToRoleAsync(user, "Operator");
                    existing = await userManager.FindByEmailAsync(email);
                }

                // Link to TourOperator row if not already linked
                var op = await db.TourOperators.FindAsync(operatorId);
                if (op != null && op.UserId == null)
                {
                    op.UserId = existing!.Id;
                    await db.SaveChangesAsync();
                }
            }

            // 3. Bookings
            if (!db.Bookings.Any())
            {
                var maria = ids["maria.ivanova@demo.bg"];
                var petar = ids["petar.georgiev@demo.bg"];
                var elena = ids["elena.todorova@demo.bg"];
                var stefan = ids["stefan.dimitrov@demo.bg"];
                var anna = ids["anna.mileva@demo.bg"];

                db.Bookings.AddRange(
                    // Maria
                    new Booking { UserId = maria, TourId = 1, NumberOfPeople = 2, TotalPrice = 1798m, Status = BookingStatus.Completed, BookedAt = new DateTime(2026, 1, 15) },
                    new Booking { UserId = maria, TourId = 5, NumberOfPeople = 2, TotalPrice = 1598m, Status = BookingStatus.Confirmed, BookedAt = new DateTime(2026, 2, 20) },
                    new Booking { UserId = maria, TourId = 13, NumberOfPeople = 1, TotalPrice = 1399m, Status = BookingStatus.Pending, BookedAt = new DateTime(2026, 3, 10) },
                    // Petar
                    new Booking { UserId = petar, TourId = 3, NumberOfPeople = 3, TotalPrice = 3597m, Status = BookingStatus.Completed, BookedAt = new DateTime(2026, 1, 20) },
                    new Booking { UserId = petar, TourId = 8, NumberOfPeople = 2, TotalPrice = 1098m, Status = BookingStatus.Completed, BookedAt = new DateTime(2026, 1, 25) },
                    new Booking { UserId = petar, TourId = 10, NumberOfPeople = 4, TotalPrice = 2916m, Status = BookingStatus.Confirmed, BookedAt = new DateTime(2026, 2, 15) },
                    new Booking { UserId = petar, TourId = 16, NumberOfPeople = 2, TotalPrice = 1348m, Status = BookingStatus.Pending, BookedAt = new DateTime(2026, 3, 15) },
                    // Elena
                    new Booking { UserId = elena, TourId = 7, NumberOfPeople = 2, TotalPrice = 1298m, Status = BookingStatus.Completed, BookedAt = new DateTime(2026, 1, 10) },
                    new Booking { UserId = elena, TourId = 11, NumberOfPeople = 3, TotalPrice = 2457m, Status = BookingStatus.Pending, BookedAt = new DateTime(2026, 3, 5) },
                    new Booking { UserId = elena, TourId = 2, NumberOfPeople = 1, TotalPrice = 1250m, Status = BookingStatus.Confirmed, BookedAt = new DateTime(2026, 2, 25) },
                    new Booking { UserId = elena, TourId = 18, NumberOfPeople = 2, TotalPrice = 1298m, Status = BookingStatus.Pending, BookedAt = new DateTime(2026, 3, 20) },
                    // Stefan
                    new Booking { UserId = stefan, TourId = 6, NumberOfPeople = 4, TotalPrice = 2796m, Status = BookingStatus.Completed, BookedAt = new DateTime(2026, 1, 5) },
                    new Booking { UserId = stefan, TourId = 9, NumberOfPeople = 2, TotalPrice = 1960m, Status = BookingStatus.Confirmed, BookedAt = new DateTime(2026, 2, 10) },
                    new Booking { UserId = stefan, TourId = 15, NumberOfPeople = 2, TotalPrice = 2200m, Status = BookingStatus.Pending, BookedAt = new DateTime(2026, 3, 12) },
                    new Booking { UserId = stefan, TourId = 20, NumberOfPeople = 2, TotalPrice = 2598m, Status = BookingStatus.Pending, BookedAt = new DateTime(2026, 3, 22) },
                    // Anna
                    new Booking { UserId = anna, TourId = 4, NumberOfPeople = 2, TotalPrice = 1298m, Status = BookingStatus.Completed, BookedAt = new DateTime(2026, 1, 8) },
                    new Booking { UserId = anna, TourId = 12, NumberOfPeople = 2, TotalPrice = 1798m, Status = BookingStatus.Confirmed, BookedAt = new DateTime(2026, 2, 5) },
                    new Booking { UserId = anna, TourId = 14, NumberOfPeople = 1, TotalPrice = 850m, Status = BookingStatus.Pending, BookedAt = new DateTime(2026, 3, 18) },
                    new Booking { UserId = anna, TourId = 22, NumberOfPeople = 3, TotalPrice = 1317m, Status = BookingStatus.Pending, BookedAt = new DateTime(2026, 3, 25) }
                );
                await db.SaveChangesAsync();
            }

            // 4. Reviews (only on tours with a Completed booking)
            if (!db.Reviews.Any())
            {
                var maria = ids["maria.ivanova@demo.bg"];
                var petar = ids["petar.georgiev@demo.bg"];
                var elena = ids["elena.todorova@demo.bg"];
                var stefan = ids["stefan.dimitrov@demo.bg"];
                var anna = ids["anna.mileva@demo.bg"];

                db.Reviews.AddRange(
                    new Review
                    {
                        UserId = maria,
                        TourId = 1,
                        Rating = 5,
                        Comment = "Absolutely breathtaking! The Colosseum visit left us speechless. Our guide was incredibly knowledgeable and made history come alive. The 4-star hotel was perfectly located. Would book again in a heartbeat!",
                        CreatedAt = new DateTime(2026, 1, 20)
                    },
                    new Review
                    {
                        UserId = petar,
                        TourId = 3,
                        Rating = 5,
                        Comment = "Paris exceeded every expectation. The Seine cruise at sunset was magical, and the Louvre tour was perfectly paced. Travelling as a group of 3, we felt looked after the whole time. Highly recommended for couples and families alike!",
                        CreatedAt = new DateTime(2026, 1, 28)
                    },
                    new Review
                    {
                        UserId = petar,
                        TourId = 8,
                        Rating = 4,
                        Comment = "Prague is a fairy-tale city and this tour does it justice. Charles Bridge at dawn was worth the early wake-up call. Only minor gripe was one of the pub stops being a bit touristy — but overall a fantastic 4 days.",
                        CreatedAt = new DateTime(2026, 2, 1)
                    },
                    new Review
                    {
                        UserId = elena,
                        TourId = 7,
                        Rating = 5,
                        Comment = "Athens is simply extraordinary. Standing on the Acropolis and looking out over the city gave me chills. The traditional Greek dinner with live bouzouki music was a highlight I'll never forget. Perfect organisation from start to finish.",
                        CreatedAt = new DateTime(2026, 1, 17)
                    },
                    new Review
                    {
                        UserId = stefan,
                        TourId = 6,
                        Rating = 4,
                        Comment = "Dubrovnik lived up to the hype. Walking the ancient city walls with the Adriatic shimmering below was surreal. The Lokrum Island day trip was a great bonus. Travelled with 4 people — everyone had an amazing time.",
                        CreatedAt = new DateTime(2026, 1, 12)
                    },
                    new Review
                    {
                        UserId = anna,
                        TourId = 4,
                        Rating = 5,
                        Comment = "The perfect Paris weekend — compact, well-organised, and genuinely romantic. We squeezed in everything we wanted in just 3 days. The hotel was charming and the included transport made everything stress-free. Will definitely come back for the longer Paris tour!",
                        CreatedAt = new DateTime(2026, 1, 13)
                    }
                );
                await db.SaveChangesAsync();
            }

            // 5. Favorites
            if (!db.FavoriteTours.Any())
            {
                var maria = ids["maria.ivanova@demo.bg"];
                var petar = ids["petar.georgiev@demo.bg"];
                var elena = ids["elena.todorova@demo.bg"];
                var stefan = ids["stefan.dimitrov@demo.bg"];
                var anna = ids["anna.mileva@demo.bg"];

                db.FavoriteTours.AddRange(
                    new FavoriteTour { UserId = maria, TourId = 2, AddedAt = new DateTime(2026, 1, 16) },
                    new FavoriteTour { UserId = maria, TourId = 13, AddedAt = new DateTime(2026, 2, 5) },
                    new FavoriteTour { UserId = maria, TourId = 20, AddedAt = new DateTime(2026, 3, 1) },
                    new FavoriteTour { UserId = maria, TourId = 16, AddedAt = new DateTime(2026, 3, 8) },
                    new FavoriteTour { UserId = petar, TourId = 1, AddedAt = new DateTime(2026, 1, 22) },
                    new FavoriteTour { UserId = petar, TourId = 10, AddedAt = new DateTime(2026, 2, 16) },
                    new FavoriteTour { UserId = petar, TourId = 15, AddedAt = new DateTime(2026, 2, 28) },
                    new FavoriteTour { UserId = petar, TourId = 23, AddedAt = new DateTime(2026, 3, 10) },
                    new FavoriteTour { UserId = elena, TourId = 3, AddedAt = new DateTime(2026, 1, 11) },
                    new FavoriteTour { UserId = elena, TourId = 6, AddedAt = new DateTime(2026, 2, 20) },
                    new FavoriteTour { UserId = elena, TourId = 21, AddedAt = new DateTime(2026, 3, 6) },
                    new FavoriteTour { UserId = stefan, TourId = 7, AddedAt = new DateTime(2026, 1, 6) },
                    new FavoriteTour { UserId = stefan, TourId = 12, AddedAt = new DateTime(2026, 2, 11) },
                    new FavoriteTour { UserId = stefan, TourId = 9, AddedAt = new DateTime(2026, 2, 18) },
                    new FavoriteTour { UserId = stefan, TourId = 24, AddedAt = new DateTime(2026, 3, 14) },
                    new FavoriteTour { UserId = anna, TourId = 8, AddedAt = new DateTime(2026, 1, 9) },
                    new FavoriteTour { UserId = anna, TourId = 14, AddedAt = new DateTime(2026, 2, 6) },
                    new FavoriteTour { UserId = anna, TourId = 3, AddedAt = new DateTime(2026, 2, 22) },
                    new FavoriteTour { UserId = anna, TourId = 25, AddedAt = new DateTime(2026, 3, 20) }
                );
                await db.SaveChangesAsync();
            }

            // 6. Payments (for Completed bookings)
            if (!db.Payments.Any())
            {
                var completedBookings = db.Bookings
                    .Where(b => b.Status == Tourism.Data.Models.Enums.BookingStatus.Completed)
                    .ToList();

                foreach (var b in completedBookings)
                {
                    db.Payments.Add(new Tourism.Data.Models.Entities.Payment
                    {
                        BookingId = b.Id,
                        Amount = b.TotalPrice,
                        Method = "Borica",
                        Status = "Paid",
                        TransactionId = "TXN-" + Guid.NewGuid().ToString("N")[..12].ToUpper(),
                        PaidAt = b.BookedAt.AddHours(1)
                    });
                }
                await db.SaveChangesAsync();
            }

            // 7. Contact Messages
            if (!db.ContactMessages.Any())
            {
                db.ContactMessages.AddRange(
                    new Tourism.Data.Models.Entities.ContactMessage
                    {
                        Name = "Maria Ivanova",
                        Email = "maria.ivanova@demo.bg",
                        Subject = "Group booking enquiry — Rome",
                        Message = "Hello! I'm interested in booking the Classic Rome tour for a group of 8 people in May. Do you offer any group discounts? We would also like to know if vegetarian meal options are available throughout the trip. Looking forward to hearing from you!",
                        SentAt = new DateTime(2026, 2, 10),
                        IsRead = true
                    },
                    new Tourism.Data.Models.Entities.ContactMessage
                    {
                        Name = "Petar Georgiev",
                        Email = "petar.georgiev@demo.bg",
                        Subject = "Question about Prague & Vienna tour",
                        Message = "Hi, I booked the Prague & Vienna 8-day tour and wanted to ask about the coach transfer between the two cities. What time does it depart from Prague and how long is the journey? Also, is there an option to upgrade to a 4-star hotel in Vienna? Thanks in advance.",
                        SentAt = new DateTime(2026, 2, 18),
                        IsRead = true
                    },
                    new Tourism.Data.Models.Entities.ContactMessage
                    {
                        Name = "Stefan Dimitrov",
                        Email = "stefan.dimitrov@demo.bg",
                        Subject = "Cancellation policy",
                        Message = "Good afternoon. I would like to understand your cancellation policy for the Balkan Express tour. If I need to cancel 30 days before departure, what percentage of the total price is refunded? Thank you.",
                        SentAt = new DateTime(2026, 3, 5),
                        IsRead = false
                    },
                    new Tourism.Data.Models.Entities.ContactMessage
                    {
                        Name = "Anna Mileva",
                        Email = "anna.mileva@demo.bg",
                        Subject = "Accessibility — Santorini Weekend",
                        Message = "Hello! I am interested in the Santorini Weekend tour but I have limited mobility. Could you let me know whether the tour is suitable for someone who uses a walking stick, and whether the villa has ground-floor rooms available? Many thanks.",
                        SentAt = new DateTime(2026, 3, 15),
                        IsRead = false
                    },
                    new Tourism.Data.Models.Entities.ContactMessage
                    {
                        Name = "John Smith",
                        Email = "john.smith@email.com",
                        Subject = "Partnership enquiry",
                        Message = "Dear NaviGo team, I represent a boutique hotel chain in Greece and Croatia and would be very interested in discussing a partnership opportunity. We believe our properties would be an excellent fit for several of your tours. Please feel free to contact me at your earliest convenience.",
                        SentAt = new DateTime(2026, 3, 20),
                        IsRead = false
                    }
                );
                await db.SaveChangesAsync();
            }

            // 8. Fix TourImage URLs — runs every startup to ensure correct destination photos
            await FixTourImageUrlsAsync(db);
        }

        private static async Task FixTourImageUrlsAsync(ApplicationDbContext db)
        {
            var allImages = await db.TourImages.ToListAsync();
            bool changed = false;

            foreach (var (tourId, urls) in _tourImages)
            {
                var tourImages = allImages
                    .Where(i => i.TourId == tourId)
                    .OrderBy(i => i.SortOrder)
                    .ToList();

                for (int i = 0; i < Math.Min(tourImages.Count, urls.Length); i++)
                {
                    if (tourImages[i].ImageUrl != urls[i])
                    {
                        tourImages[i].ImageUrl = urls[i];
                        changed = true;
                    }
                }
            }

            if (changed)
                await db.SaveChangesAsync();

        }
    }
}