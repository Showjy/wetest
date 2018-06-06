using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace wetest.Migrations
{
    public partial class migrations_308 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "progress",
                table: "Orders",
                newName: "Progress");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Items",
                newName: "Status");

            migrationBuilder.AddColumn<string>(
                name: "Data",
                table: "Items",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Data",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "Progress",
                table: "Orders",
                newName: "progress");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Items",
                newName: "status");
        }
    }
}
