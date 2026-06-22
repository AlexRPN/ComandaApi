
using Comanda.Dominio.EnderecosEmpresas.Comandos;
using Comanda.Dominio.EnderecosEmpresas.Repositorios.Interfaces;
using Comanda.Dominio.EnderecosEmpresas.Servicos.Interfaces;

namespace Comanda.Dominio.EnderecosEmpresas.Servicos
{
    public class EnderecoEmpresaServico : IEnderecoEmpresaServico
    {
        private readonly IEnderecoEmpresaRepositorio enderecoEmpresaRepositorio;
        public EnderecoEmpresaServico(IEnderecoEmpresaRepositorio enderecoEmpresaRepositorio)
        {
            this.enderecoEmpresaRepositorio = enderecoEmpresaRepositorio;
        }

        public Task<EnderecoEmpresaComando> InserirAsync(EnderecoEmpresaInserirComando comando, CancellationToken cancellationToken)
        {
            var endereco = new EnderecoEmpresaComando
            {
                EmpresaId = comando.EmpresaId,
                Cep = comando.Cep,
                Logradouro = comando.Logradouro,
                Numero = comando.Numero,
                Complemento = comando.Complemento,
                Bairro = comando.Bairro,
                Cidade = comando.Cidade,
                Estado = comando.Estado,
                Pais = comando.Pais,
                Latitude = comando.Latitude,
                Longitude = comando.Longitude
            };

            return enderecoEmpresaRepositorio.InserirAsync(endereco, cancellationToken);
        }
    }
}
