using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Comanda.Infra.Migrations
{
    /// <inheritdoc />
    public partial class CriaTabelaEnderecoClientes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EnderecoClientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    Cep = table.Column<string>(type: "nvarchar(8)", nullable: false),
                    Logradouro = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    Numero = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Complemento = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    Bairro = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    PontoReferencia = table.Column<string>(type: "nvarchar(150)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnderecoClientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnderecoClientes_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnderecoClientes_ClienteId",
                table: "EnderecoClientes",
                column: "ClienteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnderecoClientes");
        }
    }
}
