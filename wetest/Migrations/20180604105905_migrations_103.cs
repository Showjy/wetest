using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace wetest.Migrations
{
    public partial class migrations_103 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AppealDate = table.Column<string>(nullable: true),
                    ClosedDate = table.Column<string>(nullable: true),
                    Information = table.Column<string>(nullable: true),
                    OrderId = table.Column<string>(nullable: true),
                    Price = table.Column<int>(nullable: false),
                    ProviderId = table.Column<string>(nullable: true),
                    ServicerId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payment");
        }
    }
}
