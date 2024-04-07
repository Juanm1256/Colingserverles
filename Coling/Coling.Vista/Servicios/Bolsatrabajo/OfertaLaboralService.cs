using Coling.Shared;
using Coling.Vista.Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Vista.Servicios.Bolsatrabajo
{
    public class OfertaLaboralService : IOfertaLaboralService
    {
        string url = "http://localhost:7237/";
        string endPoint = "";
        private readonly HttpClient client;

        public OfertaLaboralService(HttpClient client)
        {
            this.client = client;
            this.client.BaseAddress = new Uri(url);
        }

        public async Task<bool> Eliminar(string id, string token)
        {
            bool sw = false;
            endPoint = url + $"api/EliminarOfertaLaboral/{id}";
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage respuesta = await client.DeleteAsync(endPoint);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<bool> Insertar(OfertaLaboral estudio, string token)
        {
            bool sw = false;
            endPoint = url + "api/InsertarOfertaLaboral";
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

        public async Task<List<OfertaLaboral>> Listar(string token)
        {
            endPoint = "api/ListarOfertaLaboral";
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await client.GetAsync(endPoint);
            List<OfertaLaboral> result = new List<OfertaLaboral>();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<OfertaLaboral>>(respuestaCuerpo);
            }
            return result;
        }

        public async Task<List<OfertaLaboral>> ListarEstado(string token)
        {
            string endPoint = "api/ListarOfertaLaboralEstado";
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await client.GetAsync(endPoint);
            List<OfertaLaboral> result = new List<OfertaLaboral>();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<OfertaLaboral>>(respuestaCuerpo);
            }
            return result;
        }
        public async Task<List<OfertaLaboral>> ListarPorNombre(string nombre, string token)
        {
            string endPoint = $"api/ListarPorNombreOfertaLaboralIns/{nombre}";
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await client.GetAsync(endPoint);
            List<OfertaLaboral> result = new List<OfertaLaboral>();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<OfertaLaboral>>(respuestaCuerpo);
            }
            return result;
        }

        public async Task<bool> Modificar(OfertaLaboral estudio, string id, string token)
        {
            bool sw = false;
            endPoint = url + $"api/ModificarOfertaLaboral/{id}";
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

        public async Task<OfertaLaboral> ObtenerPorId(string id, string token)
        {
            endPoint = url + $"api/ObtenerOfertaLaboral/{id}";
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await client.GetAsync(endPoint);
            OfertaLaboral estudio = new OfertaLaboral();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                estudio = JsonConvert.DeserializeObject<OfertaLaboral>(respuestaCuerpo);
            }
            return estudio;
        }
    }
}
