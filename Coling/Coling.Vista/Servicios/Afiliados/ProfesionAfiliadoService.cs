using Coling.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Vista.Servicios.Afiliados
{
    public class ProfesionAfiliadoService : IProfesionAfiliadoService
    {
        string url = "http://localhost:7102/";
        string endPoint = "";
        private readonly HttpClient clients;

        public ProfesionAfiliadoService(HttpClient clients)
        {
            this.clients = clients;
            this.clients.BaseAddress = new Uri(url);
        }

        public async Task<bool> Eliminar(int id, string token)
        {
            bool sw = false;
            endPoint = url + $"api/EliminarProfesionAfiliado/{id}";
            clients.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage respuesta = await clients.DeleteAsync(endPoint);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<bool> Insertar(ProfesionAfiliado profesionAfiliado, string token)
        {
            bool sw = false;
            endPoint = url + "api/InsertarProfesionAfiliado";
            string jsonBody = JsonConvert.SerializeObject(profesionAfiliado);
            clients.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            HttpResponseMessage respuesta = await clients.PostAsync(endPoint, content);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<List<ProfesionAfiliado>> ListarEstado(string token)
        {
            endPoint = "api/ListarProfesionAfiliadoEstado";
            clients.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await clients.GetAsync(endPoint);
            List<ProfesionAfiliado> result = new List<ProfesionAfiliado>();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<ProfesionAfiliado>>(respuestaCuerpo);
            }
            return result;
        }

        public async Task<List<ProfesionAfiliado>> ListarProfesionAfiliado(string token)
        {
            endPoint = "api/ListarProfesionAfiliado";
            clients.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await clients.GetAsync(endPoint);
            List<ProfesionAfiliado> result = new List<ProfesionAfiliado>();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<ProfesionAfiliado>>(respuestaCuerpo);
            }
            return result;
        }

        public async Task<List<ProfesionAfiliado>> ListarPorNombre(string nombre, string token)
        {
            string endPoint = $"api/ListarProfesionAfiliadoPorNombre/{nombre}";
            clients.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await clients.GetAsync(endPoint);
            List<ProfesionAfiliado> result = new List<ProfesionAfiliado>();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<ProfesionAfiliado>>(respuestaCuerpo);
            }
            return result;
        }
        public async Task<bool> Modificar(ProfesionAfiliado profesionAfiliado, int id, string token)
        {
            bool sw = false;
            endPoint = url + $"api/ModificarProfesionAfiliado/{id}";
            string jsonBody = JsonConvert.SerializeObject(profesionAfiliado);
            clients.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            HttpResponseMessage respuesta = await clients.PutAsync(endPoint, content);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<ProfesionAfiliado> ObtenerPorId(int id, string token)
        {
            endPoint = url + $"api/ObtenerProfesionAfiliado/{id}";
            clients.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await clients.GetAsync(endPoint);
            ProfesionAfiliado result = new ProfesionAfiliado();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<ProfesionAfiliado>(respuestaCuerpo);
            }
            return result;
        }

        public async Task<List<Afiliado>> ListarAfiliado(string token)
        {
            endPoint = "api/ListarAfiliadoEstadoActivo";
            clients.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await clients.GetAsync(endPoint);
            List<Afiliado> result = new List<Afiliado>();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<Afiliado>>(respuestaCuerpo);
            }
            return result;
        }
    }
}
