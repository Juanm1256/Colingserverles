using Coling.API.Curriculum.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Curriculum.Contrato.Repositorios
{
    public interface ITipoEstudioRepositorio
    {
        public Task<bool> Insertar(TipoEstudio tipoEstudio);
        public Task<bool> UpdateIns(TipoEstudio tipoEstudio);
        public Task<bool> Delete(string partitionkey, string rowkey);
        public Task<List<TipoEstudio>> Getall();
        public Task<List<TipoEstudio>> Getallstatus();
        public Task<List<TipoEstudio>> GetallTipoEstudiostatus();
        public Task<List<TipoEstudio>> ListarPorNombre(string nombre);
        public Task<TipoEstudio> Get(string id);
    }
}
