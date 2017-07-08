using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Server.DataAccess.Migrations
{
    public partial class AddBaseModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CashflowCategory",
                columns: table => new
                {
                    CashflowCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashflowCategory", x => x.CashflowCategoryId);
                    table.ForeignKey(
                        name: "FK_CashflowCategory_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Currency",
                columns: table => new
                {
                    CurrencyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.CurrencyId);
                });

            migrationBuilder.CreateTable(
                name: "CashAccount",
                columns: table => new
                {
                    CashAccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountId = table.Column<int>(type: "int", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    IsJointCashAccount = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashAccount", x => x.CashAccountId);
                    table.ForeignKey(
                        name: "FK_CashAccount_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CashAccount_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currency",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CashAccount_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cashflow",
                columns: table => new
                {
                    CashflowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    CashAccountId = table.Column<int>(type: "int", nullable: true),
                    CashflowCategoryId = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cashflow", x => x.CashflowId);
                    table.ForeignKey(
                        name: "FK_Cashflow_CashAccount_CashAccountId",
                        column: x => x.CashAccountId,
                        principalTable: "CashAccount",
                        principalColumn: "CashAccountId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cashflow_CashflowCategory_CashflowCategoryId",
                        column: x => x.CashflowCategoryId,
                        principalTable: "CashflowCategory",
                        principalColumn: "CashflowCategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cashflow_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CashAccount_AccountId",
                table: "CashAccount",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_CashAccount_CurrencyId",
                table: "CashAccount",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_CashAccount_UserId",
                table: "CashAccount",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Cashflow_CashAccountId",
                table: "Cashflow",
                column: "CashAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Cashflow_CashflowCategoryId",
                table: "Cashflow",
                column: "CashflowCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Cashflow_UserId",
                table: "Cashflow",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CashflowCategory_AccountId",
                table: "CashflowCategory",
                column: "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cashflow");

            migrationBuilder.DropTable(
                name: "CashAccount");

            migrationBuilder.DropTable(
                name: "CashflowCategory");

            migrationBuilder.DropTable(
                name: "Currency");
        }
    }
}
