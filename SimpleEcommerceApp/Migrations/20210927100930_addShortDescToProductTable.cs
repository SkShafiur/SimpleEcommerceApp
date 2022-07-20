using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleEcommerceApp.Migrations
{
    public partial class addShortDescToProductTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShortDesc",
                table: "Tbl_Product",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShortDesc",
                table: "Tbl_Product");
        }
    }
}
