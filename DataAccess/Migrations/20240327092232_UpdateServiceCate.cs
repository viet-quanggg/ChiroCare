using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateServiceCate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_ServiceCategories_ServiceCategoryCategoryId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_ServiceCategoryCategoryId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "ServiceCategoryCategoryId",
                table: "Services");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Services",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Services_CategoryId",
                table: "Services",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_ServiceCategories_CategoryId",
                table: "Services",
                column: "CategoryId",
                principalTable: "ServiceCategories",
                principalColumn: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_ServiceCategories_CategoryId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_CategoryId",
                table: "Services");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Services",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ServiceCategoryCategoryId",
                table: "Services",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Services_ServiceCategoryCategoryId",
                table: "Services",
                column: "ServiceCategoryCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_ServiceCategories_ServiceCategoryCategoryId",
                table: "Services",
                column: "ServiceCategoryCategoryId",
                principalTable: "ServiceCategories",
                principalColumn: "CategoryId");
        }
    }
}
