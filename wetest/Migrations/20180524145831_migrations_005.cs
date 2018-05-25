using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace wetest.Migrations
{
    public partial class migrations_005 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartData",
                table: "Orders",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "EndData",
                table: "Orders",
                newName: "ServicerId");

            migrationBuilder.AlterColumn<long>(
                name: "Price",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EndDate",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProviderId",
                table: "Orders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ProviderId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Orders",
                newName: "StartData");

            migrationBuilder.RenameColumn(
                name: "ServicerId",
                table: "Orders",
                newName: "EndData");

            migrationBuilder.AlterColumn<string>(
                name: "Price",
                table: "Orders",
                nullable: true,
                oldClrType: typeof(long));
        }
    }
}
