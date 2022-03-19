using Microsoft.EntityFrameworkCore.Migrations;

namespace InfluencerWannaBe.Data.Migrations
{
    public partial class AddUserIDtoInfluencer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Influencers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Influencers");
        }
    }
}
