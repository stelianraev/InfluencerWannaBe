using Microsoft.EntityFrameworkCore.Migrations;

namespace InfluencerWannaBe.Data.Migrations
{
    public partial class AddPublisherLogic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InfluencerOffers_Influencers_InfluencersId",
                table: "InfluencerOffers");

            migrationBuilder.DropForeignKey(
                name: "FK_InfluencerOffers_Offers_OffersId",
                table: "InfluencerOffers");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Publishers_PublisherId",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "OffersId",
                table: "InfluencerOffers",
                newName: "SignUpOffersId");

            migrationBuilder.RenameColumn(
                name: "InfluencersId",
                table: "InfluencerOffers",
                newName: "SignUpInfluencersId");

            migrationBuilder.RenameIndex(
                name: "IX_InfluencerOffers_OffersId",
                table: "InfluencerOffers",
                newName: "IX_InfluencerOffers_SignUpOffersId");

            migrationBuilder.AlterColumn<int>(
                name: "PublisherId",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OfferId",
                table: "Publishers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Publishers_OfferId",
                table: "Publishers",
                column: "OfferId");

            migrationBuilder.AddForeignKey(
                name: "FK_InfluencerOffers_Influencers_SignUpInfluencersId",
                table: "InfluencerOffers",
                column: "SignUpInfluencersId",
                principalTable: "Influencers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InfluencerOffers_Offers_SignUpOffersId",
                table: "InfluencerOffers",
                column: "SignUpOffersId",
                principalTable: "Offers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Publishers_Offers_OfferId",
                table: "Publishers",
                column: "OfferId",
                principalTable: "Offers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Publishers_PublisherId",
                table: "Reviews",
                column: "PublisherId",
                principalTable: "Publishers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InfluencerOffers_Influencers_SignUpInfluencersId",
                table: "InfluencerOffers");

            migrationBuilder.DropForeignKey(
                name: "FK_InfluencerOffers_Offers_SignUpOffersId",
                table: "InfluencerOffers");

            migrationBuilder.DropForeignKey(
                name: "FK_Publishers_Offers_OfferId",
                table: "Publishers");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Publishers_PublisherId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Publishers_OfferId",
                table: "Publishers");

            migrationBuilder.DropColumn(
                name: "OfferId",
                table: "Publishers");

            migrationBuilder.RenameColumn(
                name: "SignUpOffersId",
                table: "InfluencerOffers",
                newName: "OffersId");

            migrationBuilder.RenameColumn(
                name: "SignUpInfluencersId",
                table: "InfluencerOffers",
                newName: "InfluencersId");

            migrationBuilder.RenameIndex(
                name: "IX_InfluencerOffers_SignUpOffersId",
                table: "InfluencerOffers",
                newName: "IX_InfluencerOffers_OffersId");

            migrationBuilder.AlterColumn<int>(
                name: "PublisherId",
                table: "Reviews",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_InfluencerOffers_Influencers_InfluencersId",
                table: "InfluencerOffers",
                column: "InfluencersId",
                principalTable: "Influencers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InfluencerOffers_Offers_OffersId",
                table: "InfluencerOffers",
                column: "OffersId",
                principalTable: "Offers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Publishers_PublisherId",
                table: "Reviews",
                column: "PublisherId",
                principalTable: "Publishers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
