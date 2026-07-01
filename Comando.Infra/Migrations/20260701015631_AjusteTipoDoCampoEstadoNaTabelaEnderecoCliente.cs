using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Comanda.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AjusteTipoDoCampoEstadoNaTabelaEnderecoCliente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Estado",
                table: "EnderecoClientes",
                type: "nvarchar(2)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Estado",
                table: "EnderecoClientes",
                type: "nvarchar(1)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
