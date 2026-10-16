using Comanda.Dominio.Usuarios.Comandos;
using Comanda.Dominio.Usuarios.Entidades;
using Comanda.Dominio.Usuarios.Repositorios.Filtros;
using Comanda.Dominio.Usuarios.Repositorios.Interfaces;
using Comanda.Dominio.Usuarios.Servicos.Interfaces;
using Comanda.Dominio.Utils.Consultas;
using Comanda.Dominio.Utils.Enumeradores;
using Comanda.Dominio.Utils.Filtros.Enumeradores;

namespace Comanda.Dominio.Usuarios.Servicos
{
    public class UsuarioServico : IUsuarioServico
    {
        private readonly IUsuarioRepositorio usuarioRepositorio;
        public UsuarioServico(IUsuarioRepositorio usuarioRepositorio)
        {
            this.usuarioRepositorio = usuarioRepositorio;
        }

        public async Task<Usuario> InserirAsync(UsuarioInserirComando comando, CancellationToken cancellationToken)
        {
            await ValidarCpfAsync(comando.Cpf, cancellationToken);

            //autenticacaoServico.CriarSenhaHash(comando.Senha, out byte[] senhaHash, out byte[] senhaSalt);

            var usuario = new UsuarioComando
            {
                EmpresaId = comando.EmpresaId,
                Nome = comando.Nome,
                Cpf = comando.Cpf,
                Email = comando.Email,
                Perfil = comando.Perfil,
                Status = StatusEnum.Ativo,
                DataCadastro = DateTime.Now,
            };
            return await usuarioRepositorio.InserirAsync(usuario, cancellationToken);
        }

        public Task<IQueryable<Usuario>> FiltrarAsync(UsuarioListarFiltro comando, CancellationToken cancellationToken)
        {
            return usuarioRepositorio.FiltrarAsync(comando, cancellationToken);
        }

        public Task<PaginacaoConsulta<Usuario>> ListarPaginadoAsync(IQueryable<Usuario> query, int qt, int pg, string cpOrd, TipoOrdenacaoEnum tpOrd, CancellationToken cancellationToken)
        {
            return usuarioRepositorio.ListarPaginadoAsync(query, qt, pg, cpOrd, tpOrd, cancellationToken);
        }

        public async Task<Usuario> RecuperarPorIdAsync(int id, CancellationToken cancellationToken)
        {
            var usuario = await usuarioRepositorio.RecuperarPorIdAsync(id, cancellationToken);

            if (usuario == null)
            {
                throw new ArgumentException("Usuário não encontrado!");
            }

            return usuario;
        }

        public async Task<Usuario> ValidarCpfAsync(string cpf, CancellationToken cancellationToken)
        {
            var usuario = await usuarioRepositorio.ValidarCpfAsync(cpf, cancellationToken);

            if (usuario != null)
            {
                throw new ArgumentException("Cpf informado já está cadastrado no sistema!");
            }

            return usuario;
        }

        public async Task<Usuario> EditarAsync(UsuarioEditarComando comando, CancellationToken cancellationToken)
        {
            Usuario usuario = await usuarioRepositorio.RecuperarAsync(comando.Id, cancellationToken);

            if (usuario == null)
            {
                throw new Exception("Usuário não encontrado!");
            }

            usuario.SetEmpresaId(comando.EmpresaId);
            usuario.SetNome(comando.Nome);
            usuario.SetCpf(comando.Cpf);
            usuario.SetEmail(comando.Email);
            usuario.SetPerfil(comando.Perfil);

            await usuarioRepositorio.EditarAsync(usuario, cancellationToken);

            return usuario;
        }

        public async Task<Usuario> AlterarStatusAsync(int id, StatusEnum status, CancellationToken cancellationToken)
        {
            Usuario usuario = await usuarioRepositorio.RecuperarPorIdAsync(id, cancellationToken);

            if (usuario == null)
            {
                throw new ArgumentException("Usuário não encontrado!");
            }

            usuario.SetStatus(status);

            await usuarioRepositorio.EditarAsync(usuario, cancellationToken);

            return usuario;
        }
    }
}
