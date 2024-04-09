using Coling.Shared;
using Coling.Shared.DTOs;
using Coling.Vista.Modelos;
using Newtonsoft.Json;
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

        public async Task<Persona> ObtenerPorId(int id, string token)
        {
            endPoint = url + $"api/ObtenerPersona/{id}";
            clients.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await clients.GetAsync(endPoint);
            Persona result = new Persona();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<Persona>(respuestaCuerpo);
            }
            return result;
        }

        public async Task<bool> InsertarAll(PerTelDir registroperteldir, string token)
        {
            bool sw = false;
            endPoint = url + "api/InsertarAllPersona";
            string jsonBody = JsonConvert.SerializeObject(registroperteldir);
            clients.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            var respuesta = await clients.PostAsync(endPoint, content);
            if (respuesta != null)
            {
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
            return sw;
        }
    }
}
