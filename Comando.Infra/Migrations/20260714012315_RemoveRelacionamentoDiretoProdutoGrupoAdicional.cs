using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Comanda.Infra.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRelacionamentoDiretoProdutoGrupoAdicional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GruposAdicionais_Produtos_ProdutoId",
                table: "GruposAdicionais");

            migrationBuilder.DropIndex(
                name: "IX_GruposAdicionais_ProdutoId",
                table: "GruposAdicionais");

            migrationBuilder.DropColumn(
                name: "ProdutoId",
                table: "GruposAdicionais");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProdutoId",
                table: "GruposAdicionais",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GruposAdicionais_ProdutoId",
                table: "GruposAdicionais",
                column: "ProdutoId");

            migrationBuilder.AddForeignKey(
                name: "FK_GruposAdicionais_Produtos_ProdutoId",
                table: "GruposAdicionais",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id");
        }
    }
}
