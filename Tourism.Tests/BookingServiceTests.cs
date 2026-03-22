using Tourism.Data.Models.Entities;
using Tourism.Data.Models.Enums;
using Tourism.Services;
using Xunit;

namespace Tourism.Tests
{
    public class BookingServiceTests
    {
        private static Destination MakeDestination() =>
            new Destination { Id = 1, Name = "Rome", Country = "Italy", Description = "D", ImageUrl = "img.jpg" };

        private static TourOperator MakeOperator() =>
            new TourOperator { Id = 1, Name = "Op", Description = "D", Email = "e@e.com", PhoneNumber = "123", LogoUrl = "logo.jpg" };

        private static Tour MakeTour(int id = 1) =>
            new Tour { Id = id, Title = "Tour", Description = "D", PricePerPerson = 100, DurationDays = 3, MaxParticipants = 20, ImageUrl = "img.jpg", StartDate = DateTime.UtcNow.AddMonths(1), EndDate = DateTime.UtcNow.AddMonths(1).AddDays(3), DestinationId = 1, TourOperatorId = 1 };

        private static Booking MakeBooking(int id, string userId, int tourId, int people = 2, BookingStatus status = BookingStatus.Pending, decimal price = 200) =>
            new Booking { Id = id, UserId = userId, TourId = tourId, NumberOfPeople = people, TotalPrice = price, Status = status };

