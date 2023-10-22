using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ProductUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMain",
                table: "ProductPhot");

            migrationBuilder.AddColumn<bool>(
                name: "IsMain",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "MainPhoto",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMain",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "MainPhoto",
                table: "Products");

            migrationBuilder.AddColumn<bool>(
                name: "IsMain",
                table: "ProductPhot",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
