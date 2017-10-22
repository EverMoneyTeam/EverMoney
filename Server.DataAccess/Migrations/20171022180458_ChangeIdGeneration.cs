using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Server.DataAccess.Migrations
{
    public partial class ChangeIdGeneration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoryChange_Accounts_AccountId1",
                table: "HistoryChange");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HistoryChange",
                table: "HistoryChange");

            migrationBuilder.RenameTable(
                name: "HistoryChange",
                newName: "HistoryChanges");

            migrationBuilder.RenameIndex(
                name: "IX_HistoryChange_AccountId1",
                table: "HistoryChanges",
                newName: "IX_HistoryChanges_AccountId1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HistoryChanges",
                table: "HistoryChanges",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryChanges_Accounts_AccountId1",
                table: "HistoryChanges",
                column: "AccountId1",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoryChanges_Accounts_AccountId1",
                table: "HistoryChanges");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HistoryChanges",
                table: "HistoryChanges");

            migrationBuilder.RenameTable(
                name: "HistoryChanges",
                newName: "HistoryChange");

            migrationBuilder.RenameIndex(
                name: "IX_HistoryChanges_AccountId1",
                table: "HistoryChange",
                newName: "IX_HistoryChange_AccountId1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HistoryChange",
                table: "HistoryChange",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryChange_Accounts_AccountId1",
                table: "HistoryChange",
                column: "AccountId1",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
