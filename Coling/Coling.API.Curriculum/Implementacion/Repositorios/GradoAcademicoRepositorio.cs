using Azure.Data.Tables;
using Coling.API.Curriculum.Contrato.Repositorios;
using Coling.API.Curriculum.Modelo;
using Coling.Shared;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Curriculum.Implementacion.Repositorios
{
    public class GradoAcademicoRepositorio : IGradoAcademicoRepositorio
    {
        private readonly string? cadenaConexion;
        private readonly string tablaNombre;
        private readonly IConfiguration configuration;

        public GradoAcademicoRepositorio(IConfiguration conf)
        {
            configuration = conf;
            cadenaConexion = configuration.GetSection("cadenaconexion").Value;
            tablaNombre = "GradoAcademico";

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

        public async Task<GradoAcademico> Get(string id)
        {
            var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
            var filtro = $"PartitionKey eq 'Educacion' and RowKey eq '{id}'";
            await foreach (GradoAcademico gradoAcademico in tablaCliente.QueryAsync<GradoAcademico>(filter: filtro))
            {
                return gradoAcademico;
            }
            return null;
        }

        public async Task<List<GradoAcademico>> Getall()
        {
            List<GradoAcademico> lista = new List<GradoAcademico>();
            var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
            var filtro = $"PartitionKey eq 'Educacion'";
            await foreach (GradoAcademico gradoAcademico in tablaCliente.QueryAsync<GradoAcademico>(filter: filtro))
            {
                lista.Add(gradoAcademico);
            }
            return lista;
        }

        public async Task<List<GradoAcademico>> Getallstatus()
        {
            List<GradoAcademico> lista = new List<GradoAcademico>();
            var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
            var filtro = $"PartitionKey eq 'Educacion' and Estado eq 'Activo' or Estado eq 'Inactivo'";
            await foreach (GradoAcademico experienciaLaboral in tablaCliente.QueryAsync<GradoAcademico>(filter: filtro))
            {
                lista.Add(experienciaLaboral);
            }
            return lista;
        }

        public async Task<List<GradoAcademico>> ListarPorNombre(string nombre)
        {
            List<GradoAcademico> lista = new List<GradoAcademico>();
            var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
            var filtro = $"PartitionKey eq 'Educacion' and NombreGrado ge '{nombre}' and Estado ne 'Eliminado'";
            await foreach (GradoAcademico experiencia in tablaCliente.QueryAsync<GradoAcademico>(filter: filtro))
            {
                lista.Add(experiencia);
            }
            return lista;
        }

        public async Task<bool> Insertar(GradoAcademico gradoacademico)
        {
            try
            {
                var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
                await tablaCliente.UpsertEntityAsync(gradoacademico);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> UpdateIns(GradoAcademico gradoacademico)
        {
            try
            {
                var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
                await tablaCliente.UpdateEntityAsync(gradoacademico, gradoacademico.ETag);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<GradoAcademico>> GetallstatusActivo()
        {
            List<GradoAcademico> lista = new List<GradoAcademico>();
            var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
            var filtro = $"PartitionKey eq 'Educacion' and Estado eq 'Activo'";
            await foreach (GradoAcademico gradoacademico in tablaCliente.QueryAsync<GradoAcademico>(filter: filtro))
            {
                lista.Add(gradoacademico);
            }
            return lista;
        }
    }
}
