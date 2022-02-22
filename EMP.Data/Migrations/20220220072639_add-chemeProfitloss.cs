using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EMP.Data.Migrations
{
    public partial class addchemeProfitloss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SchemeProfitLoss",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    GroupId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    KeyWord = table.Column<string>(type: "TEXT", nullable: true),
                    ProfitLoss = table.Column<double>(type: "REAL", nullable: false),
                    Expense = table.Column<double>(type: "REAL", nullable: false),
                    Comments = table.Column<string>(type: "TEXT", nullable: true),
                    IsNoTradeDay = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsHoliday = table.Column<bool>(type: "INTEGER", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchemeProfitLoss", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SchemeProfitLoss");
        }
    }
}
