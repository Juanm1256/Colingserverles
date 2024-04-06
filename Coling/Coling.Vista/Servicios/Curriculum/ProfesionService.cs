using Coling.Vista.Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Vista.Servicios.Curriculum
{
    public class ProfesionService : IProfesionService
    {
        string url = "http://localhost:7015/";
        string endPoint = "";
        private readonly HttpClient client;

        public ProfesionService(HttpClient client)
        {
            this.client = client;
            this.client.BaseAddress = new Uri(url);
        }

        public async Task<bool> Eliminar(string id, string token)
        {
            bool sw = false;
            endPoint = url + $"api/eliminarProfesion/{id}";
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage respuesta = await client.DeleteAsync(endPoint);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<bool> Insertar(Profesion profesion, string token)
        {
            bool sw = false;
            endPoint = url + "api/InsertarProfesion";
            string jsonBody = JsonConvert.SerializeObject(profesion);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            HttpResponseMessage respuesta = await client.PostAsync(endPoint, content);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<List<Profesion>> Listar(string token)
        {
            endPoint = "api/ListarProfesion";
            client.BaseAddress = new Uri(url);

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await client.GetAsync(endPoint);
            List<Profesion> result = new List<Profesion>();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<Profesion>>(respuestaCuerpo);
            }
            return result;
        }

        public async Task<List<Profesion>> ListarEstado(string token)
        {
            string endPoint = "api/ListarProfesionEstado";
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await client.GetAsync(endPoint);
            List<Profesion> result = new List<Profesion>();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<Profesion>>(respuestaCuerpo);
            }
            return result;
        }

        public async Task<bool> Modificar(Profesion profesion, string token)
        {
            bool sw = false;
            endPoint = url + "api/ModificarProfesion";
            string jsonBody = JsonConvert.SerializeObject(profesion);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            HttpResponseMessage respuesta = await client.PutAsync(endPoint, content);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<Profesion> ObtenerPorId(string id, string token)
        {
            endPoint = url + $"api/obtenerProfesion/{id}";
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await client.GetAsync(endPoint);
            Profesion profesion = new Profesion();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                profesion = JsonConvert.DeserializeObject<Profesion>(respuestaCuerpo);
            }
            return profesion;
        }
    }
}
