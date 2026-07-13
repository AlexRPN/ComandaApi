using Comanda.Dominio.Arquivos.Comandos;
using Microsoft.AspNetCore.Http;

namespace Comanda.Dominio.Arquivos.Repositorios.Interfaces
{
    public interface IArquivoRepositorio
    {
        Task<ArquivoComando> UploadImagensAsync(IFormFile arquivo, CancellationToken cancellationToken);
    }
}
