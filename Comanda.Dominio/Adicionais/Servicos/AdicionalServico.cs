using Comanda.Dominio.Adicionais.Comandos;
using Comanda.Dominio.Adicionais.Entidades;
using Comanda.Dominio.Adicionais.Repositorios.Interfaces;
using Comanda.Dominio.Adicionais.Servicos.Interfaces;
using Comanda.Dominio.Utils.Enumeradores;

namespace Comanda.Dominio.Adicionais.Servicos
{
    public class AdicionalServico : IAdicionalServico
    {
        private readonly IAdicionalRepositorio adicionalRepositorio;
        public AdicionalServico(IAdicionalRepositorio adicionalRepositorio)
        {
            this.adicionalRepositorio = adicionalRepositorio;
        }

        public Task<List<Adicional>> InserirAsync(List<AdicionalComando> comando, CancellationToken cancellationToken)
        {
            var adicionais = new List<AdicionalComando>();

            foreach (var item in comando)
            {
                adicionais.Add(new AdicionalComando
                {
                    GrupoAdicionalId = item.GrupoAdicionalId,
                    Nome = item.Nome,
                    Valor = item.Valor,
                    Status = AtivoInativoEnum.Ativo,
                    DataCadastro = DateTime.UtcNow,
                    DataAlteracao = DateTime.UtcNow
                });
            }

            return adicionalRepositorio.InserirAsync(adicionais, cancellationToken);
        }
    }
}
