
using Comanda.Dominio.Usuarios.Entidades;
using Comanda.Dominio.UsuariosCredenciais.Comandos;

namespace Comanda.Dominio.UsuariosCredenciais.Entidades
{
    public class UsuarioCredencial
    {
        #region Navegação com os relacionamentos
        // Relacionamento 1:1 com Usuario
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        #endregion

        public int Id { get; private set; }
        public string NomeUsuario { get; private set; }
        public byte[] SenhaHash { get; private set; }
        public byte[] SenhaSalt { get; private set; }
        public DateTime DataAlteracaoSenha { get; private set; }
        public DateTime UltimoAcesso { get; private set; }
        public DateTime? DataFimBloqueio { get; private set; }

        private UsuarioCredencial() { }

        public UsuarioCredencial(UsuarioCredencialComando comando)
        {
            SetUsuarioId(comando.UsuarioId);
            SetNomeUsuario(comando.NomeUsuario);
            SetSenhaHash(comando.SenhaHash);
            SetSenhaSalt(comando.SenhaSalt);
            SetDataAlteracaoSenha(comando.DataAlteracaoSenha);
            SetUltimoAcesso(comando.UltimoAcesso);
            SetDataFimBloqueio(comando.DataFimBloqueio);
        }

        public void SetUsuarioId(int usuarioId)
        {
            UsuarioId = usuarioId;
        }

        public void SetNomeUsuario(string nomeUsuario)
        {
            if(string.IsNullOrWhiteSpace(nomeUsuario))
            {
                throw new ArgumentException("Nome de usuário deve ser informado!");
            }

            NomeUsuario = nomeUsuario;
        }

        public void SetSenhaHash(byte[] senhaHash)
        {
            if (senhaHash == null || senhaHash.Length == 0)
            {
                throw new ArgumentException("SenhaHash não pode ser nulo ou vazio.");
            }

            SenhaHash = senhaHash;
        }

        public void SetSenhaSalt(byte[] senhaSalt)
        {
            if (senhaSalt == null || senhaSalt.Length == 0)
            {
                throw new ArgumentException("SenhaSalt não pode ser nulo ou vazio.");
            }

            SenhaSalt = senhaSalt;
        }

        public void SetDataAlteracaoSenha(DateTime dataAlteracaoSenha)
        {
            DataAlteracaoSenha = dataAlteracaoSenha;
        }

        public void SetUltimoAcesso(DateTime ultimoAcesso)
        {
            UltimoAcesso = ultimoAcesso;
        }

        public void SetDataFimBloqueio(DateTime? dataFimBloqueio)
        {
            if(dataFimBloqueio.HasValue && dataFimBloqueio.Value < DateTime.Now)
            {
                throw new ArgumentException("Data de fim de bloqueio não pode ser anterior à data atual!");
            }

            DataFimBloqueio = dataFimBloqueio;
        }
    }
}
