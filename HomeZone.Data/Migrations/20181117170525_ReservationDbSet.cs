using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeZone.Web.Data.Migrations
{
    public partial class ReservationDbSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Properties_PropertyId",
                table: "Reservation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservation",
                table: "Reservation");

            migrationBuilder.RenameTable(
                name: "Reservation",
                newName: "Reservetions");

            migrationBuilder.RenameIndex(
                name: "IX_Reservation_PropertyId",
                table: "Reservetions",
                newName: "IX_Reservetions_PropertyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservetions",
                table: "Reservetions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservetions_Properties_PropertyId",
                table: "Reservetions",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservetions_Properties_PropertyId",
                table: "Reservetions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservetions",
                table: "Reservetions");

            migrationBuilder.RenameTable(
                name: "Reservetions",
                newName: "Reservation");

            migrationBuilder.RenameIndex(
                name: "IX_Reservetions_PropertyId",
                table: "Reservation",
                newName: "IX_Reservation_PropertyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservation",
                table: "Reservation",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Properties_PropertyId",
                table: "Reservation",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
