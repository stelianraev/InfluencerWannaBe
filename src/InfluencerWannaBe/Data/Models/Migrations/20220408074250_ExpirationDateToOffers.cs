using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InfluencerWannaBe.Data.Migrations
{
    public partial class ExpirationDateToOffers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Offers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsExpired",
                table: "Offers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "AcceptedForTheOffer",
                table: "InfleuncerOffers",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "IsExpired",
                table: "Offers");

            migrationBuilder.AlterColumn<bool>(
                name: "AcceptedForTheOffer",
                table: "InfleuncerOffers",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);
        }
    }
}
