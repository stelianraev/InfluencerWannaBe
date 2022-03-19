using Microsoft.EntityFrameworkCore.Migrations;

namespace InfluencerWannaBe.Data.Migrations
{
    public partial class fixDbRemoveCompaniesAddPublishers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Publisher_PublisherId",
                table: "Offers");

            migrationBuilder.DropForeignKey(
                name: "FK_Publisher_Countries_CountryId",
                table: "Publisher");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Publisher_PublisherId",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Publisher",
                table: "Publisher");

            migrationBuilder.RenameTable(
                name: "Publisher",
                newName: "Publishers");

            migrationBuilder.RenameIndex(
                name: "IX_Publisher_CountryId",
                table: "Publishers",
                newName: "IX_Publishers_CountryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Publishers",
                table: "Publishers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Publishers_PublisherId",
                table: "Offers",
                column: "PublisherId",
                principalTable: "Publishers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Publishers_Countries_CountryId",
                table: "Publishers",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Publishers_PublisherId",
                table: "Reviews",
                column: "PublisherId",
                principalTable: "Publishers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Publishers_PublisherId",
                table: "Offers");

            migrationBuilder.DropForeignKey(
                name: "FK_Publishers_Countries_CountryId",
                table: "Publishers");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Publishers_PublisherId",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Publishers",
                table: "Publishers");

            migrationBuilder.RenameTable(
                name: "Publishers",
                newName: "Publisher");

            migrationBuilder.RenameIndex(
                name: "IX_Publishers_CountryId",
                table: "Publisher",
                newName: "IX_Publisher_CountryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Publisher",
                table: "Publisher",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Publisher_PublisherId",
                table: "Offers",
                column: "PublisherId",
                principalTable: "Publisher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Publisher_Countries_CountryId",
                table: "Publisher",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Publisher_PublisherId",
                table: "Reviews",
                column: "PublisherId",
                principalTable: "Publisher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
