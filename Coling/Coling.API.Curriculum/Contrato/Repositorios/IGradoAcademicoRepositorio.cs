using Coling.API.Curriculum.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Curriculum.Contrato.Repositorios
{
    public interface IGradoAcademicoRepositorio
    {
        public Task<bool> Insertar(GradoAcademico gradoacademico);
        public Task<bool> UpdateIns(GradoAcademico gradoacademico);
        public Task<bool> Delete(string partitionkey, string rowkey);
        public Task<List<GradoAcademico>> Getall();
        public Task<List<GradoAcademico>> Getallstatus();
        public Task<List<GradoAcademico>> GetallstatusActivo();
        public Task<List<GradoAcademico>> ListarPorNombre(string nombre);
        public Task<GradoAcademico> Get(string id);
    }
}
