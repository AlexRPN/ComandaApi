using Comanda.Aplicacao.Categorias.Servicos.Interfaces;
using Comanda.DataTransfer.Categorias.Request;
using Comanda.DataTransfer.Utils.Status.Request;
using Microsoft.AspNetCore.Mvc;

namespace ComandaApi.Controllers.Categorias
{
    [ApiController]
    [Route("api/categorias")]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaAppServico categoriaAppServico;
        public CategoriaController(ICategoriaAppServico categoriaAppServico)
        {
            this.categoriaAppServico = categoriaAppServico;
        }

        /// <summary>
        /// Insere uma nova categoria
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("inserir")]
        public async Task<ActionResult> InserirAsync([FromBody] CategoriaRequest request, CancellationToken cancellationToken)
        {
            var response = await categoriaAppServico.InserirAsync(request, cancellationToken);
            return Ok(response);
        }

        /// <summary>
        /// Edita uma categoria existente
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("editar")]
        public async Task<ActionResult> EditarAsync([FromBody] CategoriaEditarRequest request, CancellationToken cancellationToken)
        {
            var response = await categoriaAppServico.EditarAsync(request, cancellationToken);
            return Ok(response);
        }

        /// <summary>
        /// Recupera uma categoria por ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("recuperar/{id}")]
        public async Task<ActionResult> RecuperarPorIdAsync(int id, CancellationToken cancellationToken)
        {
            var response = await categoriaAppServico.RecuperarPorIdAsync(id, cancellationToken);
            return Ok(response);
        }

        /// <summary>
        /// Lista categorias com paginação
        /// </summary>
        /// <param name="filtro"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("listar")]
        public async Task<ActionResult> ListarAsync([FromQuery] CategoriaListarRequest filtro, CancellationToken cancellationToken)
        {
            var response = await categoriaAppServico.ListarPaginadoAsync(filtro, cancellationToken);
            return Ok(response);
        }

        /// <summary>
        /// Altera o status de uma categoria (Ativo/Inativo)
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("alterar-status")]
        public async Task<ActionResult> AlterarStatusAsync([FromBody] AlterarStatusRequest request, CancellationToken cancellationToken)
        {
            var response = await categoriaAppServico.AlterarStatusAsync(request, cancellationToken);
            return Ok(response);
        }
    }
}
