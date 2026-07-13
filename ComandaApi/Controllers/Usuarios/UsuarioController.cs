using Comanda.Aplicacao.Usuarios.Servicos.Interfaces;
using Comanda.DataTransfer.Usuarios.Request;
using Comanda.DataTransfer.Utils.Status.Request;
using Microsoft.AspNetCore.Mvc;

namespace ComandaApi.Controllers.Usuarios
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioAppServico usuarioAppServico;
        public UsuarioController(IUsuarioAppServico usuarioAppServico)
        {
            this.usuarioAppServico = usuarioAppServico;
        }

        /// <summary>
        /// Insere um novo usuário no sistema.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("inserir")]
        public async Task<ActionResult> InserirAsync([FromBody] UsuarioRequest request, CancellationToken cancellationToken)
        {
            var response = await usuarioAppServico.InserirAsync(request, cancellationToken);
            return Ok(response);
        }

        /// <summary>
        /// Recupera um usuário pelo seu ID.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Route("recuperar/{id}")]
        public async Task<ActionResult> RecuperarPorIdAsync(int id, CancellationToken cancellationToken)
        {
            var response = await usuarioAppServico.RecuperarPorIdAsync(id, cancellationToken);
            return Ok(response);
        }

        /// <summary>
        /// Lista usuários de forma paginada com base nos filtros fornecidos.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("listar")]
        public async Task<ActionResult> ListarPaginadoAsync([FromBody] UsuarioListarRequest request, CancellationToken cancellationToken)
        {
            var response = await usuarioAppServico.ListarPaginadoAsync(request, cancellationToken);
            return Ok(response);
        }

        /// <summary>
        /// Edita um usuário existente no sistema.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("editar")]
        public async Task<ActionResult> EditarAsync([FromBody] UsuarioEditarRequest request, CancellationToken cancellationToken)
        {
            var response = await usuarioAppServico.EditarAsync(request, cancellationToken);
            return Ok(response);
        }

        /// <summary>
        /// Altera o status de um usuário (ativo/inativo).
        /// </summary>
        /// <param name="id">ID do usuário</param>
        /// <param name="status">Novo status do usuário</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns></returns>
        [HttpPatch]
        [Route("alterar-status")]
        public async Task<ActionResult> AlterarStatusAsync([FromBody] AlterarStatusRequest request, CancellationToken cancellationToken)
        {
            var response = await usuarioAppServico.AlterarStatusAsync(request.Id, request.Status, cancellationToken);
            return Ok(response);
        }
    }
}
