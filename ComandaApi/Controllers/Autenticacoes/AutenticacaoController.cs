using Comanda.Aplicacao.UsuariosCredenciais.Servicos.Interfaces;
using Comanda.DataTransfer.UsuariosCredenciais.Request;
using Microsoft.AspNetCore.Mvc;

namespace ComandaApi.Controllers.Login
{
    [ApiController]
    [Route("api/autenticacao")]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IUsuarioCredencialAppServico usuarioCredencialAppServico;
        public AutenticacaoController(IUsuarioCredencialAppServico usuarioCredencialAppServico)
        {
            this.usuarioCredencialAppServico = usuarioCredencialAppServico;
        }

        /// <summary>
        /// Registra um novo usuário com base nas informações fornecidas no request.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("registrar")]
        public async Task<ActionResult> RegistrarAsync([FromBody] UsuarioCredencialRegistroRequest request, 
                                                                  CancellationToken cancellationToken)
        {
            var response = await usuarioCredencialAppServico.RegistrarAsync(request, cancellationToken);
            return Ok(response);
        }

        /// <summary>
        /// Realiza o login do usuário com base nas credenciais fornecidas.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> LoginAsync([FromBody] UsuarioCredencialLoginRequest request, CancellationToken cancellationToken)
        {
            var response = await usuarioCredencialAppServico.LoginAsync(request, cancellationToken);
            return Ok(response);
        }
    }
}
