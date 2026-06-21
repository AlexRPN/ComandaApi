using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Comanda.Infra.Migrations
{
    /// <inheritdoc />
    public partial class CriaTabelaEmpresas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeFantasia = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    RazaoSocial = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    Cnpj = table.Column<string>(type: "nvarchar(14)", nullable: false),
                    InscricaoEstadual = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    BannerPrincipal = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Empresas");
        }
    }
}
