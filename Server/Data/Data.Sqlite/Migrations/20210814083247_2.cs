using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    ID = table.Column<uint>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    ID = table.Column<uint>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MentorID = table.Column<uint>(type: "INTEGER", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Employees_Employees_MentorID",
                        column: x => x.MentorID,
                        principalTable: "Employees",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Goods",
                columns: table => new
                {
                    ID = table.Column<uint>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goods", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "OutcomingOrders",
                columns: table => new
                {
                    ID = table.Column<uint>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Number = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutcomingOrders", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    ID = table.Column<uint>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeGood",
                columns: table => new
                {
                    ResponsibleEmployeesID = table.Column<uint>(type: "INTEGER", nullable: false),
                    ResponsibleForGoodsID = table.Column<uint>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeGood", x => new { x.ResponsibleEmployeesID, x.ResponsibleForGoodsID });
                    table.ForeignKey(
                        name: "FK_EmployeeGood_Employees_ResponsibleEmployeesID",
                        column: x => x.ResponsibleEmployeesID,
                        principalTable: "Employees",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeGood_Goods_ResponsibleForGoodsID",
                        column: x => x.ResponsibleForGoodsID,
                        principalTable: "Goods",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IncomingOrders",
                columns: table => new
                {
                    ID = table.Column<uint>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OutcomingOrderID = table.Column<uint>(type: "INTEGER", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Number = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomingOrders", x => x.ID);
                    table.ForeignKey(
                        name: "FK_IncomingOrders_OutcomingOrders_OutcomingOrderID",
                        column: x => x.OutcomingOrderID,
                        principalTable: "OutcomingOrders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IncomingOrderLines",
                columns: table => new
                {
                    ID = table.Column<uint>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IncomingOrderID = table.Column<uint>(type: "INTEGER", nullable: true),
                    GoodID = table.Column<uint>(type: "INTEGER", nullable: true),
                    Quantity = table.Column<uint>(type: "INTEGER", nullable: false),
                    Price = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomingOrderLines", x => x.ID);
                    table.ForeignKey(
                        name: "FK_IncomingOrderLines_Goods_GoodID",
                        column: x => x.GoodID,
                        principalTable: "Goods",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IncomingOrderLines_IncomingOrders_IncomingOrderID",
                        column: x => x.IncomingOrderID,
                        principalTable: "IncomingOrders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeGood_ResponsibleForGoodsID",
                table: "EmployeeGood",
                column: "ResponsibleForGoodsID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_MentorID",
                table: "Employees",
                column: "MentorID");

            migrationBuilder.CreateIndex(
                name: "IX_IncomingOrderLines_GoodID",
                table: "IncomingOrderLines",
                column: "GoodID");

            migrationBuilder.CreateIndex(
                name: "IX_IncomingOrderLines_IncomingOrderID",
                table: "IncomingOrderLines",
                column: "IncomingOrderID");

            migrationBuilder.CreateIndex(
                name: "IX_IncomingOrders_OutcomingOrderID",
                table: "IncomingOrders",
                column: "OutcomingOrderID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "EmployeeGood");

            migrationBuilder.DropTable(
                name: "IncomingOrderLines");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Goods");

            migrationBuilder.DropTable(
                name: "IncomingOrders");

            migrationBuilder.DropTable(
                name: "OutcomingOrders");
        }
    }
}
