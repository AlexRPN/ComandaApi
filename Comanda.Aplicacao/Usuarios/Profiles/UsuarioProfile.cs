
using Comanda.DataTransfer.Empresas.Response;
using Comanda.DataTransfer.Usuarios.Request;
using Comanda.DataTransfer.Usuarios.Response;
using Comanda.Dominio.Usuarios.Comandos;
using Comanda.Dominio.Usuarios.Entidades;
using Mapster;

namespace Comanda.Aplicacao.Usuarios.Profiles
{
    public class UsuarioProfile : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<UsuarioRequest, UsuarioInserirComando>();
            config.NewConfig<Usuario,  UsuarioResponse>()
                .Map(dest => dest.Empresa, src => src.Empresa);
            config.NewConfig<Usuario, EmpresaResponse>()
                .Map(dest => dest.Endereco, src => src.Empresa.EnderecoEmpresa)
                .Map(dest => dest.HorariosFuncionamento, src => src.Empresa.HorariosFuncionamento);
        }
    }
}
