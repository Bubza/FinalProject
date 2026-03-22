using Tourism.Data.Models.Entities;
using Tourism.Services;
using Xunit;

namespace Tourism.Tests
{
    public class ReviewServiceTests
    {
        private static Destination MakeDestination() =>
            new Destination { Id = 1, Name = "Rome", Country = "Italy", Description = "D", ImageUrl = "img.jpg" };

        private static TourOperator MakeOperator() =>
            new TourOperator { Id = 1, Name = "Op", Description = "D", Email = "e@e.com", PhoneNumber = "123", LogoUrl = "logo.jpg" };

        private static Tour MakeTour(int id = 1) =>
            new Tour { Id = id, Title = $"Tour {id}", Description = "D", PricePerPerson = 100, DurationDays = 3, MaxParticipants = 20, ImageUrl = "img.jpg", StartDate = DateTime.UtcNow.AddMonths(1), EndDate = DateTime.UtcNow.AddMonths(1).AddDays(3), DestinationId = 1, TourOperatorId = 1 };

        private static Review MakeReview(int id, string userId, int tourId, int rating = 5, string comment = "Great!") =>
            new Review { Id = id, UserId = userId, TourId = tourId, Rating = rating, Comment = comment };

        [Fact]
        public async Task GetAllAsync_ReturnsAll()
        {
            using var ctx = TestDbContextFactory.Create();
            ctx.Destinations.Add(MakeDestination());
            ctx.TourOperators.Add(MakeOperator());
            ctx.Tours.Add(MakeTour());
            ctx.Reviews.AddRange(MakeReview(1, "u1", 1, 5), MakeReview(2, "u2", 1, 4));
            await ctx.SaveChangesAsync();
            var result = await new ReviewService(ctx).GetAllAsync();
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetAllAsync_ReturnsEmpty_WhenNone()
        {
            using var ctx = TestDbContextFactory.Create();
            Assert.Empty(await new ReviewService(ctx).GetAllAsync());
        }

        [Fact]
        public async Task GetByTourIdAsync_ReturnsOnlyTourReviews()
        {
            using var ctx = TestDbContextFactory.Create();
            ctx.Destinations.Add(MakeDestination());
            ctx.TourOperators.Add(MakeOperator());
            ctx.Tours.AddRange(MakeTour(1), MakeTour(2));
            ctx.Reviews.AddRange(MakeReview(1, "u1", 1), MakeReview(2, "u2", 2), MakeReview(3, "u3", 1));
            await ctx.SaveChangesAsync();
            var result = await new ReviewService(ctx).GetByTourIdAsync(1);
            Assert.Equal(2, result.Count());
            Assert.All(result, r => Assert.Equal(1, r.TourId));
        }

        [Fact]
        public async Task GetByTourIdAsync_ReturnsEmpty_WhenNone()
        {
            using var ctx = TestDbContextFactory.Create();
            Assert.Empty(await new ReviewService(ctx).GetByTourIdAsync(999));
        }

        [Fact]
        public async Task GetByUserIdAsync_ReturnsOnlyUserReviews()
        {
            using var ctx = TestDbContextFactory.Create();
            ctx.Destinations.Add(MakeDestination());
            ctx.TourOperators.Add(MakeOperator());
            ctx.Tours.AddRange(MakeTour(1), MakeTour(2));
            ctx.Reviews.AddRange(MakeReview(1, "u1", 1), MakeReview(2, "u2", 2), MakeReview(3, "u1", 2));
            await ctx.SaveChangesAsync();
            var result = await new ReviewService(ctx).GetByUserIdAsync("u1");
            Assert.Equal(2, result.Count());
            Assert.All(result, r => Assert.Equal("u1", r.UserId));
        }

        [Fact]
        public async Task GetByUserIdAsync_ReturnsEmpty_WhenNone()
        {
            using var ctx = TestDbContextFactory.Create();
            Assert.Empty(await new ReviewService(ctx).GetByUserIdAsync("ghost"));
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsCorrect()
        {
            using var ctx = TestDbContextFactory.Create();
            ctx.Destinations.Add(MakeDestination());
            ctx.TourOperators.Add(MakeOperator());
            ctx.Tours.Add(MakeTour());
            ctx.Reviews.Add(MakeReview(1, "u1", 1, 4, "Very good"));
            await ctx.SaveChangesAsync();
            var result = await new ReviewService(ctx).GetByIdAsync(1);
            Assert.NotNull(result);
            Assert.Equal(4, result!.Rating);
            Assert.Equal("Very good", result.Comment);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNull_WhenNotFound()
        {
            using var ctx = TestDbContextFactory.Create();
            Assert.Null(await new ReviewService(ctx).GetByIdAsync(999));
        }

        [Fact]
        public async Task CreateAsync_AddsReview()
        {
            using var ctx = TestDbContextFactory.Create();
            ctx.Destinations.Add(MakeDestination());
            ctx.TourOperators.Add(MakeOperator());
            ctx.Tours.Add(MakeTour());
            await ctx.SaveChangesAsync();
            await new ReviewService(ctx).CreateAsync(MakeReview(0, "u1", 1, 5, "Excellent!"));
            Assert.Single(ctx.Reviews);
        }

        [Fact]
        public async Task CreateAsync_SetsCreatedAt()
        {
            using var ctx = TestDbContextFactory.Create();
            ctx.Destinations.Add(MakeDestination());
            ctx.TourOperators.Add(MakeOperator());
            ctx.Tours.Add(MakeTour());
            await ctx.SaveChangesAsync();
            var review = MakeReview(0, "u1", 1);
            var before = DateTime.UtcNow.AddSeconds(-1);
            await new ReviewService(ctx).CreateAsync(review);
            Assert.True(review.CreatedAt >= before);
        }

        [Fact]
        public async Task CreateAsync_SavesRating()
        {
            using var ctx = TestDbContextFactory.Create();
            ctx.Destinations.Add(MakeDestination());
            ctx.TourOperators.Add(MakeOperator());
            ctx.Tours.Add(MakeTour());
            await ctx.SaveChangesAsync();
            await new ReviewService(ctx).CreateAsync(MakeReview(0, "u1", 1, rating: 3));
            Assert.Equal(3, ctx.Reviews.First().Rating);
        }

        [Fact]
        public async Task CreateAsync_AllowsMultipleReviews()
        {
            using var ctx = TestDbContextFactory.Create();
            ctx.Destinations.Add(MakeDestination());
            ctx.TourOperators.Add(MakeOperator());
            ctx.Tours.Add(MakeTour());
            await ctx.SaveChangesAsync();
            var svc = new ReviewService(ctx);
            await svc.CreateAsync(MakeReview(0, "u1", 1, 5));
            await svc.CreateAsync(MakeReview(0, "u2", 1, 3));
            Assert.Equal(2, ctx.Reviews.Count());
        }

        [Fact]
        public async Task DeleteAsync_RemovesReview()
        {
            using var ctx = TestDbContextFactory.Create();
            ctx.Destinations.Add(MakeDestination());
            ctx.TourOperators.Add(MakeOperator());
            ctx.Tours.Add(MakeTour());
            ctx.Reviews.Add(MakeReview(1, "u1", 1));
            await ctx.SaveChangesAsync();
            await new ReviewService(ctx).DeleteAsync(1);
            Assert.Empty(ctx.Reviews);
        }

        [Fact]
        public async Task DeleteAsync_DoesNotThrow_WhenNotFound()
        {
            using var ctx = TestDbContextFactory.Create();
            await new ReviewService(ctx).DeleteAsync(999);
            Assert.True(true);
        }

        [Fact]
        public async Task DeleteAsync_LeavesOthers()
        {
            using var ctx = TestDbContextFactory.Create();
            ctx.Destinations.Add(MakeDestination());
            ctx.TourOperators.Add(MakeOperator());
            ctx.Tours.Add(MakeTour());
            ctx.Reviews.AddRange(MakeReview(1, "u1", 1, 5, "Loved it"), MakeReview(2, "u2", 1, 4, "Good"));
            await ctx.SaveChangesAsync();
            await new ReviewService(ctx).DeleteAsync(1);
            Assert.Single(ctx.Reviews);
            Assert.Equal("Good", ctx.Reviews.First().Comment);
        }
    }
}