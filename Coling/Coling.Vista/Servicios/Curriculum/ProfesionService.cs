using Coling.Shared;
using Coling.Vista.Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Coling.Vista.Servicios.Curriculum
{
    public class ProfesionService : IProfesionService
    {
        private string url = "http://localhost:7015/";
        private string endPoint = "";
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
            endPoint = "api/ListarProfesionEstado";
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var responseTask1 = client.GetAsync(endPoint);
            var responseTask2 = client.GetAsync(APIs.listargradoestadoactivo);

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

                    var result = JsonConvert.DeserializeObject<List<Profesion>>(respuestaCuerpo1);
                    var gradoAcademicos = JsonConvert.DeserializeObject<List<GradoAcademico>>(respuestaCuerpo2);

                    var diccionariograd = gradoAcademicos.ToDictionary(p => p.RowKey, p => p.NombreGrado);

                    foreach (var item in result)
                    {
                        if (diccionariograd.TryGetValue(item.Idgrado, out string nombregradoa))
                        {
                            item.Idgrado = nombregradoa;
                        }
                    }

                    return result;
                }
            }
            else
            {
                return new List<Profesion>();
            }
        }


        public async Task<List<Profesion>> ListarPorNombre(string nombre, string token)
        {
            endPoint = $"api/ListarPorNombreProfesion/{nombre}";
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

        public async Task<List<Profesion>> ListarProfesionEstadoActivo(string token)
        {
            endPoint = "api/ListarProfesionEstadoActivo";
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var responseTask1 = client.GetAsync(endPoint);
            var responseTask2 = client.GetAsync(APIs.listargradoestadoactivo);

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

                    var result = JsonConvert.DeserializeObject<List<Profesion>>(respuestaCuerpo1);
                    var gradoAcademicos = JsonConvert.DeserializeObject<List<GradoAcademico>>(respuestaCuerpo2);

                    var diccionariograd = gradoAcademicos.ToDictionary(p => p.RowKey, p => p.NombreGrado);

                    foreach (var item in result)
                    {
                        if (diccionariograd.TryGetValue(item.Idgrado, out string nombregradoa))
                        {
                            item.Idgrado = nombregradoa;
                        }
                    }

                    return result;
                }
            }
            else
            {
                return new List<Profesion>();
            }
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
