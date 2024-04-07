using Coling.Vista.Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Vista.Servicios.Curriculum
{
    public class GradoAcademicoService : IGradoAcademicoService
    {
        string url = "http://localhost:7015/";
        string endPoint = "";
        private readonly HttpClient client;

        public GradoAcademicoService(HttpClient client)
        {
            this.client = client;
            this.client.BaseAddress = new Uri(url);
        }

        public async Task<bool> Eliminar(string id, string token)
        {
            bool sw = false;
            endPoint = url + $"api/eliminarGrado/{id}";
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage respuesta = await client.DeleteAsync(endPoint);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        }


        public async Task<bool> Insertar(GradoAcademico gradoAcademico, string token)
        {
            bool sw = false;
            endPoint = url + "api/InsertarGradoAcademico";
            string jsonBody = JsonConvert.SerializeObject(gradoAcademico);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            HttpResponseMessage respuesta = await client.PostAsync(endPoint, content);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<List<GradoAcademico>> Listar(string token)
        {
            endPoint = "api/ListarGradoAcademico";
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await client.GetAsync(endPoint);
            List<GradoAcademico> result = new List<GradoAcademico>();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<GradoAcademico>>(respuestaCuerpo);
            }
            return result;
        }

        public async Task<List<GradoAcademico>> ListarEstado(string token)
        {
            string endPoint = "api/ListarGradoAEstado";
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await client.GetAsync(endPoint);
            List<GradoAcademico> result = new List<GradoAcademico>();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<GradoAcademico>>(respuestaCuerpo);
            }
            return result;
        }

        public async Task<List<GradoAcademico>> ListarPorNombre(string nombre, string token)
        {
            string endPoint = $"api/ListarPorNombreGradoA/{nombre}";
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await client.GetAsync(endPoint);
            List<GradoAcademico> result = new List<GradoAcademico>();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<GradoAcademico>>(respuestaCuerpo);
            }
            return result;
        }

        public async Task<bool> Modificar(GradoAcademico gradoAcademico, string token)
        {
            bool sw = false;
            endPoint = url + "api/ModificarGradoAcademico";
            string jsonBody = JsonConvert.SerializeObject(gradoAcademico);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            HttpResponseMessage respuesta = await client.PutAsync(endPoint, content);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<GradoAcademico> ObtenerPorId(string id, string token)
        {
            endPoint = url + $"api/obtenerGrado/{id}";
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await client.GetAsync(endPoint);
            GradoAcademico gradoAcademico = new GradoAcademico();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                gradoAcademico = JsonConvert.DeserializeObject<GradoAcademico>(respuestaCuerpo);
            }
            return gradoAcademico;
        }
    }
}
