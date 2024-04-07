using Coling.Vista.Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Vista.Servicios.Bolsatrabajo
{
    public class SolicitudService : ISolicitudService
    {
        string url = "http://localhost:7237/";
        string endPoint = "";
        private readonly HttpClient client;

        public SolicitudService(HttpClient client)
        {
            this.client = client;
            this.client.BaseAddress = new Uri(url);
        }

        public async Task<bool> Eliminar(string id, string token)
        {
            bool sw = false;
            endPoint = url + $"api/EliminarSolicitud/{id}";
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage respuesta = await client.DeleteAsync(endPoint);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<bool> Insertar(Solicitud estudio, string token)
        {
            bool sw = false;
            endPoint = url + "api/InsertarSolicitud";
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

        public async Task<List<Solicitud>> Listar(string token)
        {
            endPoint = "api/ListarSolicitud";
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await client.GetAsync(endPoint);
            List<Solicitud> result = new List<Solicitud>();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<Solicitud>>(respuestaCuerpo);
            }
            return result;
        }

        public async Task<List<Solicitud>> ListarEstado(string token)
        {
            string endPoint = "api/ListarSolicitudEstado";
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await client.GetAsync(endPoint);
            List<Solicitud> result = new List<Solicitud>();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<Solicitud>>(respuestaCuerpo);
            }
            return result;
        }
        public async Task<List<Solicitud>> ListarPorNombre(string nombre, string token)
        {
            string endPoint = $"api/ListarPorNombreSolicitudIns/{nombre}";
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await client.GetAsync(endPoint);
            List<Solicitud> result = new List<Solicitud>();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<Solicitud>>(respuestaCuerpo);
            }
            return result;
        }

        public async Task<bool> Modificar(Solicitud estudio, string id, string token)
        {
            bool sw = false;
            endPoint = url + $"api/ModificarSolicitud/{id}";
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

        public async Task<Solicitud> ObtenerPorId(string id, string token)
        {
            endPoint = url + $"api/ObtenerSolicitud/{id}";
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await client.GetAsync(endPoint);
            Solicitud estudio = new Solicitud();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                estudio = JsonConvert.DeserializeObject<Solicitud>(respuestaCuerpo);
            }
            return estudio;
        }
    }
}
