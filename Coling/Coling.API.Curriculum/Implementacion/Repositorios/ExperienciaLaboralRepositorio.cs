using Azure.Data.Tables;
using Coling.API.Curriculum.Contrato.Repositorios;
using Coling.API.Curriculum.Modelo;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Curriculum.Implementacion.Repositorios
{
    public class ExperienciaLaboralRepositorio : IExperienciaLaboralRepositorio
    {
        private readonly string? cadenaConexion;
        private readonly string tablaNombre;
        private readonly IConfiguration configuration;

        public ExperienciaLaboralRepositorio(IConfiguration conf)
        {
            configuration = conf;
            cadenaConexion = configuration.GetSection("cadenaconexion").Value;
            tablaNombre = "ExperienciaLaboral";

        }

        public async Task<bool> Delete(string partitionkey, string rowkey)
        {
            try
            {
                var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
                await tablaCliente.DeleteEntityAsync(partitionkey, rowkey);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ExperienciaLaboral> Get(string id)
        {
            var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
            var filtro = $"PartitionKey eq 'Educacion' and RowKey eq '{id}'";
            await foreach (ExperienciaLaboral experienciaLaboral in tablaCliente.QueryAsync<ExperienciaLaboral>(filter: filtro))
            {
                return experienciaLaboral;
            }
            return null;
        }

        public async Task<List<ExperienciaLaboral>> Getall()
        {
            List<ExperienciaLaboral> lista = new List<ExperienciaLaboral>();
            var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
            var filtro = $"PartitionKey eq 'Educacion'";
            await foreach (ExperienciaLaboral experienciaLaboral in tablaCliente.QueryAsync<ExperienciaLaboral>(filter: filtro))
            {
                lista.Add(experienciaLaboral);
            }
            return lista;
        }

        public async Task<List<ExperienciaLaboral>> Getallstatus()
        {
            List<ExperienciaLaboral> lista = new List<ExperienciaLaboral>();
            var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
            var filtro = $"PartitionKey eq 'Educacion' and Estado eq 'Activo' or Estado eq 'Inactivo'";
            await foreach (ExperienciaLaboral experienciaLaboral in tablaCliente.QueryAsync<ExperienciaLaboral>(filter: filtro))
            {
                lista.Add(experienciaLaboral);
            }
            return lista;
        }

        public async Task<List<ExperienciaLaboral>> ListarPorNombre(string nombre)
        {
            List<ExperienciaLaboral> lista = new List<ExperienciaLaboral>();
            var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
            var filtro = $"PartitionKey eq 'Educacion' and Institucion ge '{nombre}' and Estado ne 'Eliminado'";
            await foreach (ExperienciaLaboral experiencia in tablaCliente.QueryAsync<ExperienciaLaboral>(filter: filtro))
            {
                lista.Add(experiencia);
            }
            return lista;
        }

        public async Task<bool> Insertar(ExperienciaLaboral experienciaLaboral)
        {
            try
            {
                var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
                await tablaCliente.UpsertEntityAsync(experienciaLaboral);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> UpdateIns(ExperienciaLaboral experienciaLaboral)
        {
            try
            {
                var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
                await tablaCliente.UpdateEntityAsync(experienciaLaboral, experienciaLaboral.ETag);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
