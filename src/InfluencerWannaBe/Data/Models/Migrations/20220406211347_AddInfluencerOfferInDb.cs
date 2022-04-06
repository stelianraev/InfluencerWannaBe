using Microsoft.EntityFrameworkCore.Migrations;

namespace InfluencerWannaBe.Data.Migrations
{
    public partial class AddInfluencerOfferInDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InfluencerOffers");

            migrationBuilder.CreateTable(
                name: "InfleuncerOffers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InfluencerId = table.Column<int>(type: "int", nullable: false),
                    OfferId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfleuncerOffers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InfleuncerOffers_Influencers_InfluencerId",
                        column: x => x.InfluencerId,
                        principalTable: "Influencers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InfleuncerOffers_Offers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InfleuncerOffers_InfluencerId",
                table: "InfleuncerOffers",
                column: "InfluencerId");

            migrationBuilder.CreateIndex(
                name: "IX_InfleuncerOffers_OfferId",
                table: "InfleuncerOffers",
                column: "OfferId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InfleuncerOffers");

            migrationBuilder.CreateTable(
                name: "InfluencerOffers",
                columns: table => new
                {
                    SignUpInfluencersId = table.Column<int>(type: "int", nullable: false),
                    SignUpOffersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfluencerOffers", x => new { x.SignUpInfluencersId, x.SignUpOffersId });
                    table.ForeignKey(
                        name: "FK_InfluencerOffers_Influencers_SignUpInfluencersId",
                        column: x => x.SignUpInfluencersId,
                        principalTable: "Influencers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InfluencerOffers_Offers_SignUpOffersId",
                        column: x => x.SignUpOffersId,
                        principalTable: "Offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InfluencerOffers_SignUpOffersId",
                table: "InfluencerOffers",
                column: "SignUpOffersId");
        }
    }
}
