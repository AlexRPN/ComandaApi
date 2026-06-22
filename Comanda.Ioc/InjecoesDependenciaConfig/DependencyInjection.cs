using Comanda.Aplicacao.Empresas.Servicos;
using Comanda.Aplicacao.Empresas.Servicos.Interfaces;
using Comanda.Dominio.Empresas.Repositorios.Interfaces;
using Comanda.Dominio.Empresas.Servicos;
using Comanda.Dominio.Empresas.Servicos.Interfaces;
using Comanda.Dominio.EnderecosEmpresas.Repositorios.Interfaces;
using Comanda.Dominio.EnderecosEmpresas.Servicos;
using Comanda.Dominio.EnderecosEmpresas.Servicos.Interfaces;
using Comanda.Dominio.HorariosFuncionamento.Repositorios.Interfaces;
using Comanda.Dominio.HorariosFuncionamento.Servicos;
using Comanda.Dominio.HorariosFuncionamento.Servicos.Interfaces;
using Comanda.Infra.Data;
using Comanda.Infra.Empresas.Repositorios;
using Comanda.Infra.EnderecosEmpresas.Repositorios;
using Comanda.Infra.HorariosFuncionamento.Repositorios;
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

            // Repositorio
            services.AddScoped<IEmpresaRepositorio, EmpresaRepositorio>();
            services.AddScoped<IEnderecoEmpresaRepositorio, EnderecoEmpresaRepositorio>();
            services.AddScoped<IHorarioFuncionamentoRepositorio, HorarioFuncionamentoRepositorio>();

            // Serviços
            services.AddScoped<IEmpresaServico, EmpresaServico>();
            services.AddScoped<IEnderecoEmpresaServico, EnderecoEmpresaServico>();
            services.AddScoped<IHorarioFuncionamentoServico, HorarioFuncionamentoServico>();

            // Aplicação
            services.AddScoped<IEmpresaAppServico, EmpresaAppServico>();

            return services;
        }
    }
}
