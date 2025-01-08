using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class seedingfordiffandre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0db49dba-19b2-429d-838b-a1f2b11035df"), "Hard" },
                    { new Guid("b64726e2-8dbd-4946-98fb-5f35a8457642"), "Easy" },
                    { new Guid("d73ff9ab-c5b5-448d-bbff-4cc4aa87d371"), "Medium" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("10334b53-ba3c-4cba-8346-9caaee60fd20"), "W", "Waikato", null },
                    { new Guid("6b03355f-0734-4ea9-a690-919ac931f934"), "A", "Auckland", null },
                    { new Guid("a03fd20e-8c43-4bae-be98-0c13b9dc4804"), "N", "Northland", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("0db49dba-19b2-429d-838b-a1f2b11035df"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("b64726e2-8dbd-4946-98fb-5f35a8457642"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("d73ff9ab-c5b5-448d-bbff-4cc4aa87d371"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("10334b53-ba3c-4cba-8346-9caaee60fd20"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("6b03355f-0734-4ea9-a690-919ac931f934"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("a03fd20e-8c43-4bae-be98-0c13b9dc4804"));
        }
    }
}
