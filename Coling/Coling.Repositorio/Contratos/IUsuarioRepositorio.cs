using Coling.Repositorio.Implementacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Repositorio.Contratos
{
    public interface IUsuarioRepositorio
    {
        public Task<TokenData> VerficarCredenciales(string usuariox, string passwordx);
        public Task<bool> Insertar(string Idusuario, string usuariox, string passwordx, string rol, string estado);
        public Task<string> EncriptarPassword(string password);
        public Task<bool> ValidarToken(string token);
        public Task<TokenData> ConstruirToken(string usuarioname, string password, string rol, string estado);

    }
}
