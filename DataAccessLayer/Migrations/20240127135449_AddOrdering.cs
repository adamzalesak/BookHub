using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddOrdering : Migration
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
                values: new object[] { 5, "3061122c-1377-47fa-bef5-0ed284a1f5be", "AQAAAAIAAYagAAAAEGQ2pIdgZVvDtsxE80fQhjxgL+gONs7iq7FxqSNRuDCb6SE9dVms7oOWlzYrpApwow==", "ff0a8d8b-5078-4728-83c1-3457147a0804" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "CartId", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { 6, "5731d883-fdaf-42d2-9560-57109d939495", "AQAAAAIAAYagAAAAENHFX6NcZs2VJQX782KEyrV+DFMUQHd0/nHyTTH+96A/Aop+pZJW/BYKbNiyjskzZg==", "eeedfa04-f296-477e-b997-89488e94ef41" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "CartId", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { 7, "1dc914a3-2c04-4424-8643-5201963293ce", "AQAAAAIAAYagAAAAENc6hTx1vuykDf1t7R4Ofhi0e64Lvjo705JihORkZYv0Hra5TuqAhBNOMFNRy3PBXg==", "5dcefa59-c6ef-462c-a434-395f2e495838" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4",
                columns: new[] { "CartId", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { 8, "7e2838ce-73fc-4bd8-b042-588418d785d7", "AQAAAAIAAYagAAAAENrSoRWAP83IH77h9yaT8BDYOxZZ4TyGyufg0BQBsaO8COD3dbXdWaQPRaRq5NKIsg==", "2fa01d7c-2232-45f0-b3e8-238201c8836d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5",
                columns: new[] { "CartId", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { 9, "81b74af1-4f12-4101-99cc-834b5be2045b", "AQAAAAIAAYagAAAAEMqzAE4soxFyQJ71q0JN2uTpCl9ylFqUQ+Y7dhv/kbbkiJbFNZuoATkXDT0RE/4qsQ==", "aa89f46d-47dd-44c1-a3f7-bad4a0fda0de" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "Count",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "Count",
                value: 10);

            migrationBuilder.InsertData(
                table: "CartItems",
                columns: new[] { "Id", "BookId", "CartId", "Count" },
                values: new object[,]
                {
                    { 1, 1, 1, 1 },
                    { 2, 2, 2, 1 },
                    { 3, 3, 3, 2 },
                    { 4, 1, 3, 1 },
                    { 5, 1, 4, 1 }
                });

            migrationBuilder.InsertData(
                table: "Carts",
                column: "Id",
                values: new object[]
                {
                    6,
                    7,
                    8,
                    9
                });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "UserId" },
                values: new object[] { "pavel.novak@seznam.cz", "1" });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Email", "TotalPrice", "UserId" },
                values: new object[] { "karolina.svobodova@email.cz", 9.99m, "2" });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 3,
                column: "TotalPrice",
                value: 29.97m);

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

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "Count",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "Count",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "UserId" },
                values: new object[] { "poppar12@gmail.com", null });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Email", "TotalPrice", "UserId" },
                values: new object[] { "emmisek@zoznam.sk", 34.97m, null });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 3,
                column: "TotalPrice",
                value: 17.98m);

            migrationBuilder.CreateIndex(
                name: "IX_BookCart_CartId",
                table: "BookCart",
                column: "CartId");
        }
    }
}
