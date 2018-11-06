using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeZone.Web.Data.Migrations
{
    public partial class addSection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SectionId",
                table: "Properties",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Properties_SectionId",
                table: "Properties",
                column: "SectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Sections_SectionId",
                table: "Properties",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Sections_SectionId",
                table: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_Properties_SectionId",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "SectionId",
                table: "Properties");
        }
    }
}
