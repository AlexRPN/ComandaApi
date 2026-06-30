using Comanda.Dominio.Clientes.Entidades;
using Comanda.Dominio.Empresas.Comandos;
using Comanda.Dominio.EnderecosEmpresas.Entidades;
using Comanda.Dominio.HorariosFuncionamento.Entidades;
using Comanda.Dominio.Usuarios.Entidades;
using Comanda.Dominio.Utils.Enumeradores;

namespace Comanda.Dominio.Empresas.Entidades
{
    public class Empresa
    {
        #region Navegação com os relacionamentos
        // Relacionamento 1:1 com EnderecoEmpresa
        public EnderecoEmpresa EnderecoEmpresa { get; set; }

        // Relacionamento 1:N com HorarioFuncionamento
        public ICollection<HorarioFuncionamento> HorariosFuncionamento { get; set; } = [];

        // Relacionamento 1:N com Usuario
        public ICollection<Usuario> Usuarios { get; set; } = [];

        // Relacionamento 1:N com Cliente
        public ICollection<Cliente> Clientes { get; set; } = [];
        #endregion

        public int Id { get; private set; }
        public string NomeFantasia { get; private set; }
        public string RazaoSocial { get; private set; }
        public string Cnpj { get; private set; }
        public string InscricaoEstadual { get; private set; }
        public string Telefone { get; private set; }
        public string Email { get; private set; }
        public string Logo { get; private set; }
        public string BannerPrincipal { get; private set; }
        public AtivoInativoEnum Status { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public DateTime DataAlteracao { get; private set; }

        private Empresa()
        {

        }

        public Empresa(EmpresaComando comando)
        {
            SetNomeFantasia(comando.NomeFantasia);
            SetRazaoSocial(comando.RazaoSocial);
            SetCnpj(comando.Cnpj);
            SetInscricaoEstadual(comando.InscricaoEstadual);
            SetTelefone(comando.Telefone);
            SetEmail(comando.Email);
            SetLogo(comando.Logo);
            SetBannerPrincipal(comando.BannerPrincipal);
            Status = comando.Status;
            DataCadastro = comando.DataCadastro;
            SetDataAlteracao(comando.DataAlteracao);
        }

        public void SetNomeFantasia(string nomeFantasia)
        {
            NomeFantasia = nomeFantasia;
        }

        public void SetRazaoSocial(string razaoSocial)
        {
            RazaoSocial = razaoSocial;
        }

        public void SetCnpj(string cnpj)
        {
            if (string.IsNullOrEmpty(cnpj))
            {
                throw new ArgumentException("CNPJ não pode ser nulo ou vazio.");
            }

            if (cnpj.Length != 14)
            {
                throw new ArgumentException("CNPJ deve conter 14 caracteres.");
            }

            Cnpj = cnpj;
        }

        public void SetInscricaoEstadual(string inscricaoEstadual)
        {
            InscricaoEstadual = inscricaoEstadual;
        }

        public void SetTelefone(string telefone)
        {
            if (string.IsNullOrEmpty(telefone))
            {
                throw new ArgumentException("Telefone não pode ser nulo ou vazio.");
            }

            if (telefone.Length < 10 || telefone.Length > 11)
            {
                throw new ArgumentException("Telefone deve conter entre 10 e 11 caracteres.");
            }

            Telefone = telefone;
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

        public void SetLogo(string logo)
        {
            Logo = logo;
        }

        public void SetBannerPrincipal(string bannerPrincipal)
        {
            BannerPrincipal = bannerPrincipal;
        }

        public void SetDataAlteracao(DateTime dataAlteracao)
        {
            DataAlteracao = dataAlteracao;
        }
    }
}
