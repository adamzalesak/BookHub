using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class RefactorCart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookCart");

            migrationBuilder.AddColumn<int>(
                name: "CartId",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BookId = table.Column<int>(type: "INTEGER", nullable: false),
                    CartId = table.Column<int>(type: "INTEGER", nullable: false),
                    Count = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CartItems_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "CartId", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { 6, "8ef1d3ae-95dc-46b3-8437-bc1f1e770d16", "AQAAAAIAAYagAAAAEDlZk2Darl5lHuWnbDeuT78WR7ztP2FXU+UJfUbzwB77DasVXtpOTPLZpqxYkN4wCw==", "3672efb2-302e-4f34-90f6-d8151dc32c35" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "CartId", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { 7, "a0ab4d16-75fb-49c2-8de6-fe657b9c8bcf", "AQAAAAIAAYagAAAAEKkWJNL7OcpnAE7r+iP4JJrXEwKUKXJLK+b8Yd7s33ZPt/4Tpk5y2fRFzw9VDbzcPw==", "5d4ac658-3165-4bce-8804-a36ad286419e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "CartId", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { 8, "8d8337f1-e80d-423a-aba6-ebfe3277a9f3", "AQAAAAIAAYagAAAAEPy754XV/w+uDOkiloIepxoKyetWdGMhT0bTvkiYk648rTKcI57whzxqjF/N37O67A==", "954ec450-dfe5-4f11-b3f5-86882f44e02c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4",
                columns: new[] { "CartId", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { 9, "97bfdde7-de5c-4c32-8ee7-aa0ae7187af2", "AQAAAAIAAYagAAAAEOQmieBnAgOoFw/wE2R4Ev04e1HPGG9u3Q52jBkAIs6hHefNi2p879VGm+qbM0S20A==", "9d5a89e8-e831-448e-9e02-03b8adb36e64" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5",
                columns: new[] { "CartId", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { 10, "17d143be-7ac6-466f-9031-1489603fa7ed", "AQAAAAIAAYagAAAAEDa7A+rsfpQfEXitKX7ceP6znJapNuVXVE7Rqekzm5z7FiqRSe69yY754su7lEwx3w==", "4be8fe71-8716-491a-a9ee-8b35805291cd" });

            migrationBuilder.InsertData(
                table: "CartItems",
                columns: new[] { "Id", "BookId", "CartId", "Count" },
                values: new object[,]
                {
                    { 1, 2, 1, 1 },
                    { 2, 3, 2, 1 },
                    { 3, 1, 2, 1 },
                    { 4, 1, 3, 1 },
                    { 5, 3, 3, 1 },
                    { 6, 1, 4, 1 },
                    { 7, 3, 5, 1 }
                });

            migrationBuilder.InsertData(
                table: "Carts",
                column: "Id",
                values: new object[]
                {
                    6,
                    7,
                    8,
                    9,
                    10
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CartId",
                table: "AspNetUsers",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_BookId",
                table: "CartItems",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems",
                column: "CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Carts_CartId",
                table: "AspNetUsers",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Carts_CartId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CartId",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DropColumn(
                name: "CartId",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "BookCart",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "INTEGER", nullable: false),
                    CartId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookCart", x => new { x.BookId, x.CartId });
                    table.ForeignKey(
                        name: "FK_BookCart_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookCart_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "22495155-e7ac-46f4-8397-6f0407136e34", "AQAAAAIAAYagAAAAEPqU0xt2MusPLbL6q636f6QvSRub0OVJSeCfpvb4ICj0FLH4DkS+v0+MBjv0DJYgLg==", "1988014c-e6e3-4296-8a44-18fe7279b55a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c4df050e-cea5-4978-8212-cee60681b266", "AQAAAAIAAYagAAAAEKc6St+TdRaVtq+8xpg9p/NPej1gjx3xUySKX15U52qRndplu2H6/Ac3NU0aK32g2w==", "8f6eaa75-50e3-4bfa-903b-8b98b87d2eb0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "296ddadb-1ba0-4edd-8bb1-521a80c45e8c", "AQAAAAIAAYagAAAAEKBC2STnKASQKO3RwqWdItsKm+xTQkEZX5PQgx8U46d4BCHnfwg2R+2RG4YDZrxGbw==", "acf15264-778e-404b-8005-ecd78097630a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d464b50e-a6cb-443f-bbe0-7f36e28f8d93", "AQAAAAIAAYagAAAAEGnSXAPyXnEcShDE9EA9lHgbWFP8wOHHYjEe279D2Aw4TgJTlDnIy3mcvDv3qQcwWg==", "1ec9825e-f6a3-4624-80a3-476c28ef0470" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f9bb053f-8351-42b2-95ec-ef48d02c672b", "AQAAAAIAAYagAAAAEIHaqn1IOC9wXV0yexS4pD4uhAZ3tTNcpaRgwLjEg7BOxCM+Sitchbb+qycyrTDz9w==", "cf192816-d171-4f9e-8c75-b6bb88abe3bb" });

            migrationBuilder.InsertData(
                table: "BookCart",
                columns: new[] { "BookId", "CartId" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 1, 3 },
                    { 1, 4 },
                    { 2, 1 },
                    { 3, 2 },
                    { 3, 3 },
                    { 3, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookCart_CartId",
                table: "BookCart",
                column: "CartId");
        }
    }
}
