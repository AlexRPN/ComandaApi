using Comanda.Infra.Data;
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

            // Serviços

            // Aplicação


            return services;
        }
    }
}
