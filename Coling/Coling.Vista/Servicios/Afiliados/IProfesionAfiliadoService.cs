using Coling.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Vista.Servicios.Afiliados
{
    public interface IProfesionAfiliadoService
    {
        Task<List<ProfesionAfiliado>> ListarProfesionAfiliado(string token);
        Task<List<ProfesionAfiliado>> ListarEstado(string token);
        Task<List<ProfesionAfiliado>> ListarPorNombre(string nombre, string token);
        Task<List<Afiliado>> ListarAfiliado(string token);
        Task<bool> Insertar(ProfesionAfiliado profesionAfiliado, string token);
        Task<bool> Modificar(ProfesionAfiliado profesionAfiliado, int id, string token);
        Task<bool> Eliminar(int id, string token);
        Task<ProfesionAfiliado> ObtenerPorId(int id, string token);
    }
}
