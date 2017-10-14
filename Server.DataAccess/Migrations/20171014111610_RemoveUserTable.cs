using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Server.DataAccess.Migrations
{
    public partial class RemoveUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashAccount_Accounts_AccountId",
                table: "CashAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_CashAccount_Currency_CurrencyId",
                table: "CashAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_CashAccount_Users_UserId",
                table: "CashAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_Cashflow_CashAccount_CashAccountId",
                table: "Cashflow");

            migrationBuilder.DropForeignKey(
                name: "FK_Cashflow_CashflowCategory_CashflowCategoryId",
                table: "Cashflow");

            migrationBuilder.DropForeignKey(
                name: "FK_Cashflow_Users_UserId",
                table: "Cashflow");

            migrationBuilder.DropForeignKey(
                name: "FK_CashflowCategory_Accounts_AccountId",
                table: "CashflowCategory");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Currency",
                table: "Currency");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CashflowCategory",
                table: "CashflowCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cashflow",
                table: "Cashflow");

            migrationBuilder.DropIndex(
                name: "IX_Cashflow_UserId",
                table: "Cashflow");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CashAccount",
                table: "CashAccount");

            migrationBuilder.DropIndex(
                name: "IX_CashAccount_UserId",
                table: "CashAccount");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Cashflow");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CashAccount");

            migrationBuilder.RenameTable(
                name: "Currency",
                newName: "Currencies");

            migrationBuilder.RenameTable(
                name: "CashflowCategory",
                newName: "CashflowCategories");

            migrationBuilder.RenameTable(
                name: "Cashflow",
                newName: "Cashflows");

            migrationBuilder.RenameTable(
                name: "CashAccount",
                newName: "CashAccounts");

            migrationBuilder.RenameIndex(
                name: "IX_CashflowCategory_AccountId",
                table: "CashflowCategories",
                newName: "IX_CashflowCategories_AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Cashflow_CashflowCategoryId",
                table: "Cashflows",
                newName: "IX_Cashflows_CashflowCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Cashflow_CashAccountId",
                table: "Cashflows",
                newName: "IX_Cashflows_CashAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_CashAccount_CurrencyId",
                table: "CashAccounts",
                newName: "IX_CashAccounts_CurrencyId");

            migrationBuilder.RenameIndex(
                name: "IX_CashAccount_AccountId",
                table: "CashAccounts",
                newName: "IX_CashAccounts_AccountId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Currencies",
                table: "Currencies",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CashflowCategories",
                table: "CashflowCategories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cashflows",
                table: "Cashflows",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CashAccounts",
                table: "CashAccounts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CashAccounts_Accounts_AccountId",
                table: "CashAccounts",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CashAccounts_Currencies_CurrencyId",
                table: "CashAccounts",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CashflowCategories_Accounts_AccountId",
                table: "CashflowCategories",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cashflows_CashAccounts_CashAccountId",
                table: "Cashflows",
                column: "CashAccountId",
                principalTable: "CashAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cashflows_CashflowCategories_CashflowCategoryId",
                table: "Cashflows",
                column: "CashflowCategoryId",
                principalTable: "CashflowCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashAccounts_Accounts_AccountId",
                table: "CashAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_CashAccounts_Currencies_CurrencyId",
                table: "CashAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_CashflowCategories_Accounts_AccountId",
                table: "CashflowCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_Cashflows_CashAccounts_CashAccountId",
                table: "Cashflows");

            migrationBuilder.DropForeignKey(
                name: "FK_Cashflows_CashflowCategories_CashflowCategoryId",
                table: "Cashflows");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Currencies",
                table: "Currencies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cashflows",
                table: "Cashflows");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CashflowCategories",
                table: "CashflowCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CashAccounts",
                table: "CashAccounts");

            migrationBuilder.RenameTable(
                name: "Currencies",
                newName: "Currency");

            migrationBuilder.RenameTable(
                name: "Cashflows",
                newName: "Cashflow");

            migrationBuilder.RenameTable(
                name: "CashflowCategories",
                newName: "CashflowCategory");

            migrationBuilder.RenameTable(
                name: "CashAccounts",
                newName: "CashAccount");

            migrationBuilder.RenameIndex(
                name: "IX_Cashflows_CashflowCategoryId",
                table: "Cashflow",
                newName: "IX_Cashflow_CashflowCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Cashflows_CashAccountId",
                table: "Cashflow",
                newName: "IX_Cashflow_CashAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_CashflowCategories_AccountId",
                table: "CashflowCategory",
                newName: "IX_CashflowCategory_AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_CashAccounts_CurrencyId",
                table: "CashAccount",
                newName: "IX_CashAccount_CurrencyId");

            migrationBuilder.RenameIndex(
                name: "IX_CashAccounts_AccountId",
                table: "CashAccount",
                newName: "IX_CashAccount_AccountId");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Cashflow",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "CashAccount",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Currency",
                table: "Currency",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cashflow",
                table: "Cashflow",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CashflowCategory",
                table: "CashflowCategory",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CashAccount",
                table: "CashAccount",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AccountId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cashflow_UserId",
                table: "Cashflow",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CashAccount_UserId",
                table: "CashAccount",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AccountId",
                table: "Users",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_CashAccount_Accounts_AccountId",
                table: "CashAccount",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CashAccount_Currency_CurrencyId",
                table: "CashAccount",
                column: "CurrencyId",
                principalTable: "Currency",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CashAccount_Users_UserId",
                table: "CashAccount",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cashflow_CashAccount_CashAccountId",
                table: "Cashflow",
                column: "CashAccountId",
                principalTable: "CashAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cashflow_CashflowCategory_CashflowCategoryId",
                table: "Cashflow",
                column: "CashflowCategoryId",
                principalTable: "CashflowCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cashflow_Users_UserId",
                table: "Cashflow",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CashflowCategory_Accounts_AccountId",
                table: "CashflowCategory",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
