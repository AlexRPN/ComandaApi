using Comanda.Dominio.Categorias.Comandos;
using Comanda.Dominio.Categorias.Entidades;
using Comanda.Dominio.Categorias.Repositorios.Interfaces;
using Comanda.Dominio.Categorias.Servicos.Interfaces;
using Comanda.Dominio.Empresas.Repositorios.Interfaces;
using Comanda.Dominio.Utils.Enumeradores;

namespace Comanda.Dominio.Categorias.Servicos
{
    public class CategoriaServico : ICategoriaServico
    {
        private readonly ICategoriaRepositorio categoriaRepositorio;
        private readonly IEmpresaRepositorio empresaRepositorio;
        public CategoriaServico(ICategoriaRepositorio categoriaRepositorio, 
                                IEmpresaRepositorio empresaRepositorio)
        {
            this.categoriaRepositorio = categoriaRepositorio;
            this.empresaRepositorio = empresaRepositorio;
        }

        public async Task<Categoria> InserirAsync(CategoriaComando comando, CancellationToken cancellationToken)
        {
            var empresaValida = await empresaRepositorio.RecuperarAsync(comando.EmpresaId, cancellationToken);

            var categoria = new CategoriaComando
            {
                EmpresaId = empresaValida.Id,
                Nome = comando.Nome,
                Descricao = comando.Descricao,
                Status = AtivoInativoEnum.Ativo,
                DataCadastro = DateTime.UtcNow,
                DataAlteracao = DateTime.UtcNow
            };

            return await categoriaRepositorio.InserirAsync(categoria, cancellationToken);
        }

        public async Task<Categoria> EditarAsync(CategoriaEditarComando comando, CancellationToken cancellationToken)
        {
            Categoria? categoria = await categoriaRepositorio.RecuperarAsync(x => x.Id == comando.Id && 
                                                                             x.EmpresaId == comando.EmpresaId, cancellationToken);

            if(categoria == null)
            {
                throw new ArgumentNullException("Categoria não encontrada!");
            }

            categoria.SetNome(comando.Nome);
            categoria.SetDescricao(comando.Descricao);
            categoria.SetDataAlteracao(DateTime.UtcNow);

            return await categoriaRepositorio.EditarAsync(categoria, cancellationToken);
        }

        public async Task<Categoria> RecuperarPorIdAsync(int id, CancellationToken cancellationToken)
        {
            Categoria? categoria = await categoriaRepositorio.RecuperarAsync(x => x.Id == id, cancellationToken);

            if(categoria == null)
            {
                throw new ArgumentNullException("Categoria não encontrada!");
            }
            
            return categoria;
        }
    }
}
