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
    public class ProfesionRepositorio : IProfesionRepositorio
    {
        private readonly string? cadenaConexion;
        private readonly string tablaNombre;
        private readonly IConfiguration configuration;

        public ProfesionRepositorio(IConfiguration conf)
        {
            configuration = conf;
            cadenaConexion = configuration.GetSection("cadenaconexion").Value;
            tablaNombre = "Profesion";

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

        public async Task<Profesion> Get(string id)
        {
            var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
            var filtro = $"PartitionKey eq 'Educacion' and RowKey eq '{id}'";
            await foreach (Profesion profesion in tablaCliente.QueryAsync<Profesion>(filter: filtro))
            {
                return profesion;
            }
            return null;
        }

        public async Task<List<Profesion>> Getall()
        {
            List<Profesion> lista = new List<Profesion>();
            var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
            var filtro = $"PartitionKey eq 'Educacion'";
            await foreach (Profesion profesion in tablaCliente.QueryAsync<Profesion>(filter: filtro))
            {
                lista.Add(profesion);
            }
            return lista;
        }

        public async Task<List<Profesion>> Getallstatus()
        {
            List<Profesion> lista = new List<Profesion>();
            var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
            var filtro = $"PartitionKey eq 'Educacion' and Estado eq 'Activo' or Estado eq 'Inactivo'";
            await foreach (Profesion experienciaLaboral in tablaCliente.QueryAsync<Profesion>(filter: filtro))
            {
                lista.Add(experienciaLaboral);
            }
            return lista;
        }

        public async Task<List<Profesion>> ListarPorNombre(string nombre)
        {
            List<Profesion> lista = new List<Profesion>();
            var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
            var filtro = $"PartitionKey eq 'Educacion' and NombreProfesion ge '{nombre}' and Estado ne 'Eliminado'";
            await foreach (Profesion experiencia in tablaCliente.QueryAsync<Profesion>(filter: filtro))
            {
                lista.Add(experiencia);
            }
            return lista;
        }

        public async Task<bool> Insertar(Profesion profesion)
        {
            try
            {
                var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
                await tablaCliente.UpsertEntityAsync(profesion);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> UpdateIns(Profesion profesion)
        {
            try
            {
                var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
                await tablaCliente.UpdateEntityAsync(profesion, profesion.ETag);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
