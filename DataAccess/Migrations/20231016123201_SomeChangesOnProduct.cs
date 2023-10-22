using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SomeChangesOnProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductPhot_Products_ProductId",
                table: "ProductPhot");

            migrationBuilder.RenameColumn(
                name: "Subtitle",
                table: "Sliders",
                newName: "Description");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProductPhot",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPhot_Products_ProductId",
                table: "ProductPhot",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductPhot_Products_ProductId",
                table: "ProductPhot");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Sliders",
                newName: "Subtitle");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProductPhot",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPhot_Products_ProductId",
                table: "ProductPhot",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
