using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DevitaP120.Data.Migrations
{
    public partial class ProductsTableCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModifiedAt = table.Column<DateTime>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Slug = table.Column<string>(maxLength: 120, nullable: true),
                    Price = table.Column<double>(nullable: false),
                    ProducingPrice = table.Column<double>(nullable: false),
                    DiscountedPrice = table.Column<double>(nullable: false),
                    DiscountPercent = table.Column<double>(nullable: false),
                    Desc = table.Column<string>(maxLength: 1500, nullable: true),
                    InfoText = table.Column<string>(maxLength: 500, nullable: true),
                    IsAvailable = table.Column<bool>(nullable: false),
                    Rate = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
