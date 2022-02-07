using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EMP.Data.Migrations
{
    public partial class addShipment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    Password = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    City = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Age = table.Column<int>(type: "INTEGER", maxLength: 256, nullable: false),
                    DateOfBrith = table.Column<DateTime>(type: "datetime", nullable: false),
                    Gender = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ImageURL = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    LinkedinURL = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeGroup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    AdminId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    IconImg = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Description = table.Column<string>(type: "TEXT", maxLength: 550, nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shipment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    LoginId = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    Broker = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    Platform = table.Column<string>(type: "TEXT", nullable: true),
                    Expiry = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsLive = table.Column<bool>(type: "INTEGER", nullable: false),
                    Password = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    Password2 = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    APIKey = table.Column<string>(type: "TEXT", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeTechnology",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    EmpId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeTechnology", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeTechnology_Employee_EmpId",
                        column: x => x.EmpId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmpGroupList",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    EmpId = table.Column<Guid>(type: "TEXT", nullable: false),
                    GroupId = table.Column<Guid>(type: "TEXT", nullable: false),
                    InviteType = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpGroupList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmpGroupList_EmployeeGroup_GroupId",
                        column: x => x.GroupId,
                        principalTable: "EmployeeGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmpGroupList_GroupId",
                table: "EmpGroupList",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTechnology_EmpId",
                table: "EmployeeTechnology",
                column: "EmpId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmpGroupList");

            migrationBuilder.DropTable(
                name: "EmployeeTechnology");

            migrationBuilder.DropTable(
                name: "Shipment");

            migrationBuilder.DropTable(
                name: "EmployeeGroup");

            migrationBuilder.DropTable(
                name: "Employee");
        }
    }
}
