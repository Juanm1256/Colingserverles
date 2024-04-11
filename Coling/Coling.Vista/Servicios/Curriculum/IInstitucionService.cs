using Coling.Shared;
using Coling.Vista.Modelos;
using Coling.Vista.Modelos.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Vista.Servicios.Curriculum
{
    public interface IInstitucionService
    {
        Task<List<Institucion>> ListarInstitucion(string token);
        Task<List<Institucion>> ListarEstado(string token);
        Task<List<Institucion>> ListarInstitucionEstadoActivo(string token);
        Task<List<Institucion>> ListarPorNombre(string nombre, string token);
        Task<bool> InsertarInstitucion(InstituUser instituuser, string token);
        Task<bool> ModificarInstitucion(Institucion institucion, string token);
        Task<bool> EliminarInstitucion(string id, string token);
        Task<Institucion> ObtenerInstitucionPorId(string id, string token);
        Task<RegistrarUsuario> ObteneruserPorId(string id, string token);
    }
}
