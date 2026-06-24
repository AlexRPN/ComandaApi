using Comanda.Dominio.EnderecosEmpresas.Comandos;
using Comanda.Dominio.EnderecosEmpresas.Entidades;
using Comanda.Dominio.EnderecosEmpresas.Repositorios.Interfaces;
using Comanda.Infra.Data;

namespace Comanda.Infra.EnderecosEmpresas.Repositorios
{
    public class EnderecoEmpresaRepositorio : IEnderecoEmpresaRepositorio
    {
        private readonly AppDbContext appDbContext;
        public EnderecoEmpresaRepositorio(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<EnderecoEmpresaComando> InserirAsync(EnderecoEmpresaComando comando, CancellationToken cancellationToken)
        {
            try
            {
                var endereco = new EnderecoEmpresa(comando);

                await appDbContext.EnderecoEmpresas.AddAsync(endereco, cancellationToken);
                await appDbContext.SaveChangesAsync(cancellationToken);
                comando.Id = endereco.Id;

                return comando;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao inserir endereço!", ex);
            }
        }
    }
}
