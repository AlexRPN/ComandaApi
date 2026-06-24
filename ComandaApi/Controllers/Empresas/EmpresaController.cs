using Comanda.Aplicacao.Empresas.Servicos.Interfaces;
using Comanda.DataTransfer.Empresas.Request;
using Comanda.DataTransfer.Empresas.Response;
using Comanda.Dominio.Utils.Consultas;
using Microsoft.AspNetCore.Mvc;

namespace ComandaApi.Controllers.Empresas
{
    [ApiController]
    [Route("api/empresas")]
    public class EmpresaController : ControllerBase
    {
        private readonly IEmpresaAppServico empresaAppServico;
        public EmpresaController(IEmpresaAppServico empresaAppServico)
        {
            this.empresaAppServico = empresaAppServico;
        }

        /// <summary>
        /// Insere uma nova empresa
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("inserir")]
        public async Task<IActionResult> InserirAsync([FromBody] EmpresaRequest request, CancellationToken cancellationToken)
        {
            var response = await empresaAppServico.InserirAsync(request, cancellationToken);
            return Ok(response);
        }

        /// <summary>
        /// Recupera uma empresa pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Route("recuperar/{id}")]
        public async Task<IActionResult> RecuperarAsync(int id, CancellationToken cancellationToken)
        {
            var response = await empresaAppServico.RecuperarAsync(id, cancellationToken);
            return Ok(response);
        }

        /// <summary>
        /// Recupera uma lista de empresas filtrada por id ou cnpj
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("listar")]
        public async Task<ActionResult<PaginacaoConsulta<EmpresaResponse>>> ListarAsync([FromBody] EmpresaListarRequest request, CancellationToken cancellationToken)
        {
            var response = await empresaAppServico.ListarAsync(request, cancellationToken);
            return Ok(response);
        }
    }
}
