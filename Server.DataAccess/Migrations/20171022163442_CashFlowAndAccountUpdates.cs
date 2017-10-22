using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Server.DataAccess.Migrations
{
    public partial class CashFlowAndAccountUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsJointCashAccount",
                table: "CashAccounts");

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "Cashflows",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HistoryChange",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Column = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Table = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    USN = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryChange", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryChange_Accounts_AccountId1",
                        column: x => x.AccountId1,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cashflows_AccountId",
                table: "Cashflows",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryChange_AccountId1",
                table: "HistoryChange",
                column: "AccountId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Cashflows_Accounts_AccountId",
                table: "Cashflows",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cashflows_Accounts_AccountId",
                table: "Cashflows");

            migrationBuilder.DropTable(
                name: "HistoryChange");

            migrationBuilder.DropIndex(
                name: "IX_Cashflows_AccountId",
                table: "Cashflows");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Cashflows");

            migrationBuilder.AddColumn<bool>(
                name: "IsJointCashAccount",
                table: "CashAccounts",
                nullable: false,
                defaultValue: false);
        }
    }
}
