
using Comanda.Dominio.EnderecosEmpresas.Comandos;
using Comanda.Dominio.EnderecosEmpresas.Entidades;
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

        public async Task<EnderecoEmpresa> EditarAsync(EnderecoEmpresaEditarComando comando, CancellationToken cancellationToken)
        {
            EnderecoEmpresa enderecoEmpresa = await enderecoEmpresaRepositorio.RecuperarAsync(e => e.EmpresaId == comando.EmpresaId, cancellationToken);

            enderecoEmpresa.SetCep(comando.Cep);
            enderecoEmpresa.SetLogradouro(comando.Logradouro);
            enderecoEmpresa.SetNumero(comando.Numero);
            enderecoEmpresa.SetComplemento(comando.Complemento);
            enderecoEmpresa.SetBairro(comando.Bairro);
            enderecoEmpresa.SetCidade(comando.Cidade);
            enderecoEmpresa.SetEstado(comando.Estado);
            enderecoEmpresa.SetPais(comando.Pais);
            enderecoEmpresa.SetLatitude(comando.Latitude);
            enderecoEmpresa.SetLongitude(comando.Longitude);

            await enderecoEmpresaRepositorio.EditarAsync(enderecoEmpresa, cancellationToken);

            return enderecoEmpresa;
        }

        public Task<EnderecoEmpresa> ValidarAsync(int id, CancellationToken cancellationToken)
        {
            var enderecoEmpresa = enderecoEmpresaRepositorio.RecuperarAsync(id, cancellationToken);

            return enderecoEmpresa;
        }
    }
}
