using Microsoft.EntityFrameworkCore.Migrations;

namespace UniShop.Data.Migrations
{
    public partial class ChangeCityIdToCity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CityId",
                table: "Addresses",
                newName: "City");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "City",
                table: "Addresses",
                newName: "CityId");
        }
    }
}
