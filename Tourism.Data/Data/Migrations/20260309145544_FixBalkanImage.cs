using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tourism.Data.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixBalkanImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 15,
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1590147534648-1ac5cca59621?q=80&w=1374&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 15,
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1601599561213-832382fd07ba?w=800&q=80");
        }
    }
}
