using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarTrader.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "df1c3a0f-1234-4cde-bb55-d5f15a6aabcd", 0, "55555555-5555-5555-5555-555555555555", "user@cartrader.com", true, false, null, "USER@CARTRADER.COM", "USER@CARTRADER.COM", "AQAAAAEAAYagAAAAEAAAACAC8ObmFmbkWID8jZLZ2Lf/Is75PrJm1Yv4WsTKNzrsZa2Rt9alCLyjL7+sfRkQSpw=", null, false, "44444444-4444-4444-4444-444444444444", false, "user@cartrader.com" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Sedan" },
                    { 2, "SUV" },
                    { 3, "Truck" },
                    { 4, "Coupe" },
                    { 5, "Convertible" }
                });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "CategoryId", "Condition", "Description", "Doors", "EngineSize", "FuelType", "ImageUrl", "IsDeleted", "Make", "Mileage", "Model", "Price", "SellerId", "TransmissionType", "Year" },
                values: new object[,]
                {
                    { 1, 1, 3, "Reliable midsize sedan with good fuel economy.", 2, 2500, 0, "https://example.com/images/toyota-camry-2020.jpg", false, "Toyota", 25000, "Camry", 24000.0, "df1c3a0f-1234-4cde-bb55-d5f15a6aabcd", 1, 2020 },
                    { 2, 1, 2, "Compact car with a sporty feel and low running costs.", 2, 2000, 0, "https://example.com/images/honda-civic-2019.jpg", false, "Honda", 30000, "Civic", 20000.0, "df1c3a0f-1234-4cde-bb55-d5f15a6aabcd", 0, 2019 },
                    { 3, 3, 1, "Powerful pickup truck suitable for work and towing.", 2, 3500, 4, "https://example.com/images/ford-f150-2021.jpg", false, "Ford", 15000, "F-150", 35000.0, "df1c3a0f-1234-4cde-bb55-d5f15a6aabcd", 1, 2021 },
                    { 4, 2, 3, "Full-size SUV with spacious interior and strong towing capacity.", 2, 5300, 0, "https://example.com/images/chevrolet-tahoe-2020.jpg", false, "Chevrolet", 20000, "Tahoe", 50000.0, "df1c3a0f-1234-4cde-bb55-d5f15a6aabcd", 1, 2020 },
                    { 5, 4, 1, "High-performance coupe with track-capable handling.", 0, 3000, 0, "https://example.com/images/bmw-m4-2021.jpg", false, "BMW", 10000, "M4", 70000.0, "df1c3a0f-1234-4cde-bb55-d5f15a6aabcd", 2, 2021 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "df1c3a0f-1234-4cde-bb55-d5f15a6aabcd");

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
        }
    }
}
