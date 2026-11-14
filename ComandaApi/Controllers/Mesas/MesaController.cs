using Comanda.Aplicacao.Mesas.Servicos.Interfaces;
using Comanda.DataTransfer.Mesas.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ComandaApi.Controllers.Mesas
{
    [ApiController]
    [Route("api/mesas")]
    public class MesaController : ControllerBase
    {
        private readonly IMesaAppServico mesaAppServico;
        public MesaController(IMesaAppServico mesaAppServico)
        {
            this.mesaAppServico = mesaAppServico;
        }

        /// <summary>
        /// Insere uma nova mesa
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("inserir")]
        public async Task<IActionResult> InserirAsync([FromBody] MesaRequest request, 
                                                                 CancellationToken cancellationToken)
        {
            var response = await mesaAppServico.InserirAsync(request, cancellationToken);
            return Ok(response);
        }
    }
}
