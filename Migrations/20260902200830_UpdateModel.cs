using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MSilvaPizza.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Pizzas",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Pizzas",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DoughType",
                table: "Pizzas",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Pizzas",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ingredients",
                table: "Pizzas",
                type: "TEXT",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Pizzas",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Width",
                table: "Pizzas",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Pizzas");

            migrationBuilder.DropColumn(
                name: "DoughType",
                table: "Pizzas");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Pizzas");

            migrationBuilder.DropColumn(
                name: "Ingredients",
                table: "Pizzas");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Pizzas");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "Pizzas");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Pizzas",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }
    }
}
