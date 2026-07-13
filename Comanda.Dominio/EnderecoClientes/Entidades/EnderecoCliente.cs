using Comanda.Dominio.Clientes.Entidades;
using Comanda.Dominio.EnderecoClientes.Comandos;

namespace Comanda.Dominio.EnderecoClientes.Entidades
{
    public class EnderecoCliente
    {
        #region Navegação com os relacionamentos
        // Relacionamento 1:1 com Cliente
        public Cliente Cliente { get; private set; }
        public int ClienteId { get; private set; }
        #endregion

        public int Id { get; private set; }
        public string Cep { get; private set; }
        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Bairro { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }
        public string PontoReferencia { get; private set; }

        private EnderecoCliente()
        {

        }

        public EnderecoCliente(EnderecoClienteComando comando)
        {
            SetClienteId(comando.ClienteId);
            SetCep(comando.Cep);
            SetLogradouro(comando.Logradouro);
            SetNumero(comando.Numero);
            SetComplemento(comando.Complemento);
            SetBairro(comando.Bairro);
            SetCidade(comando.Cidade);
            SetEstado(comando.Estado);
            SetPontoReferencia(comando.PontoReferencia);
        }

        public void SetClienteId(int clienteId)
        {
            ClienteId = clienteId;
        }

        public void SetCep(string cep)
        {
            if (string.IsNullOrWhiteSpace(cep))
            {
                throw new ArgumentException("O CEP do endereço do cliente não pode ser nulo ou vazio.");
            }

            if (cep.Length > 9 || cep.Length < 2)
            {
                throw new ArgumentException("O CEP deve conter entre 2 e 9 caracteres.");
            }

            Cep = cep;
        }

        public void SetLogradouro(string logradouro)
        {
            if (string.IsNullOrWhiteSpace(logradouro))
            {
                throw new ArgumentException("O logradouro do endereço do cliente não pode ser nulo ou vazio.");
            }

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
            if (string.IsNullOrWhiteSpace(bairro))
            {
                throw new ArgumentException("O bairro do endereço do cliente não pode ser nulo ou vazio.");
            }

            Bairro = bairro;
        }

        public void SetCidade(string cidade)
        {
            if (string.IsNullOrWhiteSpace(cidade))
            {
                throw new ArgumentException("A cidade do endereço do cliente não pode ser nula ou vazia.");
            }

            Cidade = cidade;
        }

        public void SetEstado(string estado)
        {
            if (string.IsNullOrWhiteSpace(estado))
            {
                throw new ArgumentException("O estado do endereço do cliente não pode ser nulo ou vazio.");
            }

            Estado = estado;
        }

        public void SetPontoReferencia(string pontoReferencia)
        {
            PontoReferencia = pontoReferencia;
        }
    }
}
