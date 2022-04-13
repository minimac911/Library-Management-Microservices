using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Member.Migrations
{
    public partial class addedSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "members",
                columns: new[] { "Id", "Email", "Name" },
                values: new object[,]
                {
                    { 1L, "test@gmail.com", "Test" },
                    { 2L, "john@gmail.com", "John" },
                    { 3L, "meg@gmail.com", "Meg" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "members",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "members",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "members",
                keyColumn: "Id",
                keyValue: 3L);
        }
    }
}
