using Comanda.DataTransfer.Produtos.Request;
using Comanda.DataTransfer.Produtos.Response;
using Comanda.Dominio.Produtos.Comandos;
using Comanda.Dominio.Produtos.Entidades;
using Comanda.Dominio.Produtos.Repositorios.Filtros;
using Mapster;

namespace Comanda.Aplicacao.Produtos.Profiles
{
    public class ProdutoProfile : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ProdutoListarRequest, ProdutoListarFiltro>();
            config.NewConfig<Produto, ProdutoComando>();
            config.NewConfig<Produto, ProdutoListarResponse>()
                .Map(dest => dest.ProdutoVariacao, src => src.ProdutoVariacao)
                .Map(dest => dest.ImagensProdutos, src => src.ImagemProduto);
            config.NewConfig<ProdutoRequest, Produto>()
                .Map(dest => dest.ProdutoVariacao, src => src.ProdutoVariacao);
        }
    }
}
