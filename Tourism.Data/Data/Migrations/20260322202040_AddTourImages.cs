using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Tourism.Data.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTourImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TourImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TourId = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourImages_Tours_TourId",
                        column: x => x.TourId,
                        principalTable: "Tours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TourImages",
                columns: new[] { "Id", "ImageUrl", "SortOrder", "TourId" },
                values: new object[,]
                {
                    { 1, "https://images.unsplash.com/photo-1552832230-c0197dd311b5?w=1200&q=80", 1, 1 },
                    { 2, "https://images.unsplash.com/photo-1531572753322-ad063cecc140?w=1200&q=80", 2, 1 },
                    { 3, "https://images.unsplash.com/photo-1515542622106-78bda8ba0e5b?w=1200&q=80", 3, 1 },
                    { 4, "https://images.unsplash.com/photo-1571366343168-631c5bcca7a4?w=1200&q=80", 4, 1 },
                    { 5, "https://images.unsplash.com/photo-1531572753322-ad063cecc140?w=1200&q=80", 1, 2 },
                    { 6, "https://images.unsplash.com/photo-1552832230-c0197dd311b5?w=1200&q=80", 2, 2 },
                    { 7, "https://images.unsplash.com/photo-1490644658840-3f2e3f8c5625?w=1200&q=80", 3, 2 },
                    { 8, "https://images.unsplash.com/photo-1502602898657-3e91760cbb34?w=1200&q=80", 1, 3 },
                    { 9, "https://images.unsplash.com/photo-1499856871958-5b9627545d1a?w=1200&q=80", 2, 3 },
                    { 10, "https://images.unsplash.com/photo-1541171418693-a55571e5c25f?w=1200&q=80", 3, 3 },
                    { 11, "https://images.unsplash.com/photo-1467269204594-9661b134dd2b?w=1200&q=80", 4, 3 },
                    { 12, "https://images.unsplash.com/photo-1499856871958-5b9627545d1a?w=1200&q=80", 1, 4 },
                    { 13, "https://images.unsplash.com/photo-1502602898657-3e91760cbb34?w=1200&q=80", 2, 4 },
                    { 14, "https://images.unsplash.com/photo-1520939817895-060bdaf4fe1b?w=1200&q=80", 3, 4 },
                    { 15, "https://images.unsplash.com/photo-1539037116277-4db20889f2d4?w=1200&q=80", 1, 5 },
                    { 16, "https://images.unsplash.com/photo-1583422409516-2895a77efded?w=1200&q=80", 2, 5 },
                    { 17, "https://images.unsplash.com/photo-1561518776-e76a5e48f731?w=1200&q=80", 3, 5 },
                    { 18, "https://images.unsplash.com/photo-1558642452-9d2a7deb7f62?w=1200&q=80", 4, 5 },
                    { 19, "https://images.unsplash.com/photo-1555990793-da11153b2473?w=1200&q=80", 1, 6 },
                    { 20, "https://images.unsplash.com/photo-1600240644455-3edc55c375fe?w=1200&q=80", 2, 6 },
                    { 21, "https://images.unsplash.com/photo-1548625149-720834e8fa96?w=1200&q=80", 3, 6 },
                    { 22, "https://images.unsplash.com/photo-1555993539-1732b0258235?w=1200&q=80", 1, 7 },
                    { 23, "https://images.unsplash.com/photo-1603565816030-6b389eeb23cb?w=1200&q=80", 2, 7 },
                    { 24, "https://images.unsplash.com/photo-1568515387631-8b650bbcdb90?w=1200&q=80", 3, 7 },
                    { 25, "https://images.unsplash.com/photo-1541726260-e6b6a6a1b27e?w=1200&q=80", 4, 7 },
                    { 26, "https://images.unsplash.com/photo-1519677100203-a0e668c92439?w=1200&q=80", 1, 8 },
                    { 27, "https://images.unsplash.com/photo-1541849546-216549ae216d?w=1200&q=80", 2, 8 },
                    { 28, "https://images.unsplash.com/photo-1592906209472-a36b1f3782ef?w=1200&q=80", 3, 8 },
                    { 29, "https://images.unsplash.com/photo-1516550893923-42d28e5677af?w=1200&q=80", 1, 9 },
                    { 30, "https://images.unsplash.com/photo-1519677100203-a0e668c92439?w=1200&q=80", 2, 9 },
                    { 31, "https://images.unsplash.com/photo-1523731407965-2430cd12f5e4?w=1200&q=80", 3, 9 },
                    { 32, "https://images.unsplash.com/photo-1527838832700-5059252407fa?w=1200&q=80", 1, 10 },
                    { 33, "https://images.unsplash.com/photo-1541432901042-2d8bd64b4a9b?w=1200&q=80", 2, 10 },
                    { 34, "https://images.unsplash.com/photo-1524231757912-21f4fe3a7200?w=1200&q=80", 3, 10 },
                    { 35, "https://images.unsplash.com/photo-1570939274717-7eda259b50ed?w=1200&q=80", 4, 10 },
                    { 36, "https://images.unsplash.com/photo-1534351590666-13e3e96b5017?w=1200&q=80", 1, 11 },
                    { 37, "https://images.unsplash.com/photo-1512470876302-972faa2aa9a4?w=1200&q=80", 2, 11 },
                    { 38, "https://images.unsplash.com/photo-1576924542622-772281b13e94?w=1200&q=80", 3, 11 },
                    { 39, "https://images.unsplash.com/photo-1516550893923-42d28e5677af?w=1200&q=80", 1, 12 },
                    { 40, "https://images.unsplash.com/photo-1523731407965-2430cd12f5e4?w=1200&q=80", 2, 12 },
                    { 41, "https://images.unsplash.com/photo-1551882547-ff40c63fe5fa?w=1200&q=80", 3, 12 },
                    { 42, "https://images.unsplash.com/photo-1609767791926-c9e2e7ca33ac?w=1200&q=80", 4, 12 },
                    { 43, "https://images.unsplash.com/photo-1613395877344-13d4a8e0d49e?w=1200&q=80", 1, 13 },
                    { 44, "https://images.unsplash.com/photo-1570077188670-e3a8d69ac5ff?w=1200&q=80", 2, 13 },
                    { 45, "https://images.unsplash.com/photo-1533105079780-92b9be482077?w=1200&q=80", 3, 13 },
                    { 46, "https://images.unsplash.com/photo-1601581875309-fafbf2d3ed3a?w=1200&q=80", 4, 13 },
                    { 47, "https://images.unsplash.com/photo-1570077188670-e3a8d69ac5ff?w=1200&q=80", 1, 14 },
                    { 48, "https://images.unsplash.com/photo-1613395877344-13d4a8e0d49e?w=1200&q=80", 2, 14 },
                    { 49, "https://images.unsplash.com/photo-1551918120-9739cb430c6d?w=1200&q=80", 3, 14 },
                    { 50, "https://images.unsplash.com/photo-1590147534648-1ac5cca59621?w=1200&q=80", 1, 15 },
                    { 51, "https://images.unsplash.com/photo-1555993539-1732b0258235?w=1200&q=80", 2, 15 },
                    { 52, "https://images.unsplash.com/photo-1555990793-da11153b2473?w=1200&q=80", 3, 15 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TourImages_TourId",
                table: "TourImages",
                column: "TourId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TourImages");
        }
    }
}
