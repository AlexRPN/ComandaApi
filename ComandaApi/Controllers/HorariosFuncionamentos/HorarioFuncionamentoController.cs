using Comanda.Aplicacao.HorariosFuncionamento.Servicos.Interfaces;
using Comanda.DataTransfer.HorariosFuncionamento.Request;
using Microsoft.AspNetCore.Mvc;

namespace ComandaApi.Controllers.HorariosFuncionamentos
{
    [ApiController]
    [Route("api/horarios-funcionamentos")]
    public class HorarioFuncionamentoController : ControllerBase
    {
        private readonly IHorarioFuncionamentoAppServico horarioFuncionamentoAppServico;
        public HorarioFuncionamentoController(IHorarioFuncionamentoAppServico horarioFuncionamentoAppServico)
        {
            this.horarioFuncionamentoAppServico = horarioFuncionamentoAppServico;
        }

        /// <summary>
        /// Insere os horários de funcionamento de uma empresa.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("inserir")]
        public async Task<IActionResult> InserirAsync([FromBody] IEnumerable<HorarioFuncionamentoRequest> request,
                                                                 CancellationToken cancellationToken)
        {
            var response = await horarioFuncionamentoAppServico.InserirAsync(request, cancellationToken);
            return Ok(response);
        }

        /// <summary>
        /// Edita os horários de funcionamento de uma empresa.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("editar")]
        public async Task<IActionResult> EditarAsync([FromBody] IEnumerable<HorarioFuncionamentoEditarRequest> request, 
                                                                CancellationToken cancellationToken)
        {
            var response = await horarioFuncionamentoAppServico.EditarAsync(request, cancellationToken);
            return Ok(response);
        }
    }
}
