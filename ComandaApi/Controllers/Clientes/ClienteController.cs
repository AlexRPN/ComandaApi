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
    }
}
