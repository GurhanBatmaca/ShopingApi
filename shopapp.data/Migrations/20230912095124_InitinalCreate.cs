using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace shopapp.data.Migrations
{
    /// <inheritdoc />
    public partial class InitinalCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "Url" },
                values: new object[,]
                {
                    { 1, "Kaşar", "kasar" },
                    { 2, "Eski Kaşar", "eski-kasar" },
                    { 3, "Yeni Kaşar", "yeni-kasar" },
                    { 4, "Süzme Bal", "suzme-bal" },
                    { 5, "Petek Bal", "petek-bal" },
                    { 6, "Kara Kovan Bal", "kara-kovan-bal" },
                    { 7, "Çiçek Bal", "cicek-bal" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "DateAdded", "Description", "ImageUrl", "IsAproved", "IsHome", "IsPopular", "Name", "Price", "Url" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 9, 12, 12, 51, 24, 112, DateTimeKind.Local).AddTicks(1700), "Yeni Kaşar", "1.jpg", true, true, true, "Yeni Kaşar", 250.0, "yeni-kasar" },
                    { 2, new DateTime(2023, 9, 12, 12, 51, 24, 112, DateTimeKind.Local).AddTicks(1722), "Eski Kaşar", "2.jpg", true, true, false, "Eski Kaşar", 280.0, "eski-kasar" },
                    { 3, new DateTime(2023, 9, 12, 12, 51, 24, 112, DateTimeKind.Local).AddTicks(1725), "Kara Kovan Balı", "3.jpg", true, true, true, "Kara Kovan Balı", 280.0, "kara-kovan-bali" },
                    { 4, new DateTime(2023, 9, 12, 12, 51, 24, 112, DateTimeKind.Local).AddTicks(1726), "Petek Çiçek Balı", "4.jpg", true, true, false, "Petek Çiçek Balı", 280.0, "petek-cicek-bali" },
                    { 5, new DateTime(2023, 9, 12, 12, 51, 24, 112, DateTimeKind.Local).AddTicks(1728), "Süzme Çiçek Balı", "5.jpg", true, true, true, "Süzme Çiçek Balı", 280.0, "suzme-cicek-bali" }
                });

            migrationBuilder.InsertData(
                table: "ProductCategory",
                columns: new[] { "CategoryId", "ProductId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 3, 1 },
                    { 1, 2 },
                    { 2, 2 },
                    { 5, 3 },
                    { 4, 4 },
                    { 4, 5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 5, 3 });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 4, 4 });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 4, 5 });

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

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
        }
    }
}
