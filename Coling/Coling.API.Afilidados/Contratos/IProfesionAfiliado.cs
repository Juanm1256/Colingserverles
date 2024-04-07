using Coling.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Afilidados.Contratos
{
    public interface IProfesionAfiliado
    {
        public Task<bool> InsertarProfesionAfiliado(ProfesionAfiliado profesionAfiliado);
        public Task<bool> ModificaProfesionAfiliado(ProfesionAfiliado profesionAfiliado, int id);
        public Task<bool> EliminarProfesionAfiliado(int id);
        public Task<List<ProfesionAfiliado>> ListarProfesionAfiliadoTodos();
        public Task<List<ProfesionAfiliado>> ListarProfesionAfiliadoEstado();
        public Task<List<ProfesionAfiliado>> ListarProfesionAfiliadoPorNombre(string nombre);
        public Task<ProfesionAfiliado> ObtenerProfesionAfiliadoById(int id);
    }
}
