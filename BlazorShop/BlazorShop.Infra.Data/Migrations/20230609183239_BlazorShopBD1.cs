using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorShop.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class BlazorShopBD1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    IconCSS = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "DATETIME", nullable: true, defaultValueSql: "GETDATE()"),
                    InsertDate = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(250)", maxLength: 250, nullable: false),
                    Password = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: false),
                    UserType = table.Column<int>(type: "int", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "DATETIME", nullable: true, defaultValueSql: "GETDATE()"),
                    InsertDate = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(80)", maxLength: 80, nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    ImageURL = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false, defaultValue: "https://firebasestorage.googleapis.com/v0/b/podtv-5700.appspot.com/o/upload-image-icon.jpg?alt=media&token=129b5561-51a8-4c2f-81f0-264dcd2397d1"),
                    Price = table.Column<decimal>(type: "NUMERIC(8,2)", nullable: false),
                    Quantity = table.Column<int>(type: "INT", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "DATETIME", nullable: true, defaultValueSql: "GETDATE()"),
                    IdCategory = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_IdCategory",
                        column: x => x.IdCategory,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "DATETIME", nullable: true, defaultValueSql: "GETDATE()"),
                    IdUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemsCart",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "INT", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "DATETIME", nullable: true, defaultValueSql: "GETDATE()"),
                    IdCart = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdProduct = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsCart", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemsCart_Carts_IdCart",
                        column: x => x.IdCart,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemsCart_Products_IdProduct",
                        column: x => x.IdProduct,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Carts_IdUser",
                table: "Carts",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsCart_IdCart",
                table: "ItemsCart",
                column: "IdCart");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsCart_IdProduct",
                table: "ItemsCart",
                column: "IdProduct");

            migrationBuilder.CreateIndex(
                name: "IX_Products_IdCategory",
                table: "Products",
                column: "IdCategory");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemsCart");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
