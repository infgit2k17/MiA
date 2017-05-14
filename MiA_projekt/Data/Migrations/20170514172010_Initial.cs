using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MiA_projekt.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Stree",
                table: "Addresses",
                newName: "Street");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Street",
                table: "Addresses",
                newName: "Stree");
        }
    }
}
