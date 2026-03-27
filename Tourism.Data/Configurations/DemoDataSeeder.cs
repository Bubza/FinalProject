using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Tourism.Data.Models.Entities;
using Tourism.Data.Models.Enums;

namespace Tourism.Data.Seeding
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
                var anna = ids["anna.kristeva@demo.bg"];

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
                var anna = ids["anna.kristeva@demo.bg"];

                db.FavoriteTours.AddRange(
                    // Maria
                    new FavoriteTour { UserId = maria, TourId = 2, AddedAt = new DateTime(2026, 1, 16) },
                    new FavoriteTour { UserId = maria, TourId = 13, AddedAt = new DateTime(2026, 2, 5) },
                    new FavoriteTour { UserId = maria, TourId = 20, AddedAt = new DateTime(2026, 3, 1) },
                    new FavoriteTour { UserId = maria, TourId = 16, AddedAt = new DateTime(2026, 3, 8) },
                    // Petar
                    new FavoriteTour { UserId = petar, TourId = 1, AddedAt = new DateTime(2026, 1, 22) },
                    new FavoriteTour { UserId = petar, TourId = 10, AddedAt = new DateTime(2026, 2, 16) },
                    new FavoriteTour { UserId = petar, TourId = 15, AddedAt = new DateTime(2026, 2, 28) },
                    new FavoriteTour { UserId = petar, TourId = 23, AddedAt = new DateTime(2026, 3, 10) },
                    // Elena
                    new FavoriteTour { UserId = elena, TourId = 3, AddedAt = new DateTime(2026, 1, 11) },
                    new FavoriteTour { UserId = elena, TourId = 6, AddedAt = new DateTime(2026, 2, 20) },
                    new FavoriteTour { UserId = elena, TourId = 21, AddedAt = new DateTime(2026, 3, 6) },
                    // Stefan
                    new FavoriteTour { UserId = stefan, TourId = 7, AddedAt = new DateTime(2026, 1, 6) },
                    new FavoriteTour { UserId = stefan, TourId = 12, AddedAt = new DateTime(2026, 2, 11) },
                    new FavoriteTour { UserId = stefan, TourId = 9, AddedAt = new DateTime(2026, 2, 18) },
                    new FavoriteTour { UserId = stefan, TourId = 24, AddedAt = new DateTime(2026, 3, 14) },
                    // Anna
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
                // Fetch completed booking IDs — these are the ones that have a payment
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
                        Name = "Anna Kristeva",
                        Email = "anna.kristeva@demo.bg",
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
        }
    }
}