using Tourism.Data.Models.Entities;
using Tourism.Services;
using Xunit;

namespace Tourism.Tests
{
    public class FavoriteTourServiceTests
    {
        private static Destination MakeDestination() =>
            new Destination { Id = 1, Name = "Rome", Country = "Italy", Description = "D", ImageUrl = "img.jpg" };

        private static TourOperator MakeOperator() =>
            new TourOperator { Id = 1, Name = "Op", Description = "D", Email = "e@e.com", PhoneNumber = "123", LogoUrl = "logo.jpg" };

        private static Tour MakeTour(int id = 1) =>
            new Tour { Id = id, Title = $"Tour {id}", Description = "D", PricePerPerson = 100, DurationDays = 3, MaxParticipants = 20, ImageUrl = "img.jpg", StartDate = DateTime.UtcNow.AddMonths(1), EndDate = DateTime.UtcNow.AddMonths(1).AddDays(3), DestinationId = 1, TourOperatorId = 1 };

        [Fact]
        public async Task IsFavoriteAsync_ReturnsTrue_WhenExists()
        {
            using var ctx = TestDbContextFactory.Create();
            ctx.Destinations.Add(MakeDestination());
            ctx.TourOperators.Add(MakeOperator());
            ctx.Tours.Add(MakeTour());
            ctx.FavoriteTours.Add(new FavoriteTour { UserId = "u1", TourId = 1 });
            await ctx.SaveChangesAsync();
            Assert.True(await new FavoriteTourService(ctx).IsFavoriteAsync("u1", 1));
        }

        [Fact]
        public async Task IsFavoriteAsync_ReturnsFalse_WhenNotExists()
        {
            using var ctx = TestDbContextFactory.Create();
            ctx.Destinations.Add(MakeDestination());
            ctx.TourOperators.Add(MakeOperator());
            ctx.Tours.Add(MakeTour());
            await ctx.SaveChangesAsync();
            Assert.False(await new FavoriteTourService(ctx).IsFavoriteAsync("u1", 1));
        }

        [Fact]
        public async Task IsFavoriteAsync_ReturnsFalse_ForDifferentUser()
        {
            using var ctx = TestDbContextFactory.Create();
            ctx.Destinations.Add(MakeDestination());
            ctx.TourOperators.Add(MakeOperator());
            ctx.Tours.Add(MakeTour());
            ctx.FavoriteTours.Add(new FavoriteTour { UserId = "u1", TourId = 1 });
            await ctx.SaveChangesAsync();
            Assert.False(await new FavoriteTourService(ctx).IsFavoriteAsync("u2", 1));
        }

        [Fact]
        public async Task IsFavoriteAsync_ReturnsFalse_ForDifferentTour()
        {
            using var ctx = TestDbContextFactory.Create();
            ctx.Destinations.Add(MakeDestination());
            ctx.TourOperators.Add(MakeOperator());
            ctx.Tours.AddRange(MakeTour(1), MakeTour(2));
            ctx.FavoriteTours.Add(new FavoriteTour { UserId = "u1", TourId = 1 });
            await ctx.SaveChangesAsync();
            Assert.False(await new FavoriteTourService(ctx).IsFavoriteAsync("u1", 2));
        }

        [Fact]
        public async Task AddAsync_CreatesFavorite()
        {
            using var ctx = TestDbContextFactory.Create();
            ctx.Destinations.Add(MakeDestination());
            ctx.TourOperators.Add(MakeOperator());
            ctx.Tours.Add(MakeTour());
            await ctx.SaveChangesAsync();
            await new FavoriteTourService(ctx).AddAsync("u1", 1);
            Assert.Single(ctx.FavoriteTours);
        }

        [Fact]
        public async Task AddAsync_DoesNotDuplicate()
        {
            using var ctx = TestDbContextFactory.Create();
            ctx.Destinations.Add(MakeDestination());
            ctx.TourOperators.Add(MakeOperator());
            ctx.Tours.Add(MakeTour());
            ctx.FavoriteTours.Add(new FavoriteTour { UserId = "u1", TourId = 1 });
            await ctx.SaveChangesAsync();
            await new FavoriteTourService(ctx).AddAsync("u1", 1);
            Assert.Single(ctx.FavoriteTours);
        }

