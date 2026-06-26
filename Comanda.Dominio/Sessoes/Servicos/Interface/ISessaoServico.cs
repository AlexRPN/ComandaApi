using Comanda.Dominio.Usuarios.Entidades;

namespace Comanda.Dominio.Sessoes.Servicos.Interface
{
    public interface ISessaoServico
    {
        Usuario BuscarSessao();
        void CriarSessao(Usuario usuario);
        void RemoverSessao();
    }
}
