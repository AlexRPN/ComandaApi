using Comanda.Dominio.Empresas.Comandos;
using Comanda.Dominio.Empresas.Repositorios.Interfaces;
using Comanda.Dominio.Empresas.Servicos.Interfaces;
using Comanda.Dominio.Utils.Enumeradores;

namespace Comanda.Dominio.Empresas.Servicos
{
    public class EmpresaServico : IEmpresaServico
    {
        private readonly IEmpresaRepositorio empresaRepositorio;
        public EmpresaServico(IEmpresaRepositorio empresaRepositorio)
        {
            this.empresaRepositorio = empresaRepositorio;
        }

        public async Task<EmpresaComando> InserirAsync(EmpresaInserirComando comando, CancellationToken cancellationToken)
        {
            var empresa = new EmpresaComando
            {
                NomeFantasia = comando.NomeFantasia,
                RazaoSocial = comando.RazaoSocial,
                Cnpj = comando.Cnpj,
                InscricaoEstadual = comando.InscricaoEstadual,
                Telefone = comando.Telefone,
                Email = comando.Email,
                Logo = comando.Logo,
                BannerPrincipal = comando.BannerPrincipal,
                Status = AtivoInativoEnum.Ativo,
                DataCadastro = DateTime.UtcNow
            };

            return await empresaRepositorio.InserirAsync(empresa, cancellationToken);
        }
    }
}
