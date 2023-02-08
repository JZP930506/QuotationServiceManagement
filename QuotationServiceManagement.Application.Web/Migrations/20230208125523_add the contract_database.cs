using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuotationServiceManagement.Application.Web.Migrations
{
    public partial class addthecontract_database : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "contract",
                schema: "quotation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Gcid = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
                    ContractNo = table.Column<string>(type: "TEXT", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeliveryTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    QuotationId = table.Column<int>(type: "INTEGER", nullable: false),
                    FirstParty_Title = table.Column<string>(type: "TEXT", nullable: false),
                    FirstParty_Address = table.Column<string>(type: "TEXT", nullable: false),
                    FirstParty_LinkMan = table.Column<string>(type: "TEXT", nullable: false),
                    FirstParty_Email = table.Column<string>(type: "TEXT", nullable: false),
                    FirstParty_Phone = table.Column<string>(type: "TEXT", nullable: false),
                    FirstParty_Fax = table.Column<string>(type: "TEXT", nullable: false),
                    FirstParty_BankAccount = table.Column<string>(type: "TEXT", nullable: false),
                    FirstParty_OpeningBank = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false, defaultValue: ""),
                    UnitPrice = table.Column<double>(type: "REAL", nullable: false, defaultValue: 0.0),
                    TotalData = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contract", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "contractitem",
                schema: "quotation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Specification = table.Column<string>(type: "TEXT", nullable: false),
                    TechnologicalStandard = table.Column<string>(type: "TEXT", nullable: false),
                    UnitPrice = table.Column<double>(type: "REAL", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Remark = table.Column<string>(type: "TEXT", nullable: false),
                    ContractId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contractitem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_contractitem_contract_ContractId",
                        column: x => x.ContractId,
                        principalSchema: "quotation",
                        principalTable: "contract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_contractitem_ContractId",
                schema: "quotation",
                table: "contractitem",
                column: "ContractId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contractitem",
                schema: "quotation");

            migrationBuilder.DropTable(
                name: "contract",
                schema: "quotation");
        }
    }
}
