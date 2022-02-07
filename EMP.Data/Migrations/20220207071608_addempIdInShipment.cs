using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EMP.Data.Migrations
{
    public partial class addempIdInShipment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Broker",
                table: "Shipment",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 256);

            migrationBuilder.AddColumn<Guid>(
                name: "EmpId",
                table: "Shipment",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmpId",
                table: "Shipment");

            migrationBuilder.AlterColumn<string>(
                name: "Broker",
                table: "Shipment",
                type: "TEXT",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }
    }
}
