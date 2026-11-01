
using Comanda.Dominio.ProdutosGruposAdicionais.Comandos;
using Comanda.Dominio.ProdutosGruposAdicionais.Entidades;
using FizzWare.NBuilder;

namespace Comanda.Dominio.Testes.ProdutosGruposAdicionais.Entidades
{
    public class ProdutosGruposAdicionaisTestes
    {
        private readonly ProdutoGrupoAdicional sut;
        public ProdutosGruposAdicionaisTestes()
        {
            sut = Builder<ProdutoGrupoAdicional>.CreateNew().Build();
        }

        public class Construtor
        {
            [Fact]
            public void Quando_ConstrutorForChamado_Espero_PropriedadesInicializadas()
            {
                var comando = new ProdutoGrupoAdicionalComando
                {
                    ProdutoId = 1,
                    GrupoAdicionalId = 1
                };

                var produtoGrupoAdicional = new ProdutoGrupoAdicional(comando);

                Assert.Equal(produtoGrupoAdicional.ProdutoId, comando.ProdutoId);
                Assert.Equal(produtoGrupoAdicional.GrupoAdicionalId, comando.GrupoAdicionalId);
            }
        }

        public class SetProdutoId : ProdutosGruposAdicionaisTestes
        {
            [Fact]
            public void Quando_ProdutoIdForValido_Espero_PropriedadeAtualizada()
            {
                var produtoId = sut.ProdutoId;

                sut.SetProdutoId(produtoId);

                Assert.Equal(produtoId, sut.ProdutoId);
            }
        }

        public class SetGrupoAdicionalId : ProdutosGruposAdicionaisTestes
        {
            [Fact]
            public void Quando_GrupoAdicionalIdForValido_Espero_PropriedadeAtualizada()
            {
                var grupoAdicionalId = sut.GrupoAdicionalId;

                sut.SetGrupoAdicionalId(grupoAdicionalId);

                Assert.Equal(grupoAdicionalId, sut.GrupoAdicionalId);
            }
        }
    }
}
