using Comanda.Dominio.EnderecoClientes.Comandos;
using Comanda.Dominio.EnderecoClientes.Repositorios.Interfaces;
using Comanda.Dominio.EnderecoClientes.Servicos.Interfaces;

namespace Comanda.Dominio.EnderecoClientes.Servicos
{
    public class EnderecoClienteServico : IEnderecoClienteServico
    {
        private readonly IEnderecoClienteRepositorio enderecoClienteRepositorio;
        public EnderecoClienteServico(IEnderecoClienteRepositorio enderecoClienteRepositorio)
        {
            this.enderecoClienteRepositorio = enderecoClienteRepositorio;
        }

        public async Task<EnderecoClienteComando> InserirAsync(EnderecoClienteComando comando, CancellationToken cancellationToken)
        {
            if (comando == null)
            {
                throw new Exception("O endereço do cliente precisa estar preenchido!");
            }

            var enderecoCliente = new EnderecoClienteComando
            {
                ClienteId = comando.ClienteId,
                Cep = comando.Cep,
                Logradouro = comando.Logradouro,
                Numero = comando.Numero,
                Complemento = comando.Complemento,
                Bairro = comando.Bairro,
                Cidade = comando.Cidade,
                Estado = comando.Estado,
                PontoReferencia = comando.PontoReferencia
            };

            return await enderecoClienteRepositorio.InserirAsync(enderecoCliente, cancellationToken);
        }
    }
}
