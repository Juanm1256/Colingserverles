using Coling.Repositorio.Contratos;
using DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Coling.Repositorio.Implementacion
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly IConfiguration configuration;

        public UsuarioRepositorio(IConfiguration configuration) 
        {
            this.configuration = configuration;
        }
        public async Task<TokenData> ConstruirToken(string usuarioanme, string password)
        {
            var claims = new List<Claim>()
            {
                new Claim("usuario", usuarioanme),
                new Claim("rol", "Admin"),
                new Claim("estado", "Activo")
            };

            var Secretkey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration["llaveSecreta"]?? ""));
            var creds = new SigningCredentials(Secretkey, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(1);

            var tokenSeguridad = new JwtSecurityToken(issuer:null, audience:null, claims:claims, expires=expires, signingCredentials:creds);

            TokenData respuestatoken = new TokenData();
            respuestatoken.Token = new JwtSecurityTokenHandler().WriteToken(tokenSeguridad);
            respuestatoken.Expira = expires;

            return respuestatoken;
        }

        public Task<string> DesencriptarPassword(string passwords)
        {
            throw new NotImplementedException();
        }

        public async Task<string> EncriptarPassword(string password)
        {
            string Encritado = "";
            using (SHA256 sHA256 = SHA256.Create())
            {
                byte[] bytes = sHA256.ComputeHash(Encoding.UTF8.GetBytes(password));

                Encritado = Convert.ToBase64String(bytes);
            }
            return Encritado;
        }

        public Task<bool> ValidarToken(string token)
        {
            throw new NotImplementedException();
        }

        public async Task<TokenData> VerificarCredenciales(string usuariox, string passwordx)
        {
            TokenData tokenDevolver = new TokenData();
            string passEncriptado= await EncriptarPassword(passwordx);
            string consulta = "select count(idusuario) from Usuario where nombreuser= '"+usuariox+"' and password='"+passEncriptado+"'";
            int Existe = conexion.EjecutarEscalar(consulta);
            if (Existe > 0)
            {
                tokenDevolver = await ConstruirToken(usuariox, passwordx);
                
            }
            return tokenDevolver;
        }
    }
}
