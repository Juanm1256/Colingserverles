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
    public class TipoEstudioRepositorio : ITipoEstudioRepositorio
    {
        private readonly string? cadenaConexion;
        private readonly string tablaNombre;
        private readonly IConfiguration configuration;

        public TipoEstudioRepositorio(IConfiguration conf)
        {
            configuration = conf;
            cadenaConexion = configuration.GetSection("cadenaconexion").Value;
            tablaNombre = "TipoEstudio";

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

        public async Task<TipoEstudio> Get(string id)
        {
            var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
            var filtro = $"PartitionKey eq 'Educacion' and RowKey eq '{id}'";
            await foreach (TipoEstudio tipoEstudio in tablaCliente.QueryAsync<TipoEstudio>(filter: filtro))
            {
                return tipoEstudio;
            }
            return null;
        }

        public async Task<List<TipoEstudio>> Getall()
        {
            List<TipoEstudio> lista = new List<TipoEstudio>();
            var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
            var filtro = $"PartitionKey eq 'Educacion'";
            await foreach (TipoEstudio tipoEstudio in tablaCliente.QueryAsync<TipoEstudio>(filter: filtro))
            {
                lista.Add(tipoEstudio);
            }
            return lista;
        }

        public async Task<List<TipoEstudio>> Getallstatus()
        {
            List<TipoEstudio> lista = new List<TipoEstudio>();
            var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
            var filtro = $"PartitionKey eq 'Educacion' and Estado eq 'Activo' or Estado eq 'Inactivo'";
            await foreach (TipoEstudio tipoEstudio in tablaCliente.QueryAsync<TipoEstudio>(filter: filtro))
            {
                lista.Add(tipoEstudio);
            }
            return lista;
        }

        public async Task<List<TipoEstudio>> ListarPorNombre(string nombre)
        {
            List<TipoEstudio> lista = new List<TipoEstudio>();
            var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
            var filtro = $"PartitionKey eq 'Educacion' and NombreProfesion ge '{nombre}' and Estado ne 'Eliminado'";
            await foreach (TipoEstudio tipoEstudio in tablaCliente.QueryAsync<TipoEstudio>(filter: filtro))
            {
                lista.Add(tipoEstudio);
            }
            return lista;
        }

        public async Task<bool> Insertar(TipoEstudio tipoEstudio)
        {
            try
            {
                var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
                await tablaCliente.UpsertEntityAsync(tipoEstudio);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> UpdateIns(TipoEstudio tipoEstudio)
        {
            try
            {
                var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
                await tablaCliente.UpdateEntityAsync(tipoEstudio, tipoEstudio.ETag);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<TipoEstudio>> GetallTipoEstudiostatus()
        {
            List<TipoEstudio> lista = new List<TipoEstudio>();
            var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
            var filtro = $"PartitionKey eq 'Educacion' and Estado eq 'Activo'";
            await foreach (TipoEstudio tipoEstudio in tablaCliente.QueryAsync<TipoEstudio>(filter: filtro))
            {
                lista.Add(tipoEstudio);
            }
            return lista;
        }
    }
}
