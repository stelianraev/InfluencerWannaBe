using Microsoft.EntityFrameworkCore.Migrations;

namespace InfluencerWannaBe.Data.Migrations
{
    public partial class RemoveSocialMediaDbSetOptimization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_SocialMedias_SocialMediaId",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_Influencers_SocialMedias_SocialMediaId",
                table: "Influencers");

            migrationBuilder.DropTable(
                name: "SocialMedias");

            migrationBuilder.DropIndex(
                name: "IX_Influencers_SocialMediaId",
                table: "Influencers");

            migrationBuilder.DropIndex(
                name: "IX_Companies_SocialMediaId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "SocialMediaId",
                table: "Influencers");

            migrationBuilder.DropColumn(
                name: "SocialMediaId",
                table: "Companies");

            migrationBuilder.RenameColumn(
                name: "Discryption",
                table: "Influencers",
                newName: "Discription");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Influencers",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FacebookUrl",
                table: "Influencers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InstagramUrl",
                table: "Influencers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TikTokUrl",
                table: "Influencers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TwitterUrl",
                table: "Influencers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WebSiteUrl",
                table: "Influencers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "YouTubeUrl",
                table: "Influencers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AnotherLinks",
                table: "Companies",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FacebookUrl",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InstagramUrl",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TikTokUrl",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TwitterUrl",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "YouTubeUrl",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Influencers");

            migrationBuilder.DropColumn(
                name: "FacebookUrl",
                table: "Influencers");

            migrationBuilder.DropColumn(
                name: "InstagramUrl",
                table: "Influencers");

            migrationBuilder.DropColumn(
                name: "TikTokUrl",
                table: "Influencers");

            migrationBuilder.DropColumn(
                name: "TwitterUrl",
                table: "Influencers");

            migrationBuilder.DropColumn(
                name: "WebSiteUrl",
                table: "Influencers");

            migrationBuilder.DropColumn(
                name: "YouTubeUrl",
                table: "Influencers");

            migrationBuilder.DropColumn(
                name: "AnotherLinks",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "FacebookUrl",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "InstagramUrl",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "TikTokUrl",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "TwitterUrl",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "YouTubeUrl",
                table: "Companies");

            migrationBuilder.RenameColumn(
                name: "Discription",
                table: "Influencers",
                newName: "Discryption");

            migrationBuilder.AddColumn<int>(
                name: "SocialMediaId",
                table: "Influencers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SocialMediaId",
                table: "Companies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SocialMedias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FacebookUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstagramUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TikTokUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YouTubeUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialMedias", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Influencers_SocialMediaId",
                table: "Influencers",
                column: "SocialMediaId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_SocialMediaId",
                table: "Companies",
                column: "SocialMediaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_SocialMedias_SocialMediaId",
                table: "Companies",
                column: "SocialMediaId",
                principalTable: "SocialMedias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Influencers_SocialMedias_SocialMediaId",
                table: "Influencers",
                column: "SocialMediaId",
                principalTable: "SocialMedias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
