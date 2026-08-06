using Comanda.Aplicacao.Produtos.Servicos.Interfaces;
using Comanda.DataTransfer.Produtos.Request;
using Microsoft.AspNetCore.Mvc;

namespace ComandaApi.Controllers.Produtos
{
    [ApiController]
    [Route("api/produtos")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoAppServico produtoAppServico;
        public ProdutoController(IProdutoAppServico produtoAppServico)
        {
            this.produtoAppServico = produtoAppServico;
        }

        /// <summary>
        /// Insere um novo produto
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("inserir")]
        // Usar [FromForm] quando o serviço de upload de arquivos estiver sendo utilizado, caso contrário, usar [FromBody]
        public async Task<IActionResult> InserirAsync([FromBody] ProdutoRequest request, CancellationToken cancellationToken)
        {
            var produto = await produtoAppServico.InserirAsync(request, cancellationToken);
            return Ok(produto);
        }

        /// <summary>
        /// Lista os produtos de acordo com os filtros informados
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("listar")]
        public async Task<IActionResult> ListarPaginadoAsync([FromBody] ProdutoListarRequest request, 
                                                                        CancellationToken cancellationToken)
        {
            var produtos = await produtoAppServico.ListarPaginadoAsync(request, cancellationToken);
            return Ok(produtos);
        }
    }
}
