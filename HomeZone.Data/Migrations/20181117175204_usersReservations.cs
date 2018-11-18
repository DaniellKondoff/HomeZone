using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeZone.Web.Data.Migrations
{
    public partial class usersReservations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Reservetions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservetions_UserId",
                table: "Reservetions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservetions_AspNetUsers_UserId",
                table: "Reservetions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservetions_AspNetUsers_UserId",
                table: "Reservetions");

            migrationBuilder.DropIndex(
                name: "IX_Reservetions_UserId",
                table: "Reservetions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Reservetions");
        }
    }
}
