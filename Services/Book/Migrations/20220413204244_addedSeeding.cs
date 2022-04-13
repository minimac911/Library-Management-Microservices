using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Book.Migrations
{
    public partial class addedSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "books",
                columns: new[] { "Id", "Author", "ISBN", "Title" },
                values: new object[,]
                {
                    { 1L, "Author A", "0686aeec-aba5-4bf6-ab50-ce903146f2a1", "Book A" },
                    { 2L, "Author B", "d3b3e9b4-2ca1-45d6-838c-f6e4f077118e", "Book B" },
                    { 3L, "Author C", "170a34b7-37c8-486c-84dc-dd40378ad685", "Book C" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "books",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "books",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "books",
                keyColumn: "Id",
                keyValue: 3L);
        }
    }
}
