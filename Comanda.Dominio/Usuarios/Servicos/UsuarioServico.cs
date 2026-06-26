using Comanda.Dominio.Usuarios.Comandos;
using Comanda.Dominio.Usuarios.Entidades;
using Comanda.Dominio.Usuarios.Repositorios.Interfaces;
using Comanda.Dominio.Usuarios.Servicos.Interfaces;
using Comanda.Dominio.Utils.Autenticacoes.Servicos.Interfaces;
using Comanda.Dominio.Utils.Enumeradores;

namespace Comanda.Dominio.Usuarios.Servicos
{
    public class UsuarioServico : IUsuarioServico
    {
        private readonly IUsuarioRepositorio usuarioRepositorio;
        private readonly IAutenticacaoServico autenticacaoServico;
        public UsuarioServico(IUsuarioRepositorio usuarioRepositorio, 
                              IAutenticacaoServico autenticacaoServico)
        {
            this.usuarioRepositorio = usuarioRepositorio;
            this.autenticacaoServico = autenticacaoServico;
        }

        public async Task<Usuario> InserirAsync(UsuarioInserirComando comando, CancellationToken cancellationToken)
        {
            await ValidarCpfAsync(comando.Cpf, cancellationToken);

            autenticacaoServico.CriarSenhaHash(comando.Senha, out byte[] senhaHash, out byte[] senhaSalt);

            var usuario = new UsuarioComando
            {
                EmpresaId = comando.EmpresaId,
                Nome = comando.Nome,
                Cpf = comando.Cpf,
                Email = comando.Email,
                SenhaHash = senhaHash,
                SenhaSalt = senhaSalt,
                Perfil = comando.Perfil,
                Status = AtivoInativoEnum.Ativo,
                DataCadastro = DateTime.UtcNow,
            };
            return await usuarioRepositorio.InserirAsync(usuario, cancellationToken);
        }

        public async Task<Usuario> ValidarCpfAsync(string cpf, CancellationToken cancellationToken)
        {
            var usuario = await usuarioRepositorio.ValidarCpfAsync(cpf, cancellationToken);

            if(usuario != null)
            {
                throw new Exception("Cpf informado já está cadastrado no sistema!");
            }

            return usuario;
        }
    }
}
