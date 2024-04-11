using Coling.Repositorio.Contratos;
using Coling.Shared;
using DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
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
        public async Task<TokenData> ConstruirToken(string usuarioname, string password, string rol, string estado)
        {
            var claims = new List<Claim>()
            {
                new Claim("usuario", usuarioname),
                new Claim("rol", rol),
                new Claim("estado", estado)
            };

            var SecretKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration["LlaveSecreta"] ?? ""));
            var creds = new SigningCredentials(SecretKey, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddHours(24);

            var tokenSeguridad = new JwtSecurityToken(issuer: null, audience: null, claims: claims, expires: expires, signingCredentials: creds);

            TokenData respuestaToken = new TokenData();
            respuestaToken.Token = new JwtSecurityTokenHandler().WriteToken(tokenSeguridad);
            respuestaToken.Expira = expires;

            return respuestaToken;
        }

        public async Task<string> EncriptarPassword(string password)
        {
            string Encriptado = "";
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                Encriptado = Convert.ToBase64String(bytes);
            }
            return Encriptado;
        }

        public async Task<bool> Insertar(string Idusuario, string usuariox, string passwordx, string rol, string estado)
        {
            string passEncriptado = await EncriptarPassword(passwordx);
            string consulta = "INSERT INTO Usuario (idusuario, nombreuser, password, rol, estado) VALUES (@Idusuario ,@usuario, @password, @rol, @estado)";
            bool exito = conexion.Insertardatas(consulta, Idusuario, usuariox, passEncriptado, rol, estado);
            if (exito)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> Modificar(string Idusuario, string usuariox, string passwordx, string rol, string estado)
        {
            string passEncriptado = await EncriptarPassword(passwordx);
            string consulta = "UPDATE Usuario SET nombreuser = @usuario, password = @password, rol = @rol, estado = @estado WHERE idusuario = @Idusuario";
            bool exito = conexion.Insertardatas(consulta, Idusuario, usuariox, passEncriptado, rol, estado);
            if (exito)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<RegistrarUsuario> Obtenerid(string Idusuario)
        {
            string consulta = "SELECT idusuario, nombreuser, password, rol, estado FROM Usuario WHERE idusuario = @Idusuario";
            RegistrarUsuario usuario = conexion.Obtenerdata(consulta, Idusuario);
            return usuario;
        }

        public Task<bool> ValidarToken(string token)
        {
            throw new NotImplementedException();
        }

        public async Task<TokenData> VerficarCredenciales(string usuariox, string passwordx)
        {
            TokenData tokenDevolver = new TokenData();
            string passEncriptado = await EncriptarPassword(passwordx);
            string consulta = "select * from Usuario where nombreuser='" + usuariox + "' and password='" + passEncriptado + "'";
            DataTable Existe = conexion.EjecutarDataTabla(consulta, "tabla");
            if (Existe.Rows.Count > 0)
            {
                string rol = Existe.Rows[0]["rol"].ToString();
                string estado = Existe.Rows[0]["estado"].ToString();
                tokenDevolver = await ConstruirToken(usuariox, passwordx, rol, estado);
            }
            return tokenDevolver;
        }

    }
}
