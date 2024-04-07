using Coling.API.Curriculum.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Curriculum.Contrato.Repositorios
{
    public interface IEstudiosRepositorio
    {
        public Task<bool> Insertar(Estudios estudios);
        public Task<bool> UpdateIns(Estudios estudios);
        public Task<bool> Delete(string partitionkey, string rowkey);
        public Task<List<Estudios>> Getall();
        public Task<List<Estudios>> Getallstatus();
        public Task<List<Estudios>> ListarPorNombre(string nombre);
        public Task<List<Institucion>> ListarPorNombreInstitucion(string nombre);
        public Task<Estudios> Get(string id);
    }
}
