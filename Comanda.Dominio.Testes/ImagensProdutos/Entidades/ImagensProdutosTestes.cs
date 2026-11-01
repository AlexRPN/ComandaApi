
using Comanda.Dominio.ImagensProdutos.Comandos;
using Comanda.Dominio.ImagensProdutos.Entidades;
using FizzWare.NBuilder;

namespace Comanda.Dominio.Testes.ImagensProdutos.Entidades
{
    public class ImagensProdutosTestes
    {
        private readonly ImagemProduto sut;
        public ImagensProdutosTestes()
        {
            sut = Builder<ImagemProduto>.CreateNew().Build();
        }

        public class Construtor
        {
            [Fact]
            public void Quando_ConstrutorForChamado_Espero_PropriedadesInicializadas()
            {
                var comando = new ImagemProdutoComando
                {
                    Id = 1,
                    ProdutoId = 1,
                    CaminhoArquivo = "/imagens/imagem_teste.jpg",
                    UrlImagem = "http://localhost:5000/imagens/imagem_teste.jpg",
                    DataCadastro = DateTime.Now.Date
                };

                var imagemProduto = new ImagemProduto(comando);

                Assert.Equal(comando.ProdutoId, imagemProduto.ProdutoId);
                Assert.Equal(comando.UrlImagem, imagemProduto.UrlImagem);
                Assert.Equal(comando.CaminhoArquivo, imagemProduto.CaminhoArquivo);
                Assert.Equal(comando.DataCadastro, imagemProduto.DataCadastro);
            }
        }

        public class SetProdutoId : ImagensProdutosTestes
        {
            [Fact]
            public void Quando_ProdutoIdForValido_Espero_PropriedadeAtualizada()
            {
                var produtoId = 2;

                sut.SetProdutoId(produtoId);

                Assert.Equal(produtoId, sut.ProdutoId);
            }
        }

        public class SetCaminhoArquivo : ImagensProdutosTestes
        {
            [Fact]
            public void Quando_CaminhoArquivoForValido_Espero_PropriedadeAtualizada()
            {
                var caminhoArquivo = "/imagens/imagem_teste_atualizada.jpg";

                sut.SetCaminhoArquivo(caminhoArquivo);

                Assert.Equal(caminhoArquivo, sut.CaminhoArquivo);
            }
        }

        public class SetUrlImagem : ImagensProdutosTestes
        {
            [Fact]
            public void Quando_UrlImagemForValido_Espero_PropriedadeAtualizada()
            {
                var urlImagem = "http://localhost:5000/imagens/imagem_teste_atualizada.jpg";

                sut.SetUrlImagem(urlImagem);

                Assert.Equal(urlImagem, sut.UrlImagem);
            }
        }

        public class SetDataCadastro : ImagensProdutosTestes
        {
            [Fact]
            public void Quando_DataCadastroForValido_Espero_PropriedadeAtualizada()
            {
                var dataCadastro = DateTime.Now.Date;

                sut.SetDataCadastro(dataCadastro);

                Assert.Equal(dataCadastro, sut.DataCadastro);
            }
        }

        public class SetDataAlteracao : ImagensProdutosTestes
        {
            [Fact]
            public void Quando_DataAlteracaoForValido_Espero_PropriedadeAtualizada()
            {
                var dataAlteracao = DateTime.Now.Date;

                sut.SetDataAlteracao(dataAlteracao);

                Assert.Equal(dataAlteracao, sut.DataAlteracao);
            }
        }
    }
}
