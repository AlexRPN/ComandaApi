using Comanda.Aplicacao.Clientes.Servicos.Interfaces;
using Comanda.Aplicacao.Transacoes.Interfaces;
using Comanda.DataTransfer.Clientes.Request;
using Comanda.DataTransfer.Clientes.Response;
using Comanda.DataTransfer.EnderecoClientes.Response;
using Comanda.Dominio.Clientes.Comandos;
using Comanda.Dominio.Clientes.Entidades;
using Comanda.Dominio.Clientes.Repositorios.Filtros;
using Comanda.Dominio.Clientes.Servicos.Interfaces;
using Comanda.Dominio.Empresas.Servicos.Interfaces;
using Comanda.Dominio.EnderecoClientes.Comandos;
using Comanda.Dominio.EnderecoClientes.Entidades;
using Comanda.Dominio.EnderecoClientes.Servicos.Interfaces;
using Comanda.Dominio.Utils.Consultas;
using Comanda.Dominio.Utils.Enumeradores;
using Mapster;
using Microsoft.Extensions.Logging;

namespace Comanda.Aplicacao.Clientes.Servicos
{
    public class ClienteAppServico : IClienteAppServico
    {
        private readonly IClienteServico clienteServico;
        private readonly IEnderecoClienteServico enderecoClienteServico;
        private readonly IEmpresaServico empresaServico;
        private readonly ILogger<ClienteAppServico> logger;
        private readonly IUnitOfWork unitOfWork;
        public ClienteAppServico(IClienteServico clienteServico,
                                 IEnderecoClienteServico enderecoClienteServico,
                                 IEmpresaServico empresaServico,
                                 ILogger<ClienteAppServico> logger,
                                 IUnitOfWork unitOfWork)
        {
            this.clienteServico = clienteServico;
            this.enderecoClienteServico = enderecoClienteServico;
            this.empresaServico = empresaServico;
            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }

        public async Task<string> InserirAsync(ClienteRequest request, CancellationToken cancellationToken)
        {
            ClienteComando comando = request.Adapt<ClienteComando>();
            EnderecoClienteComando enderecoComando = request.Endereco.Adapt<EnderecoClienteComando>();

            try
            {
                await unitOfWork.BeginTransactionAsync(cancellationToken);

                logger.LogInformation("Iniciando inserção de cliente.");
                await empresaServico.ValidarAsync(comando.EmpresaId, cancellationToken);

                Cliente cliente = await clienteServico.InserirAsync(comando, cancellationToken);

                if (cliente == null)
                {
                    throw new Exception("Falha ao inserir cliente.");
                }

                await unitOfWork.CommitAsync(cancellationToken);

                enderecoComando.ClienteId = cliente.Id;

                logger.LogInformation("Iniciando inserção de endereço para o cliente {ClienteId}", cliente.Id);
                await enderecoClienteServico.InserirAsync(enderecoComando, cancellationToken);

                await unitOfWork.CommitAsync(cancellationToken);
                await unitOfWork.CommitTransactionAsync(cancellationToken);

                logger.LogInformation("Cliente {ClienteId} inserido com sucesso.", cliente.Id);

                return $"Cliente {cliente.Nome} inserido com sucesso!";
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);
                logger.LogError(ex, "Erro ao inserir cliente.");
                throw new Exception("Erro ao inserir cliente.", ex);
            }
        }

        public async Task<string> EditarAsync(ClienteEditarRequest request, CancellationToken cancellationToken)
        {
            ClienteEditarComando clienteComando = request.Adapt<ClienteEditarComando>();
            EnderecoClienteEditarComando endereco = request.Endereco.Adapt<EnderecoClienteEditarComando>();

            try
            {
                await unitOfWork.BeginTransactionAsync(cancellationToken);

                logger.LogInformation("Iniciando atualização do cliente: {Nome}", request.Nome);
                Cliente cliente = await clienteServico.EditarAsync(clienteComando, cancellationToken);

                endereco.ClienteId = request.Id;
                EnderecoCliente enderereco = await enderecoClienteServico.EditarAsync(endereco, cancellationToken);

                await unitOfWork.CommitAsync(cancellationToken);
                await unitOfWork.CommitTransactionAsync(cancellationToken);

                var mensagem = $"Dados do cliente {cliente.Nome} atualizados com sucesso!";

                return mensagem;

            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw new Exception("Erro ao atualizar cadastro do cliente!", ex);
            }
        }

        public async Task<PaginacaoConsulta<ClienteResponse>> ListarAsync(ClienteListarRequest request, CancellationToken cancellationToken)
        {
            ClienteListarFiltro filtro = request.Adapt<ClienteListarFiltro>();
            IQueryable<Cliente> query = await clienteServico.FiltrarAsync(filtro, cancellationToken);

            PaginacaoConsulta<Cliente> clientes = await clienteServico.ListarPaginadoAsync(query, request.Qt, request.Pg, request.CpOrd, request.TpOrd, cancellationToken);

            PaginacaoConsulta<ClienteResponse> response = clientes.Adapt<PaginacaoConsulta<ClienteResponse>>();
            return response;
        }

        public async Task<ClienteResponse> RecuperarAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                Cliente cliente = await clienteServico.RecuperarAsync(id, cancellationToken);

                var clienteResponse = cliente.Adapt<ClienteResponse>();
                clienteResponse.Endereco = cliente.EnderecoCliente.Adapt<EnderecoClienteResponse>();

                return clienteResponse;
            }
            catch (Exception ex)
            {
                logger.LogInformation("Cliente não encontrado!");
                throw new ArgumentException("Cliente não encontrado!", ex);
            }
        }

        public async Task<string> AlterarStatusAsync(int id, AtivoInativoEnum status, CancellationToken cancellationToken)
        {
            try
            {
                await unitOfWork.BeginTransactionAsync(cancellationToken);

                var statusAlterado = await clienteServico.AlterarStatusAsync(id, status, cancellationToken);

                await unitOfWork.CommitAsync(cancellationToken);
                await unitOfWork.CommitTransactionAsync(cancellationToken);

                var mensagem = $"Status do cliente {statusAlterado.Nome} alterado com sucesso!";
                return mensagem;
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);
                logger.LogError(ex, "Erro ao alterar status do cliente {Id}", id);
                throw new Exception("Erro ao alterar status do cliente!", ex);
            }
        }
    }
}
