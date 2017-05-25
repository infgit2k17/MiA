using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MiA_projekt.Data.Migrations
{
    public partial class AddRateColumsToAparmentsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RatePoints",
                table: "Apartments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RatesCount",
                table: "Apartments",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RatePoints",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "RatesCount",
                table: "Apartments");
        }
    }
}
