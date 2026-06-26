using Comanda.Dominio.Empresas.Entidades;
using Comanda.Dominio.Usuarios.Comandos;
using Comanda.Dominio.Utils.Enumeradores;

namespace Comanda.Dominio.Usuarios.Entidades
{
    public class Usuario
    {
        #region Navegação com os relacionamentos
        // Relacionamento 1:N com Empresa
        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }
        #endregion

        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Cpf { get; private set; }
        public string Email { get; private set; }
        public byte[] SenhaHash { get; private set; }
        public byte[] SenhaSalt { get; private set; }
        public PerfilEnum Perfil { get; private set; }
        public AtivoInativoEnum Status { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public DateTime UltimoAcesso { get; private set; }

        private Usuario()
        {
            
        }

        public Usuario(UsuarioComando comando)
        {
            SetNome(comando.Nome);
            SetEmpresaId(comando.EmpresaId);
            SetCpf(comando.Cpf);
            SetEmail(comando.Email);
            SetSenhaHash(comando.SenhaHash);
            SetSenhaSalt(comando.SenhaSalt);
            SetPerfil(comando.Perfil);
            Status = comando.Status;
            DataCadastro = comando.DataCadastro;
            SetUltimoAcesso(comando.UltimoAcesso);
        }

        public void SetEmpresaId(int empresaId)
        {
            EmpresaId = empresaId;
        }

        public void SetCpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
            {
                throw new ArgumentException("O CPF do usuário não pode ser vazio.");
            }

            Cpf = cpf;
        }

        public void SetNome(string nome)
        {
            if(string.IsNullOrWhiteSpace(nome))
            {
                throw new ArgumentException("O nome do usuário não pode ser vazio.");
            }

            Nome = nome;
        }

        public void SetEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Email não pode ser nulo ou vazio.");
            }

            if (!email.Contains("@") || !email.Contains("."))
            {
                throw new ArgumentException("Email deve conter '@' e '.'");
            }

            Email = email;
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

        public void SetPerfil(PerfilEnum perfil)
        {
            Perfil = perfil;
        }

        public void SetUltimoAcesso(DateTime ultimoAcesso)
        {
            UltimoAcesso = ultimoAcesso;
        }
    }
}
