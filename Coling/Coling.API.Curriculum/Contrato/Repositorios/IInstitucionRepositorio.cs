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
        public Task<string> Insertar(Institucion institucion);
        public Task<bool> UpdateIns(Institucion institucion);
        public Task<bool> Delete(string rowkey);
        public Task<List<Institucion>> Getall();
        public Task<List<Institucion>> Getallstatus();
        public Task<List<Institucion>> GetallInstitucionstatus();
        public Task<List<Institucion>> ListarPorNombre(string nombre);
        public Task<Institucion> Get(string id);
    }
}
