
using Comanda.Dominio.Empresas.Entidades;
using Comanda.Dominio.EnderecosEmpresas.Comandos;

namespace Comanda.Dominio.EnderecosEmpresas.Entidades
{
    public class EnderecoEmpresa
    {

        #region Navegação com os relacionamentos
        // Relacionamento 1:1 com Empresa
        public int EmpresaId { get; private set; }
        public Empresa Empresa { get; set; }
        #endregion

        public int Id { get; private set; }
        public string Cep { get; private set; }
        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Bairro { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }
        public string Pais { get; private set; }
        public decimal Latitude { get; private set; }
        public decimal Longitude { get; private set; }

        private EnderecoEmpresa()
        {

        }

        public EnderecoEmpresa(EnderecoEmpresaComando comando)
        {
            SetEmpresaId(comando.EmpresaId);
            SetCep(comando.Cep);
            SetLogradouro(comando.Logradouro);
            SetNumero(comando.Numero);
            SetComplemento(comando.Complemento);
            SetBairro(comando.Bairro);
            SetCidade(comando.Cidade);
            SetEstado(comando.Estado);
            SetPais(comando.Pais);
            SetLatitude(comando.Latitude);
            SetLongitude(comando.Longitude);
        }

        public void SetEmpresaId(int empresaId)
        {
            EmpresaId = empresaId;
        }

        public void SetCep(string cep)
        {
            if (string.IsNullOrEmpty(cep))
            {
                throw new ArgumentException("O CEP não pode ser nulo ou vazio.");
            }

            if (cep.Length > 9 || cep.Length < 2)
            {
                throw new ArgumentException("O CEP deve conter entre 2 e 9 caracteres.");
            }

            Cep = cep;
        }

        public void SetLogradouro(string logradouro)
        {
            Logradouro = logradouro;
        }

        public void SetNumero(string numero)
        {
            Numero = numero;
        }

        public void SetComplemento(string complemento)
        {
            Complemento = complemento;
        }

        public void SetBairro(string bairro)
        {
            Bairro = bairro;
        }

        public void SetCidade(string cidade)
        {
            Cidade = cidade;
        }

        public void SetEstado(string estado)
        {
            Estado = estado;
        }

        public void SetPais(string pais)
        {
            Pais = pais;
        }

        public void SetLatitude(decimal latitude)
        {
            Latitude = latitude;
        }

        public void SetLongitude(decimal longitude)
        {
            Longitude = longitude;
        }
    }
}
