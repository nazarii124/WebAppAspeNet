using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppAspeNet.Data.Migrations
{
    public partial class teste_autor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Autor",
                table: "jokes",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Autor",
                table: "jokes");
        }
    }
}
