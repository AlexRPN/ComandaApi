using Comanda.Dominio.Clientes.Servicos.Interfaces;
using Comanda.Dominio.EnderecoClientes.Comandos;
using Comanda.Dominio.EnderecoClientes.Entidades;
using Comanda.Dominio.EnderecoClientes.Repositorios.Interfaces;
using Comanda.Dominio.EnderecoClientes.Servicos.Interfaces;

namespace Comanda.Dominio.EnderecoClientes.Servicos
{
    public class EnderecoClienteServico : IEnderecoClienteServico
    {
        private readonly IEnderecoClienteRepositorio enderecoClienteRepositorio;
        private readonly IClienteServico clienteServico;
        public EnderecoClienteServico(IEnderecoClienteRepositorio enderecoClienteRepositorio,
                                      IClienteServico clienteServico)
        {
            this.enderecoClienteRepositorio = enderecoClienteRepositorio;
            this.clienteServico = clienteServico;
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

        public async Task<EnderecoCliente> EditarAsync(EnderecoClienteEditarComando comando, CancellationToken cancellationToken)
        {
            EnderecoCliente? endereco = await enderecoClienteRepositorio
                                       .RecuperarAsync(x => x.ClienteId == comando.ClienteId, cancellationToken);

            endereco.SetCep(comando.Cep);
            endereco.SetLogradouro(comando.Logradouro);
            endereco.SetNumero(comando.Numero);
            endereco.SetComplemento(comando.Complemento);
            endereco.SetBairro(comando.Bairro);
            endereco.SetCidade(comando.Cidade);
            endereco.SetEstado(comando.Estado);
            endereco.SetPontoReferencia(comando.PontoReferencia);

            return await enderecoClienteRepositorio.EditarAsync(endereco, cancellationToken);
        }
    }
}
