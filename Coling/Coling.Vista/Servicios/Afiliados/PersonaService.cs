using Azure;
using Coling.Shared;
using Coling.Shared.DTOs;
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
    public class PersonaService : IPersonaService
    {
        private string url = "http://localhost:7102/";
        private string baseurl = "http://localhost:7276/";
        private string endPoint = "";
        private readonly HttpClient clients;

        public PersonaService(HttpClient clients)
        {
            this.clients = clients;
            this.clients.BaseAddress = new Uri(url);
        }

        public async Task<bool> Eliminar(int id, string token)
        {
            bool sw = false;
            endPoint = url + $"api/EliminarPersona/{id}";
            clients.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage respuesta = await clients.DeleteAsync(endPoint);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<bool> Insertar(Persona persona, string token)
        {
            bool sw = false;
            endPoint = url + "api/InsertarPersona";
            string jsonBody = JsonConvert.SerializeObject(persona);
            clients.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            HttpResponseMessage respuesta = await clients.PostAsync(endPoint, content);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<List<Persona>> ListarEstado(string token)
        {
            endPoint = "api/ListarPersonasEstado";
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

        public async Task<List<Persona>> ListarPersona(string token)
        {
            endPoint = "api/ListarPersonas";
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

        public async Task<List<Persona>> ListarPorNombre(string nombre, string token)
        {
            string endPoint = $"api/ListarPersonasPorNombre/{nombre}";
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
        public async Task<bool> Modificar(Persona persona, int id, string token)
        {
            bool sw = false;
            endPoint = url + $"api/ModificarPersona/{id}";
            string jsonBody = JsonConvert.SerializeObject(persona);
            clients.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            HttpResponseMessage respuesta = await clients.PutAsync(endPoint, content);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<PerTelDir> ObtenerPorId(int id, string token)
        {
            string idper = id.ToString();
            endPoint = url + $"api/ObtenerAll/{id}";
            clients.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var responseTask1 = clients.GetAsync(endPoint);
            var responseTask2 = clients.GetAsync($"{baseurl}{APIs.obteneruser}{idper}");

            await Task.WhenAll(responseTask1, responseTask2);

            var response1 = await responseTask1;
            var response2 = await responseTask2;
            PerTelDir result = new PerTelDir();
            if (response1.IsSuccessStatusCode && response2.IsSuccessStatusCode)
            {
                using (var stream1 = await response1.Content.ReadAsStreamAsync())
                using (var stream2 = await response2.Content.ReadAsStreamAsync())
                using (var reader1 = new StreamReader(stream1))
                using (var reader2 = new StreamReader(stream2))
                {
                    var respuestaCuerpo1 = await reader1.ReadToEndAsync();
                    var respuestaCuerpo2 = await reader2.ReadToEndAsync();

                    result = JsonConvert.DeserializeObject<PerTelDir>(respuestaCuerpo1);
                    var usuario = JsonConvert.DeserializeObject<RegistrarUsuario>(respuestaCuerpo2);
                    usuario.Password = "";
                    result.registrarUsuario = usuario;

                }
            }

            return result;
        }

        public async Task<bool> InsertarAll(PerTelDir registroperteldir, string token)
        {
            try
            {
                bool sw = false;
                endPoint = url + "api/InsertarAllPersona";
                string jsonBody = JsonConvert.SerializeObject(registroperteldir);
                clients.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                var respuesta = await clients.PostAsync(endPoint, content);
                if (respuesta.IsSuccessStatusCode)
                {
                    var responseBody = await respuesta.Content.ReadAsStringAsync();
                    var jsonObject = JObject.Parse(responseBody);
                    var idInsertado = jsonObject["Idinsertado"].ToString();
                    if (idInsertado!=null)
                    {
                        registroperteldir.registrarUsuario.Id = idInsertado;
                        using (var client = new HttpClient())
                        {
                            var url = $"{baseurl}{APIs.insertaruser}";

                            var serializedStr = JsonConvert.SerializeObject(registroperteldir.registrarUsuario);
                            var response = await client.PostAsync(url, new StringContent(serializedStr, Encoding.UTF8, "application/json"));
                            if (response.IsSuccessStatusCode)
                            {
                                sw = true;
                            }
                        }
                    }
                }
                return sw;
            }
            catch (Exception)
            {
                return false;
            }
            
        }
    }
}
