using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuotationServiceManagement.Application.Web.Migrations
{
    public partial class Add_Quoatation_DataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "quotation");

            migrationBuilder.CreateTable(
                name: "inquiryparty",
                schema: "quotation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Gcid = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    LinkInfo_LinkMan = table.Column<string>(type: "TEXT", nullable: false),
                    LinkInfo_Email = table.Column<string>(type: "TEXT", nullable: false),
                    LinkInfo_Phone = table.Column<string>(type: "TEXT", nullable: false),
                    LinkInfo_Fax = table.Column<string>(type: "TEXT", nullable: false),
                    BankInfo_BankAccount = table.Column<string>(type: "TEXT", nullable: false),
                    BankInfo_OpeningBank = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inquiryparty", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "quotation",
                schema: "quotation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Gcid = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
                    InquiryPartyId = table.Column<int>(type: "INTEGER", nullable: false),
                    QuotationDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    QuotationStatus = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    QuotationCount = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false, defaultValue: ""),
                    TotalData = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_quotation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "quotationitem",
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
                    QuotationId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_quotationitem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_quotationitem_quotation_QuotationId",
                        column: x => x.QuotationId,
                        principalSchema: "quotation",
                        principalTable: "quotation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_quotationitem_QuotationId",
                schema: "quotation",
                table: "quotationitem",
                column: "QuotationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "inquiryparty",
                schema: "quotation");

            migrationBuilder.DropTable(
                name: "quotationitem",
                schema: "quotation");

            migrationBuilder.DropTable(
                name: "quotation",
                schema: "quotation");
        }
    }
}
