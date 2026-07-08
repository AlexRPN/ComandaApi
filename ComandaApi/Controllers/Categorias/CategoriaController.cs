using Comanda.Aplicacao.Categorias.Servicos.Interfaces;
using Comanda.DataTransfer.Categorias.Request;
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
    }
}
