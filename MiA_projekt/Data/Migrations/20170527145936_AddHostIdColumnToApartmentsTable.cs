using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MiA_projekt.Data.Migrations
{
    public partial class AddHostIdColumnToApartmentsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apartments_Addresses_Addressid",
                table: "Apartments");

            migrationBuilder.RenameColumn(
                name: "Addressid",
                table: "Apartments",
                newName: "AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Apartments_Addressid",
                table: "Apartments",
                newName: "IX_Apartments_AddressId");

            migrationBuilder.AddColumn<string>(
                name: "HostId",
                table: "Apartments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Apartments_HostId",
                table: "Apartments",
                column: "HostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Apartments_Addresses_AddressId",
                table: "Apartments",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Apartments_AspNetUsers_HostId",
                table: "Apartments",
                column: "HostId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apartments_Addresses_AddressId",
                table: "Apartments");

            migrationBuilder.DropForeignKey(
                name: "FK_Apartments_AspNetUsers_HostId",
                table: "Apartments");

            migrationBuilder.DropIndex(
                name: "IX_Apartments_HostId",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "HostId",
                table: "Apartments");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                table: "Apartments",
                newName: "Addressid");

            migrationBuilder.RenameIndex(
                name: "IX_Apartments_AddressId",
                table: "Apartments",
                newName: "IX_Apartments_Addressid");

            migrationBuilder.AddForeignKey(
                name: "FK_Apartments_Addresses_Addressid",
                table: "Apartments",
                column: "Addressid",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
