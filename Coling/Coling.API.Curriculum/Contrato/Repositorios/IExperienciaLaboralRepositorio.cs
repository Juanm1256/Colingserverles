using Coling.API.Curriculum.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Curriculum.Contrato.Repositorios
{
    public interface IExperienciaLaboralRepositorio
    {
        public Task<bool> Insertar(ExperienciaLaboral experiencia);
        public Task<bool> UpdateIns(ExperienciaLaboral experiencia);
        public Task<bool> Delete(string partitionkey, string rowkey);
        public Task<List<ExperienciaLaboral>> Getall();
        public Task<List<ExperienciaLaboral>> Getallstatus();
        public Task<List<ExperienciaLaboral>> ListarPorNombre(string nombre);
        public Task<ExperienciaLaboral> Get(string id);
    }
}
