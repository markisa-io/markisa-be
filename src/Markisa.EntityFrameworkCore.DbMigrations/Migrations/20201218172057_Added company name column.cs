using Microsoft.EntityFrameworkCore.Migrations;

namespace Markisa.Migrations
{
    public partial class Addedcompanynamecolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "AbpUsers",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "AbpUsers");
        }
    }
}
