using Comanda.Aplicacao.Empresas.Servicos.Interfaces;
using Comanda.DataTransfer.Empresas.Request;
using Comanda.DataTransfer.Empresas.Response;
using Comanda.DataTransfer.EnderecosEmpresas.Response;
using Comanda.DataTransfer.HorariosFuncionamento.Response;
using Comanda.Dominio.Empresas.Comandos;
using Comanda.Dominio.Empresas.Entidades;
using Comanda.Dominio.Empresas.Repositorios.Filtros;
using Comanda.Dominio.Empresas.Servicos.Interfaces;
using Comanda.Dominio.EnderecosEmpresas.Comandos;
using Comanda.Dominio.EnderecosEmpresas.Servicos.Interfaces;
using Comanda.Dominio.HorariosFuncionamento.Comando;
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
        public EmpresaAppServico(IEmpresaServico empresaServico,
                                 IEnderecoEmpresaServico enderecoEmpresaServico,
                                 IHorarioFuncionamentoServico horarioFuncionamentoServico,
                                 ILogger<EmpresaAppServico> logger)
        {
            this.empresaServico = empresaServico;
            this.enderecoEmpresaServico = enderecoEmpresaServico;
            this.horarioFuncionamentoServico = horarioFuncionamentoServico;
            this.logger = logger;
        }

        public async Task<EmpresaResponse> InserirAsync(EmpresaRequest request, CancellationToken cancellationToken)
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
                var empresaResponse = empresa.Adapt<EmpresaResponse>();

                empresaResponse.Endereco = enderecoComando.Adapt<EnderecoEmpresaResponse>();
                empresaResponse.HorariosFuncionamento = horariosInseridos.Adapt<IEnumerable<HorarioFuncionamentoResponse>>();

                empresaResponse.Mensagem = "Empresa cadastrada com sucesso!";
                return empresaResponse;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao cadastrar empresa!", ex);
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
                var empresa = await empresaServico.RecuperarAsync(id, cancellationToken);

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
