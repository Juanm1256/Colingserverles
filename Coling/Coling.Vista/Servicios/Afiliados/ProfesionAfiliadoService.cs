using Coling.Shared;
using Coling.Vista.Modelos;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Vista.Servicios.Afiliados
{
    public class ProfesionAfiliadoService : IProfesionAfiliadoService
    {
        private string url = "http://localhost:7102/";
        private string curl = "http://localhost:7015/";
        private string endPoint = "";
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
            var responseTask1 = clients.GetAsync(endPoint);
            var responseTask2 = clients.GetAsync($"{curl}{APIs.listarprofesionestadoactivo}");

            await Task.WhenAll(responseTask1, responseTask2);

            var response1 = await responseTask1;
            var response2 = await responseTask2;

            if (response1.IsSuccessStatusCode && response2.IsSuccessStatusCode)
            {
                using (var stream1 = await response1.Content.ReadAsStreamAsync())
                using (var stream2 = await response2.Content.ReadAsStreamAsync())
                using (var reader1 = new StreamReader(stream1))
                using (var reader2 = new StreamReader(stream2))
                {
                    var respuestaCuerpo1 = await reader1.ReadToEndAsync();
                    var respuestaCuerpo2 = await reader2.ReadToEndAsync();

                    var result = JsonConvert.DeserializeObject<List<ProfesionAfiliado>>(respuestaCuerpo1);
                    var profesiones = JsonConvert.DeserializeObject<List<Profesion>>(respuestaCuerpo2);

                    var diccionarioProfesiones = profesiones.ToDictionary(p => p.RowKey, p => p.NombreProfesion);

                    foreach (var item in result)
                    {
                        if (diccionarioProfesiones.TryGetValue(item.Idprofesion, out string nombreProfesion))
                        {
                            item.Idprofesion = nombreProfesion;
                        }
                    }

                    return result;
                }
            }
            else
            {
                return new List<ProfesionAfiliado>();
            }
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
