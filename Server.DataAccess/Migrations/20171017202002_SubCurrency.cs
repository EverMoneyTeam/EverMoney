using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Server.DataAccess.Migrations
{
    public partial class SubCurrency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ParentCashflowCategoryId",
                table: "CashflowCategories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CashflowCategories_ParentCashflowCategoryId",
                table: "CashflowCategories",
                column: "ParentCashflowCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_CashflowCategories_CashflowCategories_ParentCashflowCategoryId",
                table: "CashflowCategories",
                column: "ParentCashflowCategoryId",
                principalTable: "CashflowCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashflowCategories_CashflowCategories_ParentCashflowCategoryId",
                table: "CashflowCategories");

            migrationBuilder.DropIndex(
                name: "IX_CashflowCategories_ParentCashflowCategoryId",
                table: "CashflowCategories");

            migrationBuilder.DropColumn(
                name: "ParentCashflowCategoryId",
                table: "CashflowCategories");
        }
    }
}
