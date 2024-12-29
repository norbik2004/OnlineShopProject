using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShopProject.Migrations
{
    public partial class PhotosProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IMGFileLink",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IMGFileLink",
                table: "Products");
        }
    }
}
