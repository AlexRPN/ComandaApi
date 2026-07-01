using Comanda.Dominio.Clientes.Comandos;
using Comanda.Dominio.Clientes.Entidades;
using Comanda.Dominio.Clientes.Repositorios.Interfaces;
using Comanda.Dominio.Clientes.Servicos.Interfaces;
using Comanda.Dominio.Utils.Enumeradores;

namespace Comanda.Dominio.Clientes.Servicos
{
    public class ClienteServico : IClienteServico
    {
        private readonly IClienteRepositorio clienteRepositorio;
        public ClienteServico(IClienteRepositorio clienteRepositorio)
        {
            this.clienteRepositorio = clienteRepositorio;
        }

        public async Task<Cliente> InserirAsync(ClienteComando comando, CancellationToken cancellationToken)
        {
            await ValidarTelefoneDuplicadoAsync(comando.EmpresaId, comando.Telefone, cancellationToken);

            var cliente = new ClienteComando
            {
                EmpresaId = comando.EmpresaId,
                Nome = comando.Nome,
                Telefone = comando.Telefone,
                PontosFidelidade = comando.PontosFidelidade,
                DataCadastro = DateTime.UtcNow,
                Status = AtivoInativoEnum.Ativo
            };

            return await clienteRepositorio.InserirAsync(cliente, cancellationToken);
        }

        private async Task ValidarTelefoneDuplicadoAsync(int empresaId, string telefone, CancellationToken cancellationToken)
        {
            var cliente = await clienteRepositorio.RecuperarAsync(x => x.EmpresaId == empresaId &&
                                                                       x.Telefone == telefone, cancellationToken);

            if (cliente != null)
            {
                throw new Exception("Já existe um cliente cadastrado com esse telefone.");
            }
        }

        public async Task<Cliente> RecuperarAsync(int id, CancellationToken cancellationToken)
        {
            var cliente = await clienteRepositorio.RecuperarAsync(id, cancellationToken);

            if(cliente == null)
            {
                throw new Exception("Cliente não encontrado.");
            }

            return cliente;
        }
    }
}
