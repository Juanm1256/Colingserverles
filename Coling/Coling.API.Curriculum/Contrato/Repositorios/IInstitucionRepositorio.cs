using Coling.API.Curriculum.Modelo;
using Coling.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Curriculum.Contrato.Repositorios
{
    public interface IInstitucionRepositorio
    {
        public Task<bool> Insertar(Institucion institucion);
        public Task<bool> UpdateIns(Institucion institucion);
        public Task<bool> Delete(string partitionkey, string rowkey);
        public Task<List<Institucion>> Getall();
        public Task<Institucion> Get(string id);
    }
}
