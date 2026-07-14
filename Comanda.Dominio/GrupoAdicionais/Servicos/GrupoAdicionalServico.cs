using Comanda.Dominio.GrupoAdicionais.Comandos;
using Comanda.Dominio.GrupoAdicionais.Entidades;
using Comanda.Dominio.GrupoAdicionais.Repositorios.Interfaces;
using Comanda.Dominio.GrupoAdicionais.Servicos.Interfaces;

namespace Comanda.Dominio.GrupoAdicionais.Servicos
{
    public class GrupoAdicionalServico : IGrupoAdicionalServico
    {
        private readonly IGrupoAdicionalRepositorio grupoAdicionalRepositorio;
        public GrupoAdicionalServico(IGrupoAdicionalRepositorio grupoAdicionalRepositorio)
        {
            this.grupoAdicionalRepositorio = grupoAdicionalRepositorio;
        }

        public async Task<List<GrupoAdicional>> InserirAsync(List<GrupoAdicionalComando> comandos, CancellationToken cancellationToken)
        {
            return await grupoAdicionalRepositorio.InserirAsync(comandos, cancellationToken);
        }

        public Task<GrupoAdicional> RecuperarAsync(int empresaId, int grupoAdicionalId, CancellationToken cancellationToken)
        {
            var grupoAdicional = grupoAdicionalRepositorio.RecuperarAsync(x => x.EmpresaId == empresaId && x.Id == grupoAdicionalId, cancellationToken);

            if (grupoAdicional is null)
            {
                throw new Exception($"Grupo adicional {grupoAdicionalId} não encontrado para esta empresa!");
            }

            return grupoAdicional;
        }
    }
}
