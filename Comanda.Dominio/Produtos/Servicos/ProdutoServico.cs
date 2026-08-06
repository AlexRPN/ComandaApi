using Comanda.Dominio.Produtos.Comandos;
using Comanda.Dominio.Produtos.Entidades;
using Comanda.Dominio.Produtos.Repositorios.Filtros;
using Comanda.Dominio.Produtos.Repositorios.Interfaces;
using Comanda.Dominio.Produtos.Servicos.Interfaces;
using Comanda.Dominio.Utils.Consultas;
using Comanda.Dominio.Utils.Enumeradores;
using Comanda.Dominio.Utils.Filtros.Enumeradores;

namespace Comanda.Dominio.Produtos.Servicos
{
    public class ProdutoServico : IProdutoServico
    {
        private readonly IProdutoRepositorio produtoRepositorio;
        private readonly IProdutoDapperRepositorio produtoDapperRepositorio;
        public ProdutoServico(IProdutoRepositorio produtoRepositorio,
                              IProdutoDapperRepositorio produtoDapperRepositorio)
        {
            this.produtoRepositorio = produtoRepositorio;
            this.produtoDapperRepositorio = produtoDapperRepositorio;
        }

        public async Task<Produto> InserirAsync(ProdutoComando comando, CancellationToken cancellationToken)
        {
            bool produtoExistente = await produtoRepositorio.ValidarAsync(x => x.EmpresaId == comando.EmpresaId &&
                                                                          x.CategoriaId == comando.CategoriaId &&
                                                                          x.Nome == comando.Nome, cancellationToken);

            if (produtoExistente)
            {
                throw new Exception("Produto já cadastrado para esta empresa e categoria.");
            }

            var produto = new ProdutoComando
            {
                EmpresaId = comando.EmpresaId,
                CategoriaId = comando.CategoriaId,
                Nome = comando.Nome,
                Descricao = comando.Descricao,
                TempoPreparo = comando.TempoPreparo,
                Status = AtivoInativoEnum.Ativo,
                SituacaoProduto = SituacaoProdutoEnum.Disponivel,
                DataCadastro = DateTime.UtcNow,
                DataAlteracao = DateTime.UtcNow
            };

            return await produtoRepositorio.InserirAsync(produto, cancellationToken);
        }

        public async Task<IQueryable<Produto>> FiltrarAsync(ProdutoListarFiltro filtro, CancellationToken cancellationToken)
        {
            return await produtoRepositorio.FiltrarAsync(filtro, cancellationToken);
        }

        public async Task<PaginacaoConsulta<Produto>> ListarPaginadoAsync(IQueryable<Produto> query, int qt, int pg, string cpOrd, TipoOrdenacaoEnum tpOrd, CancellationToken cancellationToken)
        {
            return await produtoRepositorio.ListarPaginadoAsync(query, qt, pg, cpOrd, tpOrd, cancellationToken);
        }
    }
}
