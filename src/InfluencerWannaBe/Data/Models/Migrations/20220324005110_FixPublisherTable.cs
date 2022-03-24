using Microsoft.EntityFrameworkCore.Migrations;

namespace InfluencerWannaBe.Data.Migrations
{
    public partial class FixPublisherTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Publishers",
                newName: "LastName");

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "Publishers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Publishers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Publishers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "GenderId",
                table: "Publishers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                table: "Publishers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Publishers",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Publishers_GenderId",
                table: "Publishers",
                column: "GenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Publishers_Genders_GenderId",
                table: "Publishers",
                column: "GenderId",
                principalTable: "Genders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publishers_Genders_GenderId",
                table: "Publishers");

            migrationBuilder.DropIndex(
                name: "IX_Publishers_GenderId",
                table: "Publishers");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Publishers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Publishers");

            migrationBuilder.DropColumn(
                name: "GenderId",
                table: "Publishers");

            migrationBuilder.DropColumn(
                name: "MiddleName",
                table: "Publishers");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Publishers");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Publishers",
                newName: "Name");

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "Publishers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
