using Comanda.Dominio.Empresas.Comandos;
using Comanda.Dominio.Empresas.Entidades;
using Comanda.Dominio.Empresas.Repositorios.Filtros;
using Comanda.Dominio.Empresas.Repositorios.Interfaces;
using Comanda.Dominio.Empresas.Servicos.Interfaces;
using Comanda.Dominio.Utils.Consultas;
using Comanda.Dominio.Utils.Enumeradores;
using Comanda.Dominio.Utils.Filtros.Enumeradores;

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

        

        public async Task<IQueryable<Empresa>> FiltrarAsync(EmpresaListarFiltro comando, CancellationToken cancellationToken)
        {
            return await empresaRepositorio.FiltrarAsync(comando, cancellationToken); 
        }

        public async Task<Empresa> RecuperarAsync(int id, CancellationToken cancellationToken)
        {
            var empresa = await empresaRepositorio.RecuperarAsync(id, cancellationToken);

            if (empresa == null)
                throw new Exception("Empresa não encontrada.");

            return empresa;
        }

        public async Task<PaginacaoConsulta<Empresa>> ListarAsync(IQueryable<Empresa> query, int qt, int pg, string cpOrd, 
                                                            TipoOrdenacaoEnum tpOrd, 
                                                            CancellationToken cancellationToken)
        {
            return await empresaRepositorio.ListarAsync(query, qt, pg, cpOrd, tpOrd, cancellationToken);
        }
    }
}
