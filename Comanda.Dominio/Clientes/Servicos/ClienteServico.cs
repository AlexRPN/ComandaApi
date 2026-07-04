using Comanda.Dominio.Clientes.Comandos;
using Comanda.Dominio.Clientes.Entidades;
using Comanda.Dominio.Clientes.Repositorios.Filtros;
using Comanda.Dominio.Clientes.Repositorios.Interfaces;
using Comanda.Dominio.Clientes.Servicos.Interfaces;
using Comanda.Dominio.Empresas.Entidades;
using Comanda.Dominio.Utils.Consultas;
using Comanda.Dominio.Utils.Enumeradores;
using Comanda.Dominio.Utils.Filtros.Enumeradores;

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

        public async Task<PaginacaoConsulta<Cliente>> ListarPaginadoAsync(IQueryable<Cliente> query, int qt, int pg, string cpOrd, TipoOrdenacaoEnum tpOrd, CancellationToken cancellationToken)
        {
            return await clienteRepositorio.ListarPaginadoAsync(query, qt, pg, cpOrd, tpOrd, cancellationToken);
        }

        public async Task<IQueryable<Cliente>> FiltrarAsync(ClienteListarFiltro filtro, CancellationToken cancellationToken)
        {
            return await clienteRepositorio.FiltrarAsync(filtro, cancellationToken);
        }

        public async Task<Cliente> EditarAsync(ClienteEditarComando comando, CancellationToken cancellationToken)
        {
            Cliente? cliente = await clienteRepositorio.RecuperarAsync(x => x.EmpresaId == comando.EmpresaId &&
                                                                       x.Id == comando.Id, cancellationToken);

            if (cliente is null)
            {
                throw new Exception("Cliente não encontrado!");
            }

            cliente.SetEmpresaId(comando.EmpresaId);
            cliente.SetNome(comando.Nome);
            cliente.SetTelefone(comando.Telefone);
            cliente.SetDataAlteracao(DateTime.Now);

            await clienteRepositorio.EditarAsync(cliente, cancellationToken);

            return cliente;
        }
    }
}
