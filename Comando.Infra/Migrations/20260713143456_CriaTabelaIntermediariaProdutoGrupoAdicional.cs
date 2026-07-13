using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Comanda.Infra.Migrations
{
    /// <inheritdoc />
    public partial class CriaTabelaIntermediariaProdutoGrupoAdicional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adicionais_GrupoAdicionais_GrupoAdicionalId",
                table: "Adicionais");

            migrationBuilder.DropForeignKey(
                name: "FK_GrupoAdicionais_Produtos_ProdutoId",
                table: "GrupoAdicionais");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GrupoAdicionais",
                table: "GrupoAdicionais");

            migrationBuilder.RenameTable(
                name: "GrupoAdicionais",
                newName: "GruposAdicionais");

            migrationBuilder.RenameIndex(
                name: "IX_GrupoAdicionais_ProdutoId",
                table: "GruposAdicionais",
                newName: "IX_GruposAdicionais_ProdutoId");

            migrationBuilder.AlterColumn<int>(
                name: "ProdutoId",
                table: "GruposAdicionais",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "GruposAdicionais",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GruposAdicionais",
                table: "GruposAdicionais",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ProdutosGruposAdicionais",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProdutoId = table.Column<int>(type: "int", nullable: false),
                    GrupoAdicionalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutosGruposAdicionais", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProdutosGruposAdicionais_GruposAdicionais_GrupoAdicionalId",
                        column: x => x.GrupoAdicionalId,
                        principalTable: "GruposAdicionais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProdutosGruposAdicionais_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GruposAdicionais_EmpresaId",
                table: "GruposAdicionais",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutosGruposAdicionais_GrupoAdicionalId",
                table: "ProdutosGruposAdicionais",
                column: "GrupoAdicionalId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutosGruposAdicionais_ProdutoId",
                table: "ProdutosGruposAdicionais",
                column: "ProdutoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Adicionais_GruposAdicionais_GrupoAdicionalId",
                table: "Adicionais",
                column: "GrupoAdicionalId",
                principalTable: "GruposAdicionais",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GruposAdicionais_Empresas_EmpresaId",
                table: "GruposAdicionais",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GruposAdicionais_Produtos_ProdutoId",
                table: "GruposAdicionais",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adicionais_GruposAdicionais_GrupoAdicionalId",
                table: "Adicionais");

            migrationBuilder.DropForeignKey(
                name: "FK_GruposAdicionais_Empresas_EmpresaId",
                table: "GruposAdicionais");

            migrationBuilder.DropForeignKey(
                name: "FK_GruposAdicionais_Produtos_ProdutoId",
                table: "GruposAdicionais");

            migrationBuilder.DropTable(
                name: "ProdutosGruposAdicionais");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GruposAdicionais",
                table: "GruposAdicionais");

            migrationBuilder.DropIndex(
                name: "IX_GruposAdicionais_EmpresaId",
                table: "GruposAdicionais");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "GruposAdicionais");

            migrationBuilder.RenameTable(
                name: "GruposAdicionais",
                newName: "GrupoAdicionais");

            migrationBuilder.RenameIndex(
                name: "IX_GruposAdicionais_ProdutoId",
                table: "GrupoAdicionais",
                newName: "IX_GrupoAdicionais_ProdutoId");

            migrationBuilder.AlterColumn<int>(
                name: "ProdutoId",
                table: "GrupoAdicionais",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GrupoAdicionais",
                table: "GrupoAdicionais",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Adicionais_GrupoAdicionais_GrupoAdicionalId",
                table: "Adicionais",
                column: "GrupoAdicionalId",
                principalTable: "GrupoAdicionais",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GrupoAdicionais_Produtos_ProdutoId",
                table: "GrupoAdicionais",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