        [Fact]
        public async Task GetAllAsync_ReturnsAll()
        {
            using var ctx = TestDbContextFactory.Create();
            ctx.Destinations.Add(MakeDestination());
            ctx.TourOperators.Add(MakeOperator());
            ctx.Tours.Add(MakeTour());
            ctx.Bookings.AddRange(MakeBooking(1, "u1", 1), MakeBooking(2, "u2", 1));
            await ctx.SaveChangesAsync();
            var result = await new BookingService(ctx).GetAllAsync();
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetAllAsync_ReturnsEmpty_WhenNone()
        {
            using var ctx = TestDbContextFactory.Create();
            var result = await new BookingService(ctx).GetAllAsync();
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetByUserIdAsync_ReturnsOnlyUsersBookings()
        {
            using var ctx = TestDbContextFactory.Create();
            ctx.Destinations.Add(MakeDestination());
            ctx.TourOperators.Add(MakeOperator());
            ctx.Tours.Add(MakeTour());
            ctx.Bookings.AddRange(MakeBooking(1, "u1", 1), MakeBooking(2, "u2", 1), MakeBooking(3, "u1", 1));
            await ctx.SaveChangesAsync();
            var result = await new BookingService(ctx).GetByUserIdAsync("u1");
            Assert.Equal(2, result.Count());
            Assert.All(result, b => Assert.Equal("u1", b.UserId));
        }

        [Fact]
        public async Task GetByUserIdAsync_ReturnsEmpty_WhenNoBookings()
        {
            using var ctx = TestDbContextFactory.Create();
            var result = await new BookingService(ctx).GetByUserIdAsync("nobody");
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsBooking()
        {
            using var ctx = TestDbContextFactory.Create();
            ctx.Destinations.Add(MakeDestination());
            ctx.TourOperators.Add(MakeOperator());
            ctx.Tours.Add(MakeTour());
            ctx.Bookings.Add(MakeBooking(1, "u1", 1, people: 3));
            await ctx.SaveChangesAsync();
            var result = await new BookingService(ctx).GetByIdAsync(1);
            Assert.NotNull(result);
            Assert.Equal(3, result!.NumberOfPeople);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNull_WhenNotFound()
        {
            using var ctx = TestDbContextFactory.Create();
            Assert.Null(await new BookingService(ctx).GetByIdAsync(999));
        }

        [Fact]
        public async Task CreateAsync_AddsBooking()
        {
            using var ctx = TestDbContextFactory.Create();
            ctx.Destinations.Add(MakeDestination());
            ctx.TourOperators.Add(MakeOperator());
            ctx.Tours.Add(MakeTour());
            await ctx.SaveChangesAsync();
            await new BookingService(ctx).CreateAsync(MakeBooking(0, "u1", 1));
            Assert.Equal(1, ctx.Bookings.Count());
        }

        [Fact]
        public async Task CreateAsync_SetsBookedAt()
        {
            using var ctx = TestDbContextFactory.Create();
            ctx.Destinations.Add(MakeDestination());
            ctx.TourOperators.Add(MakeOperator());
            ctx.Tours.Add(MakeTour());
            await ctx.SaveChangesAsync();
            var booking = MakeBooking(0, "u1", 1);
            var before = DateTime.UtcNow.AddSeconds(-1);
            await new BookingService(ctx).CreateAsync(booking);
            Assert.True(booking.BookedAt >= before);
        }

        [Fact]
        public async Task UpdateAsync_ChangesStatus()
        {
            using var ctx = TestDbContextFactory.Create();
            ctx.Destinations.Add(MakeDestination());
            ctx.TourOperators.Add(MakeOperator());
            ctx.Tours.Add(MakeTour());
            ctx.Bookings.Add(MakeBooking(1, "u1", 1, status: BookingStatus.Pending));
            await ctx.SaveChangesAsync();
            var b = ctx.Bookings.Find(1)!;
            b.Status = BookingStatus.Confirmed;
            await new BookingService(ctx).UpdateAsync(b);
            Assert.Equal(BookingStatus.Confirmed, ctx.Bookings.Find(1)!.Status);
        }

        [Fact]
        public async Task DeleteAsync_RemovesBooking()
        {
            using var ctx = TestDbContextFactory.Create();
            ctx.Destinations.Add(MakeDestination());
            ctx.TourOperators.Add(MakeOperator());
            ctx.Tours.Add(MakeTour());
            ctx.Bookings.Add(MakeBooking(1, "u1", 1));
            await ctx.SaveChangesAsync();
            await new BookingService(ctx).DeleteAsync(1);
            Assert.Empty(ctx.Bookings);
        }

        [Fact]
        public async Task DeleteAsync_DoesNotThrow_WhenNotFound()
        {
            using var ctx = TestDbContextFactory.Create();
            await new BookingService(ctx).DeleteAsync(999);
            Assert.True(true);
        }

        [Fact]
        public async Task DeleteAsync_LeavesOthers()
        {
            using var ctx = TestDbContextFactory.Create();
            ctx.Destinations.Add(MakeDestination());
            ctx.TourOperators.Add(MakeOperator());
            ctx.Tours.Add(MakeTour());
            ctx.Bookings.AddRange(MakeBooking(1, "u1", 1), MakeBooking(2, "u2", 1));
            await ctx.SaveChangesAsync();
            await new BookingService(ctx).DeleteAsync(1);
            Assert.Single(ctx.Bookings);
            Assert.Equal("u2", ctx.Bookings.First().UserId);
        }

        [Fact]
        public async Task GetBookedSpotsAsync_SumsExcludingCancelled()
        {
            using var ctx = TestDbContextFactory.Create();
            ctx.Destinations.Add(MakeDestination());
            ctx.TourOperators.Add(MakeOperator());
            ctx.Tours.Add(MakeTour());
            ctx.Bookings.AddRange(
                MakeBooking(1, "u1", 1, people: 3, status: BookingStatus.Confirmed),
                MakeBooking(2, "u2", 1, people: 2, status: BookingStatus.Pending),
                MakeBooking(3, "u3", 1, people: 5, status: BookingStatus.Cancelled));
            await ctx.SaveChangesAsync();
            var spots = await new BookingService(ctx).GetBookedSpotsAsync(1);
            Assert.Equal(5, spots);
        }

        [Fact]
        public async Task GetBookedSpotsAsync_ReturnsZero_WhenNone()
        {
            using var ctx = TestDbContextFactory.Create();
            ctx.Destinations.Add(MakeDestination());
            ctx.TourOperators.Add(MakeOperator());
            ctx.Tours.Add(MakeTour());
            await ctx.SaveChangesAsync();
            Assert.Equal(0, await new BookingService(ctx).GetBookedSpotsAsync(1));
        }

        [Fact]
        public async Task GetBookedSpotsAsync_ReturnsZero_WhenAllCancelled()
        {
            using var ctx = TestDbContextFactory.Create();
            ctx.Destinations.Add(MakeDestination());
            ctx.TourOperators.Add(MakeOperator());
            ctx.Tours.Add(MakeTour());
            ctx.Bookings.AddRange(
                MakeBooking(1, "u1", 1, people: 4, status: BookingStatus.Cancelled),
                MakeBooking(2, "u2", 1, people: 6, status: BookingStatus.Cancelled));
            await ctx.SaveChangesAsync();
            Assert.Equal(0, await new BookingService(ctx).GetBookedSpotsAsync(1));
        }

        [Fact]
        public async Task GetBookedSpotsAsync_OnlyCountsSpecifiedTour()
        {
            using var ctx = TestDbContextFactory.Create();
            ctx.Destinations.Add(MakeDestination());
            ctx.TourOperators.Add(MakeOperator());
            ctx.Tours.AddRange(MakeTour(1), MakeTour(2));
            ctx.Bookings.AddRange(
                MakeBooking(1, "u1", 1, people: 3, status: BookingStatus.Confirmed),
                MakeBooking(2, "u2", 2, people: 7, status: BookingStatus.Confirmed));
            await ctx.SaveChangesAsync();
            Assert.Equal(3, await new BookingService(ctx).GetBookedSpotsAsync(1));
        }

        [Fact]
        public async Task GetBookedSpotsAsync_IncludesCompleted()
        {
            using var ctx = TestDbContextFactory.Create();
            ctx.Destinations.Add(MakeDestination());
            ctx.TourOperators.Add(MakeOperator());
            ctx.Tours.Add(MakeTour());
            ctx.Bookings.AddRange(
                MakeBooking(1, "u1", 1, people: 2, status: BookingStatus.Completed),
                MakeBooking(2, "u2", 1, people: 3, status: BookingStatus.Confirmed));
            await ctx.SaveChangesAsync();
            Assert.Equal(5, await new BookingService(ctx).GetBookedSpotsAsync(1));
        }
    }
}