using Microsoft.EntityFrameworkCore.Migrations;

namespace eshop.Migrations
{
    public partial class MSSQL_1_1_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ProductCategoryID",
                table: "Products",
                nullable: false,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ProductCategoryID",
                table: "Products",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
