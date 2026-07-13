using Comanda.Dominio.ProdutosVariacoes.Comandos;
using Comanda.Dominio.ProdutosVariacoes.Entidades;
using Comanda.Dominio.ProdutosVariacoes.Repositorios.Interfaces;
using Comanda.Dominio.ProdutosVariacoes.Services.Interfaces;
using Comanda.Dominio.Utils.Enumeradores;

namespace Comanda.Dominio.ProdutosVariacoes.Services
{
    public class ProdutoVariacaoServico : IProdutoVariacaoServico
    {
        private readonly IProdutoVariacaoRepositorio produtoVariacaoRepositorio;
        public ProdutoVariacaoServico(IProdutoVariacaoRepositorio produtoVariacaoRepositorio)
        {
            this.produtoVariacaoRepositorio = produtoVariacaoRepositorio;
        }

        public async Task<List<ProdutoVariacao>> InserirAsync(List<ProdutoVariacaoComando> comandos, CancellationToken cancellationToken)
        {
            var produtoVariacao = new List<ProdutoVariacaoComando>();

            bool variacaoExistente = await produtoVariacaoRepositorio.ValidarAsync(x => x.ProdutoId == comandos.First().ProdutoId &&
                                                                 x.Descricao == comandos.First().Descricao, cancellationToken);

            if (variacaoExistente)
            {
                throw new Exception("Já existe uma variação com a mesma descrição para este produto!");
            }

            foreach (var comando in comandos)
            {
                produtoVariacao.Add(new ProdutoVariacaoComando
                {
                    ProdutoId = comando.ProdutoId,
                    Descricao = comando.Descricao,
                    Preco = comando.Preco,
                    Ordem = comando.Ordem,
                    Status = AtivoInativoEnum.Ativo,
                    DataCadastro = DateTime.UtcNow,
                    DataAlteracao = DateTime.UtcNow
                });
            }

            return await produtoVariacaoRepositorio.InserirAsync(produtoVariacao, cancellationToken);
        }
    }
}
