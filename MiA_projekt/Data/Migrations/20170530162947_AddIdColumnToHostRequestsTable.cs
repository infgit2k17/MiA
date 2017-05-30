using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MiA_projekt.Data.Migrations
{
    public partial class AddIdColumnToHostRequestsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DocumentId",
                table: "HostRequests",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "HostRequests",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PersonalId",
                table: "HostRequests",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentId",
                table: "HostRequests");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "HostRequests");

            migrationBuilder.DropColumn(
                name: "PersonalId",
                table: "HostRequests");
        }
    }
}
