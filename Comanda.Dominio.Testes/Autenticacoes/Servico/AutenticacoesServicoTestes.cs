
using Comanda.Dominio.Usuarios.Comandos;
using Comanda.Dominio.Usuarios.Entidades;
using Comanda.Dominio.UsuariosCredenciais.Comandos;
using Comanda.Dominio.UsuariosCredenciais.Entidades;
using Comanda.Dominio.Utils.Autenticacoes.Servicos;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NSubstitute;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Comanda.Dominio.Testes.Autenticacoes.Servico
{
    public class AutenticacoesServicoTestes
    {
        private readonly AutenticacaoServico sut;
        private readonly IConfiguration configuration;

        public AutenticacoesServicoTestes()
        {
            configuration = Substitute.For<IConfiguration>();
            sut = new AutenticacaoServico(configuration);
        }

        [Fact]
        public void Quando_CriarSenhaHash_Espero_HashESaltGerados()
        {
            byte[] senhaHash;
            byte[] senhaSalt;

            sut.CriarSenhaHash("senha_teste", out senhaHash, out senhaSalt);

            Assert.Equal(64, senhaHash.Length);
            Assert.NotEmpty(senhaSalt);
        }

        [Fact]
        public void Quando_VerificarLoginComSenhaCorreta_Espero_RetornarVerdadeiro()
        {
            byte[] senhaHash;
            byte[] senhaSalt;

            sut.CriarSenhaHash("senha_teste", out senhaHash, out senhaSalt);

            var resultado = sut.VerificarLogin("senha_teste", senhaHash, senhaSalt);

            Assert.True(resultado);
        }

        [Fact]
        public void Quando_VerificarLoginComSenhaIncorreta_Espero_RetornarFalso()
        {
            byte[] senhaHash;
            byte[] senhaSalt;

            sut.CriarSenhaHash("senha_teste", out senhaHash, out senhaSalt);

            var resultado = sut.VerificarLogin("outra_senha", senhaHash, senhaSalt);

            Assert.False(resultado);
        }

        [Fact]
        public void Quando_CriarTokenJwt_Espero_TokenAssinadoComClaimsConfiguradas()
        {
            // Arrange
            const string tokenKey = "0123456789abcdef0123456789abcdef0123456789abcdef0123456789abcdef";
            const string issuer = "ComandaApi";
            const string audience = "ComandaApi";
            configuration["JwtSettings:Token"].Returns(tokenKey);
            configuration["JwtSettings:Issuer"].Returns(issuer);
            configuration["JwtSettings:Audience"].Returns(audience);

            var usuario = new Usuario(new UsuarioComando
            {
                Nome = "Alex",
                Cpf = "12345678901",
                Email = "alex@example.com"
            });
            var credencial = new UsuarioCredencial(new UsuarioCredencialComando
            {
                UsuarioId = 42,
                NomeUsuario = "alex.nunes",
                SenhaHash = new byte[] { 1, 2, 3 },
                SenhaSalt = new byte[] { 4, 5, 6 }
            })
            {
                Usuario = usuario
            };

            // Act
            var tokenString = sut.CriarTokenJwt(credencial, CancellationToken.None);
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = issuer,
                ValidateAudience = true,
                ValidAudience = audience,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
                ClockSkew = TimeSpan.Zero
            };
            var principal = tokenHandler.ValidateToken(tokenString, validationParameters, out var validatedToken);
            var jwt = Assert.IsType<JwtSecurityToken>(validatedToken);

            // Assert
            Assert.Equal(issuer, jwt.Issuer);
            Assert.Contains(audience, jwt.Audiences);
            Assert.Equal("alex.nunes", principal.FindFirst("NomeUsuario")?.Value);
            Assert.Equal("alex@example.com", principal.FindFirst("Email")?.Value);
            Assert.Equal("42", principal.FindFirst("UsuarioId")?.Value);
            Assert.True(jwt.ValidTo > DateTime.UtcNow);
        }

        [Fact]
        public void Quando_CriarTokenJwtSemChaveConfigurada_Espero_LancarExcecao()
        {
            // Arrange
            configuration["JwtSettings:Token"].Returns((string?)null);
            var usuario = new Usuario(new UsuarioComando
            {
                Nome = "Alex",
                Cpf = "12345678901",
                Email = "alex@example.com"
            });
            var credencial = new UsuarioCredencial(new UsuarioCredencialComando
            {
                UsuarioId = 42,
                NomeUsuario = "alex.nunes",
                SenhaHash = new byte[] { 1, 2, 3 },
                SenhaSalt = new byte[] { 4, 5, 6 }
            })
            {
                Usuario = usuario
            };

            // Act & Assert
            var excecao = Assert.Throws<InvalidOperationException>(
                () => sut.CriarTokenJwt(credencial, CancellationToken.None));
            Assert.Equal("JwtSettings:Token não foi configurado.", excecao.Message);
        }
    }
}
