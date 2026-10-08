using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Comanda.Infra.Migrations
{
    /// <inheritdoc />
    public partial class CriaTabelaUsuariosCredenciaisAjusteTabelaUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SenhaHash",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "SenhaSalt",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "UltimoAcesso",
                table: "Usuarios");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "Usuarios",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UsuariosCredenciais",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    NomeUsuario = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    SenhaHash = table.Column<byte[]>(type: "varbinary(250)", nullable: false),
                    SenhaSalt = table.Column<byte[]>(type: "varbinary(250)", nullable: false),
                    DataAlteracaoSenha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UltimoAcesso = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFimBloqueio = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosCredenciais", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuariosCredenciais_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosCredenciais_UsuarioId",
                table: "UsuariosCredenciais",
                column: "UsuarioId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuariosCredenciais");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "Usuarios");

            migrationBuilder.AddColumn<byte[]>(
                name: "SenhaHash",
                table: "Usuarios",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "SenhaSalt",
                table: "Usuarios",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<DateTime>(
                name: "UltimoAcesso",
                table: "Usuarios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
