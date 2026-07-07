using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Comanda.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AjustaNomeTabelaGrupoAdicionais : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adicionais_Produtos_ProdutoId",
                table: "Adicionais");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Adicionais",
                table: "Adicionais");

            migrationBuilder.RenameTable(
                name: "Adicionais",
                newName: "GrupoAdicionais");

            migrationBuilder.RenameIndex(
                name: "IX_Adicionais_ProdutoId",
                table: "GrupoAdicionais",
                newName: "IX_GrupoAdicionais_ProdutoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GrupoAdicionais",
                table: "GrupoAdicionais",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GrupoAdicionais_Produtos_ProdutoId",
                table: "GrupoAdicionais",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GrupoAdicionais_Produtos_ProdutoId",
                table: "GrupoAdicionais");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GrupoAdicionais",
                table: "GrupoAdicionais");

            migrationBuilder.RenameTable(
                name: "GrupoAdicionais",
                newName: "Adicionais");

            migrationBuilder.RenameIndex(
                name: "IX_GrupoAdicionais_ProdutoId",
                table: "Adicionais",
                newName: "IX_Adicionais_ProdutoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Adicionais",
                table: "Adicionais",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Adicionais_Produtos_ProdutoId",
                table: "Adicionais",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
