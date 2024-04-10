using Coling.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Vista.Servicios.Afiliados
{
    public class DireccionService : IDireccionService
    {
        string url = "http://localhost:7102/";
        string endPoint = "";
        private readonly HttpClient clients;

        public DireccionService(HttpClient clients)
        {
            this.clients = clients;
            this.clients.BaseAddress = new Uri(url);
        }

        public async Task<bool> Eliminar(int id, string token)
        {
            bool sw = false;
            endPoint = url + $"api/EliminarDireccion/{id}";
            clients.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage respuesta = await clients.DeleteAsync(endPoint);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<bool> Insertar(Direccion direccion, string token)
        {
            bool sw = false;
            endPoint = url + "api/InsertarDireccion";
            string jsonBody = JsonConvert.SerializeObject(direccion);
            clients.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            HttpResponseMessage respuesta = await clients.PostAsync(endPoint, content);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<List<Direccion>> ListarEstado(string token)
        {
            endPoint = "api/ListarDireccionEstado";
            clients.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await clients.GetAsync(endPoint);
            List<Direccion> result = new List<Direccion>();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<Direccion>>(respuestaCuerpo);
            }
            return result;
        }

        public async Task<List<Direccion>> ListarDireccion(string token)
        {
            endPoint = "api/ListarDireccion";
            clients.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await clients.GetAsync(endPoint);
            List<Direccion> result = new List<Direccion>();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<Direccion>>(respuestaCuerpo);
            }
            return result;
        }

        public async Task<List<Direccion>> ListarPorNombre(string nombre, string token)
        {
            string endPoint = $"api/ListarDireccionPorNombre/{nombre}";
            clients.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await clients.GetAsync(endPoint);
            List<Direccion> result = new List<Direccion>();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<Direccion>>(respuestaCuerpo);
            }
            return result;
        }
        public async Task<bool> Modificar(Direccion direccion, int id, string token)
        {
            bool sw = false;
            endPoint = url + $"api/ModificarDireccion/{id}";
            string jsonBody = JsonConvert.SerializeObject(direccion);
            clients.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            HttpResponseMessage respuesta = await clients.PutAsync(endPoint, content);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<Direccion> ObtenerPorId(int id, string token)
        {
            endPoint = url + $"api/ObtenerDireccion/{id}";
            clients.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await clients.GetAsync(endPoint);
            Direccion result = new Direccion();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<Direccion>(respuestaCuerpo);
            }
            return result;
        }

        public async Task<List<Persona>> ListarPersona(string token)
        {
            endPoint = "api/ListarPersonasEstadoActivo";
            clients.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await clients.GetAsync(endPoint);
            List<Persona> result = new List<Persona>();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<Persona>>(respuestaCuerpo);
            }
            return result;
        }
    }
}
