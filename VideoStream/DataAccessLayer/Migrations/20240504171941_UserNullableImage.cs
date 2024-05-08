using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class UserNullableImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "VideoCategories",
                keyColumn: "Id",
                keyValue: new Guid("2cdf792a-e869-410f-8dcf-7e26cd1fb7e8"));

            migrationBuilder.DeleteData(
                table: "VideoCategories",
                keyColumn: "Id",
                keyValue: new Guid("2d8f4f65-6413-43ae-bd41-52861793a2a8"));

            migrationBuilder.DeleteData(
                table: "VideoCategories",
                keyColumn: "Id",
                keyValue: new Guid("abd85314-1909-4fa1-b79b-b91596896522"));

            migrationBuilder.AlterColumn<string>(
                name: "ImagePath",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "VideoCategories",
                columns: new[] { "Id", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("1ca2fe1c-41d3-4f03-99c1-96274bc2b0dd"), new DateTime(2010, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gaming" },
                    { new Guid("2ce21405-72e5-47e0-98be-7048e6a75f0e"), new DateTime(2010, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Action" },
                    { new Guid("3a326ac1-01d1-42d1-b8a5-bdb316d59574"), new DateTime(2010, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Vloging" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "VideoCategories",
                keyColumn: "Id",
                keyValue: new Guid("1ca2fe1c-41d3-4f03-99c1-96274bc2b0dd"));

            migrationBuilder.DeleteData(
                table: "VideoCategories",
                keyColumn: "Id",
                keyValue: new Guid("2ce21405-72e5-47e0-98be-7048e6a75f0e"));

            migrationBuilder.DeleteData(
                table: "VideoCategories",
                keyColumn: "Id",
                keyValue: new Guid("3a326ac1-01d1-42d1-b8a5-bdb316d59574"));

            migrationBuilder.AlterColumn<string>(
                name: "ImagePath",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "VideoCategories",
                columns: new[] { "Id", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("2cdf792a-e869-410f-8dcf-7e26cd1fb7e8"), new DateTime(2010, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Vloging" },
                    { new Guid("2d8f4f65-6413-43ae-bd41-52861793a2a8"), new DateTime(2010, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gaming" },
                    { new Guid("abd85314-1909-4fa1-b79b-b91596896522"), new DateTime(2010, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Action" }
                });
        }
    }
}
