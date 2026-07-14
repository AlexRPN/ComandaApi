using Comanda.Aplicacao.GruposAdicionais.Servicos.Interfaces;
using Comanda.DataTransfer.GruposAdicionais.Request;
using Microsoft.AspNetCore.Mvc;

namespace ComandaApi.Controllers.GruposAdicionais
{
    [ApiController]
    [Route("api/grupos-adicionais")]
    public class GrupoAdicionalController : ControllerBase
    {
        private readonly IGrupoAdicionalAppServico grupoAdicionalAppServico;
        public GrupoAdicionalController(IGrupoAdicionalAppServico grupoAdicionalAppServico)
        {
            this.grupoAdicionalAppServico = grupoAdicionalAppServico;
        }

        /// <summary>
        /// Insere um grupo adicional com seus respectivos adicionais.
        /// </summary>
        /// <param name="requests"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("inserir")]
        public async Task<IActionResult> InserirAsync([FromBody] List<GrupoAdicionalRequest> requests, CancellationToken cancellationToken)
        {
            var response = await grupoAdicionalAppServico.InserirAsync(requests, cancellationToken);
            return Ok(response);
        }
    }
}
