using Azure.Data.Tables;
using Coling.API.Curriculum.Contrato.Repositorios;
using Coling.API.Curriculum.Modelo;
using Coling.Shared;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Curriculum.Implementacion.Repositorios
{
    public class InstitucionRepositorio : IInstitucionRepositorio
    {
        private readonly string? cadenaConexion;
        private readonly string tablaNombre;
        private readonly IConfiguration configuration;

        public InstitucionRepositorio(IConfiguration conf)
        {
            configuration= conf;
            cadenaConexion = configuration.GetSection("cadenaconexion").Value;
            tablaNombre = "Institucion";

        }

        public async Task<bool> Delete(string rowkey)
        {
            try
            {
                var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
                var entity = tablaCliente.QueryAsync<Institucion>(filter: $"RowKey eq '{rowkey}'");
                await foreach (var item in entity)
                {
                    await tablaCliente.DeleteEntityAsync(item.PartitionKey, item.RowKey);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Institucion> Get(string id)
        {
            var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
            var filtro = $"PartitionKey eq 'Educacion' and RowKey eq '{id}'";
            await foreach (Institucion institucion in tablaCliente.QueryAsync<Institucion>(filter: filtro))
            {
                return institucion;
            }
            return null;
        }

        public async Task<List<Institucion>> Getall()
        {
            List<Institucion> lista = new List<Institucion>();
            var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
            var filtro = $"PartitionKey eq 'Educacion'";
            await foreach (Institucion institucion in tablaCliente.QueryAsync<Institucion>(filter: filtro))
            {
                lista.Add(institucion);
            }
            return lista;
        }

        public async Task<bool> Insertar(Institucion institucion)
        {
            try
            {
                var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
                await tablaCliente.UpsertEntityAsync(institucion);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> UpdateIns(Institucion institucion)
        {
            try
            {
                var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
                await tablaCliente.UpdateEntityAsync(institucion, institucion.ETag);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
