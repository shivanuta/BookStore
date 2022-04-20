using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore_Models.Migrations
{
    public partial class Stock_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stock",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    TotalStock = table.Column<int>(type: "int", nullable: false),
                    AvailableStock = table.Column<int>(type: "int", nullable: true),
                    AmountPerBook = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountPercentage = table.Column<int>(type: "int", nullable: false),
                    DeliveryCharges = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stock_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stock_BookId",
                table: "Stock",
                column: "BookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stock");
        }
    }
}
