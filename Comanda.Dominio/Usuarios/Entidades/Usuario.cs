using Comanda.Dominio.Empresas.Entidades;
using Comanda.Dominio.Usuarios.Comandos;
using Comanda.Dominio.UsuariosCredenciais.Entidades;
using Comanda.Dominio.Utils.Enumeradores;

namespace Comanda.Dominio.Usuarios.Entidades
{
    public class Usuario
    {
        #region Navegação com os relacionamentos
        // Relacionamento 1:N com Empresa
        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }

        // Relacionamento 1:1 com UsuarioCredencial
        public UsuarioCredencial UsuarioCredencial { get; set; }
        #endregion

        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Cpf { get; private set; }
        public string Email { get; private set; }
        public PerfilEnum Perfil { get; private set; }
        public StatusEnum Status { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public DateTime? DataAlteracao { get; private set; }

        private Usuario()
        {

        }

        public Usuario(UsuarioComando comando)
        {
            SetNome(comando.Nome);
            SetEmpresaId(comando.EmpresaId);
            SetCpf(comando.Cpf);
            SetEmail(comando.Email);
            SetPerfil(comando.Perfil);
            SetStatus(comando.Status);
            SetDataCadastro(comando.DataCadastro);
            SetDataAlteracao(comando.DataAlteracao);
        }

        public void SetStatus(StatusEnum status)
        {
            Status = status;
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
            if (string.IsNullOrWhiteSpace(nome))
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

        public void SetPerfil(PerfilEnum perfil)
        {
            Perfil = perfil;
        }

        public void SetDataAlteracao(DateTime dataAlteracao)
        {
            DataAlteracao = dataAlteracao;
        }

        public void SetDataCadastro(DateTime dataCadastro)
        {
            DataCadastro = dataCadastro;
        }
    }
}
