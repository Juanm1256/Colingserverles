using Coling.API.Curriculum.Modelo;
using Coling.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Curriculum.Contrato.Repositorios
{
    public interface IProfesionRepositorio
    {
        public Task<bool> Insertar(Profesion profesion);
        public Task<bool> UpdateIns(Profesion profesion);
        public Task<bool> Delete(string partitionkey, string rowkey);
        public Task<List<Profesion>> Getall();
        public Task<List<Profesion>> Getallstatus();
        public Task<List<Profesion>> ListarPorNombre(string nombre);
        public Task<Profesion> Get(string id);
    }
}
