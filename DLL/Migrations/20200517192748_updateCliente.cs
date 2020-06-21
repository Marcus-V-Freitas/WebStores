using Microsoft.EntityFrameworkCore.Migrations;

namespace DLL.Migrations
{
    public partial class updateCliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Clientes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Senha",
                table: "Clientes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Senha",
                table: "Clientes");
        }
    }
}
