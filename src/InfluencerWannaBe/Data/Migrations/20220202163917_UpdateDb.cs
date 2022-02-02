using Microsoft.EntityFrameworkCore.Migrations;

namespace InfluencerWannaBe.Data.Migrations
{
    public partial class UpdateDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Influencers_Companies_CompanyId",
                table: "Influencers");

            migrationBuilder.DropForeignKey(
                name: "FK_Influencers_Countries_CountryId",
                table: "Influencers");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Influencers_InfluencerId",
                table: "Offers");

            migrationBuilder.DropIndex(
                name: "IX_Offers_InfluencerId",
                table: "Offers");

            migrationBuilder.DropIndex(
                name: "IX_Influencers_CompanyId",
                table: "Influencers");

            migrationBuilder.DropColumn(
                name: "InfluencerId",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Influencers");

            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "Influencers",
                newName: "Genderid");

            migrationBuilder.AlterColumn<int>(
                name: "InfluencerId",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InfluencerCompanies",
                columns: table => new
                {
                    CompaniesId = table.Column<int>(type: "int", nullable: false),
                    InfluencersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfluencerCompanies", x => new { x.CompaniesId, x.InfluencersId });
                    table.ForeignKey(
                        name: "FK_InfluencerCompanies_Companies_CompaniesId",
                        column: x => x.CompaniesId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InfluencerCompanies_Influencers_InfluencersId",
                        column: x => x.InfluencersId,
                        principalTable: "Influencers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InfluencerOffers",
                columns: table => new
                {
                    InfluencersId = table.Column<int>(type: "int", nullable: false),
                    OffersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfluencerOffers", x => new { x.InfluencersId, x.OffersId });
                    table.ForeignKey(
                        name: "FK_InfluencerOffers_Influencers_InfluencersId",
                        column: x => x.InfluencersId,
                        principalTable: "Influencers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InfluencerOffers_Offers_OffersId",
                        column: x => x.OffersId,
                        principalTable: "Offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Influencers_Genderid",
                table: "Influencers",
                column: "Genderid");

            migrationBuilder.CreateIndex(
                name: "IX_InfluencerCompanies_InfluencersId",
                table: "InfluencerCompanies",
                column: "InfluencersId");

            migrationBuilder.CreateIndex(
                name: "IX_InfluencerOffers_OffersId",
                table: "InfluencerOffers",
                column: "OffersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Influencers_Countries_CountryId",
                table: "Influencers",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Influencers_Genders_Genderid",
                table: "Influencers",
                column: "Genderid",
                principalTable: "Genders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Influencers_Countries_CountryId",
                table: "Influencers");

            migrationBuilder.DropForeignKey(
                name: "FK_Influencers_Genders_Genderid",
                table: "Influencers");

            migrationBuilder.DropTable(
                name: "Genders");

            migrationBuilder.DropTable(
                name: "InfluencerCompanies");

            migrationBuilder.DropTable(
                name: "InfluencerOffers");

            migrationBuilder.DropIndex(
                name: "IX_Influencers_Genderid",
                table: "Influencers");

            migrationBuilder.RenameColumn(
                name: "Genderid",
                table: "Influencers",
                newName: "Gender");

            migrationBuilder.AlterColumn<int>(
                name: "InfluencerId",
                table: "Reviews",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "InfluencerId",
                table: "Offers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Influencers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Offers_InfluencerId",
                table: "Offers",
                column: "InfluencerId");

            migrationBuilder.CreateIndex(
                name: "IX_Influencers_CompanyId",
                table: "Influencers",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Influencers_Companies_CompanyId",
                table: "Influencers",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Influencers_Countries_CountryId",
                table: "Influencers",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Influencers_InfluencerId",
                table: "Offers",
                column: "InfluencerId",
                principalTable: "Influencers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
