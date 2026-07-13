using Comanda.Dominio.Produtos.Entidades;
using Comanda.Dominio.Produtos.Repositorios.Filtros;
using Comanda.Dominio.Produtos.Repositorios.Interfaces;
using Comanda.Infra.ConnectionFactory.Interfaces;
using Dapper;
using System.Text;

namespace Comanda.Infra.Produtos.Repositorios
{
    public class ProdutoDapperRepositorio : IProdutoDapperRepositorio
    {
        private readonly IDbConnectionFactory _connectionFactory;
        public ProdutoDapperRepositorio(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        const string listarProdutos = @"SELECT Id,
                                               EmpresaId,
                                               CategoriaId,
                                               Nome,
                                               Descricao,
                                               TempoPreparo,
                                               Status,
                                               SituacaoProduto,
                                               DataCadastro,
                                               DataAlteracao
                                           FROM Produtos
                                           WHERE 1 = 1";

        public async Task<IEnumerable<Produto>> ListarAsync(ProdutoListarFiltro filtro, CancellationToken cancellationToken)
        {
            using var connection = _connectionFactory.CreateConnection();

            StringBuilder sql = new(listarProdutos);

            DynamicParameters parameters = new();

            if (filtro.EmpresaId > 0)
            {
                sql.AppendLine(" AND EmpresaId = @EmpresaId");
                parameters.Add("EmpresaId", filtro.EmpresaId);
            }

            if (filtro.CategoriaId > 0)
            {
                sql.AppendLine(" AND CategoriaId = @CategoriaId");
                parameters.Add("CategoriaId", filtro.CategoriaId);
            }

            if (!string.IsNullOrWhiteSpace(filtro.Nome))
            {
                sql.AppendLine(" AND Nome LIKE @Nome");
                parameters.Add("Nome", $"%{filtro.Nome}%");
            }

            if (filtro.Status.HasValue)
            {
                sql.AppendLine(" AND Status = @Status");
                parameters.Add("Status", filtro.Status);
            }

            sql.AppendLine(" ORDER BY Nome");

            CommandDefinition command = new(sql.ToString(), parameters, cancellationToken: cancellationToken);

            return await connection.QueryAsync<Produto>(command);
        }
    }
}
