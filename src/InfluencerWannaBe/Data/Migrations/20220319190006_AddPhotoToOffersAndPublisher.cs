using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InfluencerWannaBe.Data.Migrations
{
    public partial class AddPhotoToOffersAndPublisher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Photo",
                table: "Publishers",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Publishers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte[]>(
                name: "Photo",
                table: "Offers",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Publishers");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Publishers");

            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Offers");
        }
    }
}
