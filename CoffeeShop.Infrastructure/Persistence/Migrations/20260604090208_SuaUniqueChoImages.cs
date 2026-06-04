using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeShop.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SuaUniqueChoImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Images_ReferenceId_Type_IsPrimary",
                table: "Images");

            migrationBuilder.CreateIndex(
                name: "IX_Images_ReferenceId_Type",
                table: "Images",
                columns: new[] { "ReferenceId", "Type" },
                unique: true,
                filter: "[IsPrimary] = 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Images_ReferenceId_Type",
                table: "Images");

            migrationBuilder.CreateIndex(
                name: "IX_Images_ReferenceId_Type_IsPrimary",
                table: "Images",
                columns: new[] { "ReferenceId", "Type", "IsPrimary" });
        }
    }
}
