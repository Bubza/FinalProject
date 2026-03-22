using Tourism.Data.Models.Entities;
using Tourism.Services;
using Xunit;

namespace Tourism.Tests
{
    public class TourServiceTests
    {
        private static Destination MakeDestination(int id = 1, string name = "Rome", string country = "Italy") =>
            new Destination { Id = id, Name = name, Country = country, Description = "D", ImageUrl = "img.jpg" };

        private static TourOperator MakeOperator(int id = 1) =>
            new TourOperator { Id = id, Name = "Op", Description = "D", Email = "e@e.com", PhoneNumber = "123", LogoUrl = "logo.jpg" };

        private static Tour MakeTour(int id, string title, int destinationId = 1, int operatorId = 1, decimal price = 100, decimal discount = 0) =>
            new Tour
            {
                Id = id,
                Title = title,
                Description = "Desc",
                PricePerPerson = price,
                DiscountPercent = discount,
                DurationDays = 3,
                MaxParticipants = 20,
                ImageUrl = "img.jpg",
                StartDate = DateTime.UtcNow.AddMonths(1),
                EndDate = DateTime.UtcNow.AddMonths(1).AddDays(3),
                DestinationId = destinationId,
                TourOperatorId = operatorId
            };

        [Fact]
        public async Task GetAllAsync_ReturnsAllTours()
        {
            using var ctx = TestDbContextFactory.Create();
            ctx.Destinations.Add(MakeDestination());
            ctx.TourOperators.Add(MakeOperator());
            ctx.Tours.AddRange(MakeTour(1, "Tour A"), MakeTour(2, "Tour B"));
            await ctx.SaveChangesAsync();
            var result = await new TourService(ctx).GetAllAsync();
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetAllAsync_ReturnsEmpty_WhenNoTours()
        {
            using var ctx = TestDbContextFactory.Create();
            var result = await new TourService(ctx).GetAllAsync();
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsCorrectTour()
        {
            using var ctx = TestDbContextFactory.Create();
            ctx.Destinations.Add(MakeDestination());
            ctx.TourOperators.Add(MakeOperator());
            ctx.Tours.Add(MakeTour(1, "Classic Rome"));
            await ctx.SaveChangesAsync();
            var result = await new TourService(ctx).GetByIdAsync(1);
            Assert.NotNull(result);
            Assert.Equal("Classic Rome", result.Title);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNull_WhenNotFound()
        {
            using var ctx = TestDbContextFactory.Create();
            var result = await new TourService(ctx).GetByIdAsync(999);
            Assert.Null(result);
        }

        [Fact]
        public async Task GetByIdAsync_IncludesDestination()
        {
            using var ctx = TestDbContextFactory.Create();
            ctx.Destinations.Add(MakeDestination(1, "Paris", "France"));
            ctx.TourOperators.Add(MakeOperator());
            ctx.Tours.Add(MakeTour(1, "Paris Tour"));
            await ctx.SaveChangesAsync();
            var result = await new TourService(ctx).GetByIdAsync(1);
            Assert.Equal("Paris", result!.Destination.Name);
        }

        [Fact]
        public async Task CreateAsync_AddsTour()
        {
            using var ctx = TestDbContextFactory.Create();
            ctx.Destinations.Add(MakeDestination());
            ctx.TourOperators.Add(MakeOperator());
            await ctx.SaveChangesAsync();
            await new TourService(ctx).CreateAsync(MakeTour(0, "New Tour"));
            Assert.Equal(1, ctx.Tours.Count());
        }

        [Fact]
        public async Task CreateAsync_SetsCreatedAt()
        {
            using var ctx = TestDbContextFactory.Create();
            ctx.Destinations.Add(MakeDestination());
            ctx.TourOperators.Add(MakeOperator());
            await ctx.SaveChangesAsync();
            var tour = MakeTour(0, "New Tour");
            var before = DateTime.UtcNow.AddSeconds(-1);
            await new TourService(ctx).CreateAsync(tour);
            Assert.True(tour.CreatedAt >= before);
        }

        [Fact]
        public async Task CreateAsync_SavesTitle()
        {
            using var ctx = TestDbContextFactory.Create();
            ctx.Destinations.Add(MakeDestination());
            ctx.TourOperators.Add(MakeOperator());
            await ctx.SaveChangesAsync();
            await new TourService(ctx).CreateAsync(MakeTour(0, "Santorini Adventure"));
            Assert.Equal("Santorini Adventure", ctx.Tours.First().Title);
        }

        [Fact]
        public async Task UpdateAsync_UpdatesTitle()
        {
            using var ctx = TestDbContextFactory.Create();
            ctx.Destinations.Add(MakeDestination());
            ctx.TourOperators.Add(MakeOperator());
            ctx.Tours.Add(MakeTour(1, "Old Title"));
            await ctx.SaveChangesAsync();
            await new TourService(ctx).UpdateAsync(MakeTour(1, "New Title"));
            Assert.Equal("New Title", ctx.Tours.Find(1)!.Title);
        }

        [Fact]
        public async Task UpdateAsync_UpdatesDiscount()
        {
            using var ctx = TestDbContextFactory.Create();
            ctx.Destinations.Add(MakeDestination());
            ctx.TourOperators.Add(MakeOperator());
            ctx.Tours.Add(MakeTour(1, "Tour", price: 200, discount: 0));
            await ctx.SaveChangesAsync();
            await new TourService(ctx).UpdateAsync(MakeTour(1, "Tour", price: 200, discount: 20));
            Assert.Equal(20, ctx.Tours.Find(1)!.DiscountPercent);
        }

        [Fact]
        public async Task DeleteAsync_RemovesTour()
        {
            using var ctx = TestDbContextFactory.Create();
            ctx.Destinations.Add(MakeDestination());
            ctx.TourOperators.Add(MakeOperator());
            ctx.Tours.Add(MakeTour(1, "Rome Tour"));
            await ctx.SaveChangesAsync();
            await new TourService(ctx).DeleteAsync(1);
            Assert.Empty(ctx.Tours);
        }

        [Fact]
        public async Task DeleteAsync_DoesNotThrow_WhenNotFound()
        {
            using var ctx = TestDbContextFactory.Create();
            await new TourService(ctx).DeleteAsync(999);
            Assert.True(true);
        }

        [Fact]
        public async Task DeleteAsync_LeavesOtherTours()
        {
            using var ctx = TestDbContextFactory.Create();
            ctx.Destinations.Add(MakeDestination());
            ctx.TourOperators.Add(MakeOperator());
            ctx.Tours.AddRange(MakeTour(1, "Tour A"), MakeTour(2, "Tour B"));
            await ctx.SaveChangesAsync();
            await new TourService(ctx).DeleteAsync(1);
            Assert.Single(ctx.Tours);
            Assert.Equal("Tour B", ctx.Tours.First().Title);
        }

        [Fact]
        public async Task SearchAsync_ByTitle()
        {
            using var ctx = TestDbContextFactory.Create();
            ctx.Destinations.Add(MakeDestination(1, "Rome", "Italy"));
            ctx.TourOperators.Add(MakeOperator());
            ctx.Tours.AddRange(MakeTour(1, "Classic Rome"), MakeTour(2, "Paris Escape"));
            await ctx.SaveChangesAsync();
            var result = await new TourService(ctx).SearchAsync("Classic", null, null);
            Assert.Single(result);
        }

        [Fact]
        public async Task SearchAsync_ByCountry()
        {
            using var ctx = TestDbContextFactory.Create();
            ctx.Destinations.AddRange(MakeDestination(1, "Rome", "Italy"), MakeDestination(2, "Athens", "Greece"));
            ctx.TourOperators.Add(MakeOperator());
            ctx.Tours.AddRange(MakeTour(1, "Rome Tour", 1), MakeTour(2, "Athens Tour", 2));
            await ctx.SaveChangesAsync();
            var result = await new TourService(ctx).SearchAsync("Greece", null, null);
            Assert.Single(result);
            Assert.Equal("Athens Tour", result.First().Title);
        }

        [Fact]
        public async Task SearchAsync_ByMaxPrice()
        {
            using var ctx = TestDbContextFactory.Create();
            ctx.Destinations.Add(MakeDestination());
            ctx.TourOperators.Add(MakeOperator());
            ctx.Tours.AddRange(MakeTour(1, "Cheap", price: 200), MakeTour(2, "Expensive", price: 1000));
            await ctx.SaveChangesAsync();
            var result = await new TourService(ctx).SearchAsync(null, null, 500);
            Assert.Single(result);
            Assert.Equal("Cheap", result.First().Title);
        }

        [Fact]
        public async Task SearchAsync_NullParams_ReturnsAll()
        {
            using var ctx = TestDbContextFactory.Create();
            ctx.Destinations.Add(MakeDestination());
            ctx.TourOperators.Add(MakeOperator());
            ctx.Tours.AddRange(MakeTour(1, "A"), MakeTour(2, "B"), MakeTour(3, "C"));
            await ctx.SaveChangesAsync();
            var result = await new TourService(ctx).SearchAsync(null, null, null);
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public async Task SearchAsync_ByDestinationId()
        {
            using var ctx = TestDbContextFactory.Create();
            ctx.Destinations.AddRange(MakeDestination(1, "Rome", "Italy"), MakeDestination(2, "Paris", "France"));
            ctx.TourOperators.Add(MakeOperator());
            ctx.Tours.AddRange(MakeTour(1, "Tour A", 1), MakeTour(2, "Tour B", 2));
            await ctx.SaveChangesAsync();
            var result = await new TourService(ctx).SearchAsync(null, 2, null);
            Assert.Single(result);
            Assert.Equal("Tour B", result.First().Title);
        }

        [Fact]
        public async Task GetRecommendationsAsync_ReturnsEmpty_WhenTourNotFound()
        {
            using var ctx = TestDbContextFactory.Create();
            var result = await new TourService(ctx).GetRecommendationsAsync(999, null);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetRecommendationsAsync_ExcludesCurrentTour()
        {
            using var ctx = TestDbContextFactory.Create();
            ctx.Destinations.Add(MakeDestination());
            ctx.TourOperators.Add(MakeOperator());
            ctx.Tours.AddRange(MakeTour(1, "A"), MakeTour(2, "B"), MakeTour(3, "C"));
            await ctx.SaveChangesAsync();
            var result = await new TourService(ctx).GetRecommendationsAsync(1, null, 3);
            Assert.DoesNotContain(result, t => t.Id == 1);
        }

        [Fact]
        public async Task GetRecommendationsAsync_ReturnsCorrectCount()
        {
            using var ctx = TestDbContextFactory.Create();
            ctx.Destinations.Add(MakeDestination());
            ctx.TourOperators.Add(MakeOperator());
            ctx.Tours.AddRange(MakeTour(1, "A"), MakeTour(2, "B"), MakeTour(3, "C"), MakeTour(4, "D"));
            await ctx.SaveChangesAsync();
            var result = await new TourService(ctx).GetRecommendationsAsync(1, null, 3);
            Assert.True(result.Count() <= 3);
        }

        [Fact]
        public async Task GetRecommendationsAsync_PrioritisesSameDestination()
        {
            using var ctx = TestDbContextFactory.Create();
            ctx.Destinations.AddRange(MakeDestination(1, "Rome", "Italy"), MakeDestination(2, "Paris", "France"));
            ctx.TourOperators.Add(MakeOperator());
            ctx.Tours.AddRange(MakeTour(1, "Rome A", 1), MakeTour(2, "Rome B", 1), MakeTour(3, "Paris A", 2));
            await ctx.SaveChangesAsync();
            var result = await new TourService(ctx).GetRecommendationsAsync(1, null, 2);
            Assert.Contains(result, t => t.Id == 2);
        }
    }
}