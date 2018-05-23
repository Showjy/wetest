using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace wetest.Migrations
{
    public partial class migrations_002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserToItem",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ItemId = table.Column<string>(nullable: true),
                    SellerId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToItem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserToOrder",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    BuyerId = table.Column<string>(nullable: true),
                    OrderId = table.Column<string>(nullable: true),
                    SellerId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToOrder", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserToItem");

            migrationBuilder.DropTable(
                name: "UserToOrder");
        }
    }
}
