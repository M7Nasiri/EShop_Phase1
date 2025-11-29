using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class seedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsInSlider",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImagePath", "IsDelete", "IsInSlider", "Rate", "Stock", "Title", "UnitCost" },
                values: new object[,]
                {
                    { 1, null, "لباس مناسب ورزش روزانه", "/Products/1.jpg", false, true, 4, 15, "لباس ورزشی", 350000L },
                    { 2, null, "کفش حرفه‌ای راحتی", "/Products/2.jpg", false, true, 5, 8, "کفش اسپرت", 780000L },
                    { 3, null, "ساعت هوشمند با امکانات کامل", "/Products/3.jpg", false, true, 4, 5, "ساعت هوشمند", 1250000L },
                    { 4, null, "کیفیت بالا و باتری مناسب", "/Products/4.jpg", false, false, 3, 25, "هندزفری بیسیم", 420000L },
                    { 5, null, "مناسب سفر و دانشگاه", "/Products/5.jpg", false, false, 4, 12, "کوله پشتی", 260000L },
                    { 6, null, "کیبورد RGB حرفه‌ای", "/Products/6.jpg", false, false, 5, 7, "کیبورد گیمینگ", 690000L },
                    { 7, null, "ماوس با حساسیت بالا", "/Products/7.jpg", false, false, 4, 18, "ماوس گیمینگ", 350000L },
                    { 8, null, "مانیتور Full HD", "/Products/8.jpg", false, false, 5, 4, "مانیتور 24 اینچ", 3200000L },
                    { 9, null, "قابلیت شارژ سریع", "/Products/9.jpg", false, false, 4, 20, "پاوربانک", 550000L },
                    { 10, null, "کیفیت صدای بالا", "/Products/10.jpg", false, false, 5, 6, "هدفون حرفه‌ای", 980000L }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DropColumn(
                name: "IsInSlider",
                table: "Products");
        }
    }
}
