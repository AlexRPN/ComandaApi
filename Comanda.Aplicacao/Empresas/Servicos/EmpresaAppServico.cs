using Comanda.Aplicacao.Empresas.Servicos.Interfaces;
using Comanda.Aplicacao.Transacoes.Interfaces;
using Comanda.DataTransfer.Empresas.Request;
using Comanda.DataTransfer.Empresas.Response;
using Comanda.DataTransfer.EnderecosEmpresas.Response;
using Comanda.DataTransfer.HorariosFuncionamento.Response;
using Comanda.Dominio.Empresas.Comandos;
using Comanda.Dominio.Empresas.Entidades;
using Comanda.Dominio.Empresas.Repositorios.Filtros;
using Comanda.Dominio.Empresas.Servicos.Interfaces;
using Comanda.Dominio.EnderecosEmpresas.Comandos;
using Comanda.Dominio.EnderecosEmpresas.Entidades;
using Comanda.Dominio.EnderecosEmpresas.Servicos.Interfaces;
using Comanda.Dominio.HorariosFuncionamento.Comando;
using Comanda.Dominio.HorariosFuncionamento.Entidades;
using Comanda.Dominio.HorariosFuncionamento.Servicos.Interfaces;
using Comanda.Dominio.Utils.Consultas;
using Mapster;
using Microsoft.Extensions.Logging;

namespace Comanda.Aplicacao.Empresas.Servicos
{
    public class EmpresaAppServico : IEmpresaAppServico
    {
        private readonly IEmpresaServico empresaServico;
        private readonly IEnderecoEmpresaServico enderecoEmpresaServico;
        private readonly IHorarioFuncionamentoServico horarioFuncionamentoServico;
        private readonly ILogger<EmpresaAppServico> logger;
        private readonly IUnitOfWork unitOfWork;
        public EmpresaAppServico(IEmpresaServico empresaServico,
                                 IEnderecoEmpresaServico enderecoEmpresaServico,
                                 IHorarioFuncionamentoServico horarioFuncionamentoServico,
                                 ILogger<EmpresaAppServico> logger,
                                 IUnitOfWork unitOfWork)
        {
            this.empresaServico = empresaServico;
            this.enderecoEmpresaServico = enderecoEmpresaServico;
            this.horarioFuncionamentoServico = horarioFuncionamentoServico;
            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }

        public async Task<string> InserirAsync(EmpresaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var horariosFuncionamento = new List<HorarioFuncionamentoInserirComando>();

                logger.LogInformation("Iniciando cadastro de empresa: {NomeFantasia}", request.NomeFantasia);
                EmpresaInserirComando comando = request.Adapt<EmpresaInserirComando>();

                var empresa = await empresaServico.InserirAsync(comando, cancellationToken);

                if (empresa == null)
                {
                    throw new Exception("Erro ao cadastrar empresa!");
                }

                logger.LogInformation("Iniciando cadastro de endereço para empresa: {EmpresaId}", empresa.Id);
                EnderecoEmpresaInserirComando enderecoComando = request.Endereco.Adapt<EnderecoEmpresaInserirComando>();

                enderecoComando.EmpresaId = empresa.Id;
                await enderecoEmpresaServico.InserirAsync(enderecoComando, cancellationToken);

                logger.LogInformation("Iniciando cadastro de horários de funcionamento para empresa: {EmpresaId}", empresa.Id);

                foreach (var horario in request.HorariosFuncionamento)
                {
                    HorarioFuncionamentoInserirComando horarioComando = horario.Adapt<HorarioFuncionamentoInserirComando>();
                    horarioComando.EmpresaId = empresa.Id;
                    horariosFuncionamento.Add(horarioComando);
                }

                var horariosInseridos = await horarioFuncionamentoServico.InserirAsync(horariosFuncionamento, cancellationToken);

                logger.LogInformation("Cadastro da empresa {RazaoSocial} concluído com sucesso: {EmpresaId}", empresa.RazaoSocial, empresa.Id);

                var mensagem = $"Empresa {empresa.RazaoSocial} cadastrada com sucesso!";
                return mensagem;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao cadastrar empresa!", ex);
            }
        }

        public async Task<string> EditarAsync(EmpresaRequest request, CancellationToken cancellationToken)
        {
            EmpresaEditarComando comando = request.Adapt<EmpresaEditarComando>();

            EnderecoEmpresaEditarComando enderecoComando = request.Endereco.Adapt<EnderecoEmpresaEditarComando>();

            IEnumerable<HorarioFuncionamentoEditarComando> horariosComando = request.HorariosFuncionamento.Adapt<IEnumerable<HorarioFuncionamentoEditarComando>>();

            try
            {
                await unitOfWork.BeginTransactionAsync(cancellationToken);

                logger.LogInformation("Iniciando atualização de empresa: {NomeFantasia}", request.NomeFantasia);
                Empresa empresa = await empresaServico.EditarAsync( comando, cancellationToken);

                logger.LogInformation("Iniciando atualização de endereço para empresa: {NomeFantasia}", empresa.NomeFantasia);
                enderecoComando.EmpresaId = empresa.Id;
                EnderecoEmpresa enderecoEmpresa = await enderecoEmpresaServico.EditarAsync(enderecoComando, cancellationToken);

                logger.LogInformation("Iniciando atualização de horários de funcionamento para empresa: {NomeFantasia}", empresa.NomeFantasia);

                IEnumerable<HorarioFuncionamento> horariosFuncionamento = await horarioFuncionamentoServico.EditarAsync(empresa.Id, horariosComando, cancellationToken);

                await unitOfWork.CommitAsync(cancellationToken);
                await unitOfWork.CommitTransactionAsync(cancellationToken);

                var mensagem = $"Dados da empresa {empresa.RazaoSocial} atualizados com sucesso!";

                return mensagem;
            }
            catch
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }

        public async Task<PaginacaoConsulta<EmpresaResponse>> ListarAsync(EmpresaListarRequest request, CancellationToken cancellationToken)
        {
            EmpresaListarFiltro filtro = request.Adapt<EmpresaListarFiltro>();
            IQueryable<Empresa> query = await empresaServico.FiltrarAsync(filtro, cancellationToken);

            PaginacaoConsulta<Empresa> empresas = await empresaServico.ListarAsync(query, request.Qt, request.Pg, request.CpOrd, request.TpOrd, cancellationToken);

            PaginacaoConsulta<EmpresaResponse> empresasResponse = empresas.Adapt<PaginacaoConsulta<EmpresaResponse>>();

            return empresasResponse;
        }

        public async Task<EmpresaResponse> RecuperarAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                var empresa = await empresaServico.ValidarAsync(id, cancellationToken);

                var empresaResponse = empresa.Adapt<EmpresaResponse>();
                empresaResponse.Endereco = empresa.EnderecoEmpresa.Adapt<EnderecoEmpresaResponse>();
                empresaResponse.HorariosFuncionamento = empresa.HorariosFuncionamento.Adapt<IEnumerable<HorarioFuncionamentoResponse>>();

                return empresaResponse;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao recuperar empresa!", ex);
            }
        }
    }
}
