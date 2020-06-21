using Microsoft.EntityFrameworkCore.Migrations;

namespace DLL.Migrations
{
    public partial class AddCliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CEP",
                table: "Vendas");

            migrationBuilder.DropColumn(
                name: "Cidade",
                table: "Vendas");

            migrationBuilder.DropColumn(
                name: "Endereco",
                table: "Vendas");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Vendas");

            migrationBuilder.DropColumn(
                name: "PrimeiroNome",
                table: "Vendas");

            migrationBuilder.DropColumn(
                name: "Telefone",
                table: "Vendas");

            migrationBuilder.DropColumn(
                name: "UltimoNome",
                table: "Vendas");

            migrationBuilder.AddColumn<int>(
                name: "ClienteId",
                table: "Vendas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrimeiroNome = table.Column<string>(nullable: false),
                    UltimoNome = table.Column<string>(nullable: false),
                    Endereco = table.Column<string>(maxLength: 50, nullable: false),
                    Cidade = table.Column<string>(maxLength: 50, nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    CEP = table.Column<string>(nullable: true),
                    Telefone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_ClienteId",
                table: "Vendas",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendas_Clientes_ClienteId",
                table: "Vendas",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendas_Clientes_ClienteId",
                table: "Vendas");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropIndex(
                name: "IX_Vendas_ClienteId",
                table: "Vendas");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "Vendas");

            migrationBuilder.AddColumn<string>(
                name: "CEP",
                table: "Vendas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cidade",
                table: "Vendas",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Endereco",
                table: "Vendas",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Vendas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrimeiroNome",
                table: "Vendas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Telefone",
                table: "Vendas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UltimoNome",
                table: "Vendas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
