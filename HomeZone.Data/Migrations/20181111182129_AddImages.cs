using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeZone.Web.Data.Migrations
{
    public partial class AddImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "MainImage",
                table: "Properties",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "SecondaryImage",
                table: "Properties",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainImage",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "SecondaryImage",
                table: "Properties");
        }
    }
}
