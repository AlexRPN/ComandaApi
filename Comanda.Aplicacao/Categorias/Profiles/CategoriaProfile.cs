using Comanda.DataTransfer.Categorias.Request;
using Comanda.DataTransfer.Categorias.Response;
using Comanda.Dominio.Categorias.Comandos;
using Comanda.Dominio.Categorias.Entidades;
using Comanda.Dominio.Categorias.Repositorios.Filtros;
using Mapster;

namespace Comanda.Aplicacao.Categorias.Profiles
{
    public class CategoriaProfile : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CategoriaRequest, CategoriaComando>();
            config.NewConfig<Categoria, CategoriaResponse>();
            config.NewConfig<CategoriaEditarRequest, CategoriaEditarComando>();
            config.NewConfig<CategoriaListarRequest, CategoriaListarFiltro>();
            config.NewConfig<Categoria, CategoriaListarResponse>();
        }
    }
}
