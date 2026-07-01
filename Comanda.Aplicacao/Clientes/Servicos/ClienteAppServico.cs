using Comanda.Aplicacao.Clientes.Servicos.Interfaces;
using Comanda.Aplicacao.Transacoes.Interfaces;
using Comanda.DataTransfer.Clientes.Request;
using Comanda.Dominio.Clientes.Comandos;
using Comanda.Dominio.Clientes.Entidades;
using Comanda.Dominio.Clientes.Servicos.Interfaces;
using Comanda.Dominio.Empresas.Servicos.Interfaces;
using Comanda.Dominio.EnderecoClientes.Comandos;
using Comanda.Dominio.EnderecoClientes.Servicos.Interfaces;
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
    }
}
