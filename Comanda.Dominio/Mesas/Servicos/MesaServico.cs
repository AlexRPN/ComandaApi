
using Comanda.Dominio.Empresas.Servicos.Interfaces;
using Comanda.Dominio.Mesas.Comandos;
using Comanda.Dominio.Mesas.Entidades;
using Comanda.Dominio.Mesas.Repositorios.Interfaces;
using Comanda.Dominio.Mesas.Servicos.Interfaces;
using Comanda.Dominio.Utils.Enumeradores;

namespace Comanda.Dominio.Mesas.Servicos
{
    public class MesaServico : IMesaServico
    {
        private readonly IMesaRepositorio mesaRepositorio;
        private readonly IEmpresaServico empresaServico;
        public MesaServico(IMesaRepositorio mesaRepositorio, 
                           IEmpresaServico empresaServico)
        {
            this.mesaRepositorio = mesaRepositorio;
            this.empresaServico = empresaServico;
        }

        public async Task<Mesa> InserirAsync(MesaComando comando, CancellationToken cancellationToken)
        {
            await empresaServico.RecuperarAsync(comando.EmpresaId, cancellationToken);

            comando.DataCadastro = DateTime.Now.Date;
            comando.DataAlteracao = DateTime.Now.Date;
            comando.Situacao = SituacaoMesaEnum.Livre;
            comando.Status = StatusEnum.Ativo;

            var mesaExistente = await mesaRepositorio.RecuperarAsync(x => x.Numero == comando.Numero && 
                                                                     x.EmpresaId == comando.EmpresaId, 
                                                                     cancellationToken);

            if (mesaExistente != null)
            {
                throw new ArgumentException("Já existe uma mesa com o mesmo número para esta empresa.");
            }

            Mesa mesa = new Mesa(comando);

            var mesaInserida = await mesaRepositorio.InserirAsync(mesa, cancellationToken);

            if (mesaInserida is null)
            {
                throw new ArgumentException("Não foi possível inserir a mesa.");
            }

            return mesaInserida;
        }
    }
}
