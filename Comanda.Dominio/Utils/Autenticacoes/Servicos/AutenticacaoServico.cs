using Comanda.Dominio.UsuariosCredenciais.Entidades;
using Comanda.Dominio.Utils.Autenticacoes.Servicos.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Comanda.Dominio.Utils.Autenticacoes.Servicos
{
    public class AutenticacaoServico : IAutenticacaoServico
    {
        private readonly IConfiguration configuration;
        public AutenticacaoServico(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void CriarSenhaHash(string senha, out byte[] senhaHash, out byte[] senhaSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                senhaSalt = hmac.Key;
                senhaHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
            }
        }

        public bool VerificarLogin(string senha, byte[] senhaHash, byte[] senhaSalt)
        {
            using (var hmac = new HMACSHA512(senhaSalt))
            {
                var hashUsuario = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
                return hashUsuario.SequenceEqual(senhaHash);
            }
        }

        public string CriarTokenJwt(UsuarioCredencial usuarioCredencial, CancellationToken cancellationToken)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim("NomeUsuario", usuarioCredencial.NomeUsuario),
                new Claim("Email", usuarioCredencial.Usuario.Email),
                new Claim("UsuarioId", usuarioCredencial.UsuarioId.ToString())
            };

            var tokenKey = configuration["JwtSettings:Token"]
                ?? throw new InvalidOperationException("JwtSettings:Token não foi configurado.");

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(tokenKey));

            var credenciais = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    issuer: configuration["JwtSettings:Issuer"],
                    audience: configuration["JwtSettings:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(10),
                    signingCredentials: credenciais
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
