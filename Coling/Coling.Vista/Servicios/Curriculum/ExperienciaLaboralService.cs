using Coling.Vista.Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Vista.Servicios.Curriculum
{
    public class ExperienciaLaboralService : IExperienciaLaboralService
    {
        string url = "http://localhost:7015/";
        string endPoint = "";
        private readonly HttpClient client;

        public ExperienciaLaboralService(HttpClient client)
        {
            this.client = client;
            this.client.BaseAddress = new Uri(url);
        }

        public async Task<bool> Eliminar(string id, string token)
        {
            bool sw = false;
            endPoint = url + $"api/eliminarExperienciaLaboral/{id}";
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage respuesta = await client.DeleteAsync(endPoint);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<bool> Insertar(ExperienciaLaboral experienciaLaboral, string token)
        {
            bool sw = false;
            endPoint = url + "api/InsertarExperienciaLaboral";
            string jsonBody = JsonConvert.SerializeObject(experienciaLaboral);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            HttpResponseMessage respuesta = await client.PostAsync(endPoint, content);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<List<ExperienciaLaboral>> Listar(string token)
        {
            endPoint = "api/ListarExperienciaLaboral";
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await client.GetAsync(endPoint);
            List<ExperienciaLaboral> result = new List<ExperienciaLaboral>();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<ExperienciaLaboral>>(respuestaCuerpo);
            }
            return result;
        }

        public async Task<List<ExperienciaLaboral>> ListarEstado(string token)
        {
            string endPoint = "api/ListarExperienciaLaboralEstado";
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await client.GetAsync(endPoint);
            List<ExperienciaLaboral> result = new List<ExperienciaLaboral>();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<ExperienciaLaboral>>(respuestaCuerpo);
            }
            return result;
        }

        public async Task<bool> Modificar(ExperienciaLaboral experienciaLaboral, string token)
        {
            bool sw = false;
            endPoint = url + "api/ModificarExperienciaLaboral";
            string jsonBody = JsonConvert.SerializeObject(experienciaLaboral);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            HttpResponseMessage respuesta = await client.PutAsync(endPoint, content);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<ExperienciaLaboral> ObtenerPorId(string id, string token)
        {
            endPoint = url + $"api/obtenerExperienciaLaboral/{id}";
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await client.GetAsync(endPoint);
            ExperienciaLaboral experienciaLaboral = new ExperienciaLaboral();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                experienciaLaboral = JsonConvert.DeserializeObject<ExperienciaLaboral>(respuestaCuerpo);
            }
            return experienciaLaboral;
        }
    }
}
