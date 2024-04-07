using Coling.Vista.Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Vista.Servicios.Curriculum
{
    public class TipoEstudioService : ITipoEstudioService
    {
        string url = "http://localhost:7015/";
        string endPoint = "";
        private readonly HttpClient client;

        public TipoEstudioService(HttpClient client)
        {
            this.client = client;
            this.client.BaseAddress = new Uri(url);
        }

        public async Task<bool> Eliminar(string id, string token)
        {
            bool sw = false;
            endPoint = url + $"api/eliminarTipo/{id}";
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage respuesta = await client.DeleteAsync(endPoint);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<bool> Insertar(TipoEstudio estudio, string token)
        {
            bool sw = false;
            endPoint = url + "api/InsertarTipoEstudio";
            string jsonBody = JsonConvert.SerializeObject(estudio);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            HttpResponseMessage respuesta = await client.PostAsync(endPoint, content);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<List<TipoEstudio>> Listar(string token)
        {
            endPoint = "api/ListarTipoEstudio";
            client.BaseAddress = new Uri(url);

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await client.GetAsync(endPoint);
            List<TipoEstudio> result = new List<TipoEstudio>();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<TipoEstudio>>(respuestaCuerpo);
            }
            return result;
        }

        public async Task<List<TipoEstudio>> ListarEstado(string token)
        {
            string endPoint = "api/ListarTipoEstudioEstado";
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await client.GetAsync(endPoint);
            List<TipoEstudio> result = new List<TipoEstudio>();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<TipoEstudio>>(respuestaCuerpo);
            }
            return result;
        }

        public async Task<List<TipoEstudio>> ListarPorNombre(string nombre, string token)
        {
            string endPoint = $"api/ListarPorNombreTipoEstudio/{nombre}";
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await client.GetAsync(endPoint);
            List<TipoEstudio> result = new List<TipoEstudio>();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<TipoEstudio>>(respuestaCuerpo);
            }
            return result;
        }

        public async Task<bool> Modificar(TipoEstudio estudio, string token)
        {
            bool sw = false;
            endPoint = url + "api/ModificarTipoEstudio";
            string jsonBody = JsonConvert.SerializeObject(estudio);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            HttpResponseMessage respuesta = await client.PutAsync(endPoint, content);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<TipoEstudio> ObtenerPorId(string id, string token)
        {
            endPoint = url + $"api/obtenerTipo/{id}";
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await client.GetAsync(endPoint);
            TipoEstudio estudio = new TipoEstudio();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                estudio = JsonConvert.DeserializeObject<TipoEstudio>(respuestaCuerpo);
            }
            return estudio;
        }
    }
}
