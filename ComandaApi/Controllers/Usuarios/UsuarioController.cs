using Comanda.Aplicacao.Usuarios.Servicos.Interfaces;
using Comanda.DataTransfer.Usuarios.Request;
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
    }
}
