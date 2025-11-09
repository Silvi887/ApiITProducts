using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiITProducts.Migrations
{
    /// <inheritdoc />
    public partial class Mgrationnew3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Town",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "picurl",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CatMaxPricesDto",
                columns: table => new
                {
                    catname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sumpriceperiod = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CatMaxPricesDto");

            migrationBuilder.DropColumn(
                name: "picurl",
                table: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "Town",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
