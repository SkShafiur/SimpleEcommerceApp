using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleEcommerceApp.Migrations
{
    public partial class addApplicationTypeToProduct_Rename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tbl_Product_Tbl_ApplicationType_ApplicationId",
                table: "Tbl_Product");

            migrationBuilder.RenameColumn(
                name: "ApplicationId",
                table: "Tbl_Product",
                newName: "ApplicationTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Tbl_Product_ApplicationId",
                table: "Tbl_Product",
                newName: "IX_Tbl_Product_ApplicationTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tbl_Product_Tbl_ApplicationType_ApplicationTypeId",
                table: "Tbl_Product",
                column: "ApplicationTypeId",
                principalTable: "Tbl_ApplicationType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tbl_Product_Tbl_ApplicationType_ApplicationTypeId",
                table: "Tbl_Product");

            migrationBuilder.RenameColumn(
                name: "ApplicationTypeId",
                table: "Tbl_Product",
                newName: "ApplicationId");

            migrationBuilder.RenameIndex(
                name: "IX_Tbl_Product_ApplicationTypeId",
                table: "Tbl_Product",
                newName: "IX_Tbl_Product_ApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tbl_Product_Tbl_ApplicationType_ApplicationId",
                table: "Tbl_Product",
                column: "ApplicationId",
                principalTable: "Tbl_ApplicationType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
