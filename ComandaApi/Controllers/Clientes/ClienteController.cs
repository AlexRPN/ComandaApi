using Comanda.Aplicacao.Clientes.Servicos.Interfaces;
using Comanda.DataTransfer.Clientes.Request;
using Microsoft.AspNetCore.Mvc;

namespace ComandaApi.Controllers.Clientes
{
    [ApiController]
    [Route("api/clientes")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteAppServico clienteAppServico;
        public ClienteController(IClienteAppServico clienteAppServico)
        {
            this.clienteAppServico = clienteAppServico;
        }

        /// <summary>
        /// Insere um novo cliente no sistema.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("inserir")]
        public async Task<ActionResult> InserirAsync([FromBody] ClienteRequest request, CancellationToken cancellationToken)
        {
            var response = await clienteAppServico.InserirAsync(request, cancellationToken);
            return Ok(response);
        }

        /// <summary>
        /// Lista os clientes cadastrados no sistema com base nos filtros fornecidos.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("listar")]
        public async Task<ActionResult> ListarAsync([FromBody] ClienteListarRequest request, CancellationToken cancellationToken)
        {
            var response = await clienteAppServico.ListarAsync(request, cancellationToken);
            return Ok(response);
        }

        /// <summary>
        /// Edita os dados do cliente.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("editar")]
        public async Task<ActionResult> EditarAsync([FromBody] ClienteEditarRequest request, CancellationToken cancellationToken)
        {
            var response = await clienteAppServico.EditarAsync(request, cancellationToken);
            return Ok(response);
        }
    }
}
