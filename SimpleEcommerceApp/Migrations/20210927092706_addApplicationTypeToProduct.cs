using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleEcommerceApp.Migrations
{
    public partial class addApplicationTypeToProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                table: "Tbl_Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Product_ApplicationId",
                table: "Tbl_Product",
                column: "ApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tbl_Product_Tbl_ApplicationType_ApplicationId",
                table: "Tbl_Product",
                column: "ApplicationId",
                principalTable: "Tbl_ApplicationType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tbl_Product_Tbl_ApplicationType_ApplicationId",
                table: "Tbl_Product");

            migrationBuilder.DropIndex(
                name: "IX_Tbl_Product_ApplicationId",
                table: "Tbl_Product");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "Tbl_Product");
        }
    }
}
