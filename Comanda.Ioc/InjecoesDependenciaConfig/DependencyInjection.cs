using Comanda.Aplicacao.Categorias.Servicos;
using Comanda.Aplicacao.Categorias.Servicos.Interfaces;
using Comanda.Aplicacao.Clientes.Servicos;
using Comanda.Aplicacao.Clientes.Servicos.Interfaces;
using Comanda.Aplicacao.Empresas.Servicos;
using Comanda.Aplicacao.Empresas.Servicos.Interfaces;
using Comanda.Aplicacao.Produtos.Servicos;
using Comanda.Aplicacao.Produtos.Servicos.Interfaces;
using Comanda.Aplicacao.Transacoes.Interfaces;
using Comanda.Aplicacao.Usuarios.Servicos;
using Comanda.Aplicacao.Usuarios.Servicos.Interfaces;
using Comanda.Dominio.Adicionais.Repositorios.Interfaces;
using Comanda.Dominio.Adicionais.Servicos;
using Comanda.Dominio.Adicionais.Servicos.Interfaces;
using Comanda.Dominio.Categorias.Repositorios.Interfaces;
using Comanda.Dominio.Categorias.Servicos;
using Comanda.Dominio.Categorias.Servicos.Interfaces;
using Comanda.Dominio.Clientes.Repositorios.Interfaces;
using Comanda.Dominio.Clientes.Servicos;
using Comanda.Dominio.Clientes.Servicos.Interfaces;
using Comanda.Dominio.Empresas.Repositorios.Interfaces;
using Comanda.Dominio.Empresas.Servicos;
using Comanda.Dominio.Empresas.Servicos.Interfaces;
using Comanda.Dominio.EnderecoClientes.Repositorios.Interfaces;
using Comanda.Dominio.EnderecoClientes.Servicos;
using Comanda.Dominio.EnderecoClientes.Servicos.Interfaces;
using Comanda.Dominio.EnderecosEmpresas.Repositorios.Interfaces;
using Comanda.Dominio.EnderecosEmpresas.Servicos;
using Comanda.Dominio.EnderecosEmpresas.Servicos.Interfaces;
using Comanda.Dominio.GrupoAdicionais.Repositorios.Interfaces;
using Comanda.Dominio.GrupoAdicionais.Servicos;
using Comanda.Dominio.GrupoAdicionais.Servicos.Interfaces;
using Comanda.Dominio.HorariosFuncionamento.Repositorios.Interfaces;
using Comanda.Dominio.HorariosFuncionamento.Servicos;
using Comanda.Dominio.HorariosFuncionamento.Servicos.Interfaces;
using Comanda.Dominio.ImagensProdutos.Repositorios.Interfaces;
using Comanda.Dominio.ImagensProdutos.Servicos;
using Comanda.Dominio.ImagensProdutos.Servicos.Interfaces;
using Comanda.Dominio.Produtos.Repositorios.Interfaces;
using Comanda.Dominio.Produtos.Servicos;
using Comanda.Dominio.Produtos.Servicos.Interfaces;
using Comanda.Dominio.ProdutosVariacoes.Repositorios.Interfaces;
using Comanda.Dominio.ProdutosVariacoes.Services;
using Comanda.Dominio.ProdutosVariacoes.Services.Interfaces;
using Comanda.Dominio.Usuarios.Repositorios.Interfaces;
using Comanda.Dominio.Usuarios.Servicos;
using Comanda.Dominio.Usuarios.Servicos.Interfaces;
using Comanda.Dominio.Utils.Autenticacoes.Servicos;
using Comanda.Dominio.Utils.Autenticacoes.Servicos.Interfaces;
using Comanda.Infra.Adicionais.Repositorios;
using Comanda.Infra.Categorias.Repositorios;
using Comanda.Infra.Clientes.Repositorios;
using Comanda.Infra.ConnectionFactory;
using Comanda.Infra.ConnectionFactory.Interfaces;
using Comanda.Infra.Data;
using Comanda.Infra.Empresas.Repositorios;
using Comanda.Infra.EnderecoClientes.Repositorios;
using Comanda.Infra.EnderecosEmpresas.Repositorios;
using Comanda.Infra.GruposAdicionais.Repositorios;
using Comanda.Infra.HorariosFuncionamento.Repositorios;
using Comanda.Infra.ImagensProdutos.Repositorios;
using Comanda.Infra.Produtos.Repositorios;
using Comanda.Infra.ProdutosVariacoes.Repositorios;
using Comanda.Infra.Usuarios;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Comanda.Ioc.InjecoesDependenciaConfig
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraestrutura(
        this IServiceCollection services,
        IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));

            // IOC
            services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();

            // Repositorio
            services.AddScoped<IEmpresaRepositorio, EmpresaRepositorio>();
            services.AddScoped<IEnderecoEmpresaRepositorio, EnderecoEmpresaRepositorio>();
            services.AddScoped<IHorarioFuncionamentoRepositorio, HorarioFuncionamentoRepositorio>();
            services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            services.AddScoped<IClienteRepositorio, ClienteRepositorio>();
            services.AddScoped<IEnderecoClienteRepositorio, EnderecoClienteRepositorio>();
            services.AddScoped<ICategoriaRepositorio, CategoriaRepositorio>();
            services.AddScoped<IProdutoRepositorio, ProdutoRepositorio>();
            services.AddScoped<IProdutoDapperRepositorio, ProdutoDapperRepositorio>();
            services.AddScoped<IGrupoAdicionalRepositorio, GrupoAdicionalRepositorio>();
            services.AddScoped<IProdutoVariacaoRepositorio, ProdutoVariacaoRepositorio>();
            services.AddScoped<IImagemProdutoRepositorio, ImagemProdutoRepositorio>();
            services.AddScoped<IAdicionalRepositorio, AdicionalRepositorio>();

            // Serviços
            services.AddScoped<IAutenticacaoServico, AutenticacaoServico>();
            services.AddScoped<IEmpresaServico, EmpresaServico>();
            services.AddScoped<IEnderecoEmpresaServico, EnderecoEmpresaServico>();
            services.AddScoped<IHorarioFuncionamentoServico, HorarioFuncionamentoServico>();
            services.AddScoped<IUsuarioServico, UsuarioServico>();
            services.AddScoped<IClienteServico, ClienteServico>();
            services.AddScoped<IEnderecoClienteServico, EnderecoClienteServico>();
            services.AddScoped<ICategoriaServico, CategoriaServico>();
            services.AddScoped<IProdutoServico, ProdutoServico>();
            services.AddScoped<IGrupoAdicionalServico, GrupoAdicionalServico>();
            services.AddScoped<IProdutoVariacaoServico, ProdutoVariacaoServico>();
            services.AddScoped<IImagemProdutoServico, ImagemProdutoServico>();
            services.AddScoped<IAdicionalServico, AdicionalServico>();

            // Aplicação
            services.AddScoped<IEmpresaAppServico, EmpresaAppServico>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUsuarioAppServico, UsuarioAppServico>();
            services.AddScoped<IClienteAppServico, ClienteAppServico>();
            services.AddScoped<ICategoriaAppServico, CategoriaAppServico>();
            services.AddScoped<IProdutoAppServico, ProdutoAppServico>();

            return services;
        }
    }
}