        [Fact]
        public async Task AddAsync_AllowsDifferentUsers()
        {
            using var ctx = TestDbContextFactory.Create();
            ctx.Destinations.Add(MakeDestination());
            ctx.TourOperators.Add(MakeOperator());
            ctx.Tours.Add(MakeTour());
            await ctx.SaveChangesAsync();
            var svc = new FavoriteTourService(ctx);
            await svc.AddAsync("u1", 1);
            await svc.AddAsync("u2", 1);
            Assert.Equal(2, ctx.FavoriteTours.Count());
        }

        [Fact]
        public async Task RemoveAsync_DeletesFavorite()
        {
            using var ctx = TestDbContextFactory.Create();
            ctx.Destinations.Add(MakeDestination());
            ctx.TourOperators.Add(MakeOperator());
            ctx.Tours.Add(MakeTour());
            ctx.FavoriteTours.Add(new FavoriteTour { UserId = "u1", TourId = 1 });
            await ctx.SaveChangesAsync();
            await new FavoriteTourService(ctx).RemoveAsync("u1", 1);
            Assert.Empty(ctx.FavoriteTours);
        }

        [Fact]
        public async Task RemoveAsync_DoesNotThrow_WhenNotExists()
        {
            using var ctx = TestDbContextFactory.Create();
            await new FavoriteTourService(ctx).RemoveAsync("u1", 99);
            Assert.True(true);
        }

        [Fact]
        public async Task RemoveAsync_LeavesOthers()
        {
            using var ctx = TestDbContextFactory.Create();
            ctx.Destinations.Add(MakeDestination());
            ctx.TourOperators.Add(MakeOperator());
            ctx.Tours.AddRange(MakeTour(1), MakeTour(2));
            ctx.FavoriteTours.AddRange(
                new FavoriteTour { UserId = "u1", TourId = 1 },
                new FavoriteTour { UserId = "u1", TourId = 2 });
            await ctx.SaveChangesAsync();
            await new FavoriteTourService(ctx).RemoveAsync("u1", 1);
            Assert.Single(ctx.FavoriteTours);
            Assert.Equal(2, ctx.FavoriteTours.First().TourId);
        }

        [Fact]
        public async Task GetByUserIdAsync_ReturnsOnlyUsersFavorites()
        {
            using var ctx = TestDbContextFactory.Create();
            ctx.Destinations.Add(MakeDestination());
            ctx.TourOperators.Add(MakeOperator());
            ctx.Tours.AddRange(MakeTour(1), MakeTour(2));
            ctx.FavoriteTours.AddRange(
                new FavoriteTour { UserId = "u1", TourId = 1 },
                new FavoriteTour { UserId = "u2", TourId = 2 });
            await ctx.SaveChangesAsync();
            var result = await new FavoriteTourService(ctx).GetByUserIdAsync("u1");
            Assert.Single(result);
            Assert.Equal("u1", result.First().UserId);
        }

        [Fact]
        public async Task GetByUserIdAsync_ReturnsEmpty_WhenNone()
        {
            using var ctx = TestDbContextFactory.Create();
            var result = await new FavoriteTourService(ctx).GetByUserIdAsync("ghost");
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetByUserIdAsync_IncludesTour()
        {
            using var ctx = TestDbContextFactory.Create();
            ctx.Destinations.Add(MakeDestination());
            ctx.TourOperators.Add(MakeOperator());
            ctx.Tours.Add(MakeTour(1));
            ctx.FavoriteTours.Add(new FavoriteTour { UserId = "u1", TourId = 1 });
            await ctx.SaveChangesAsync();
            var result = await new FavoriteTourService(ctx).GetByUserIdAsync("u1");
            Assert.NotNull(result.First().Tour);
        }
    }
}