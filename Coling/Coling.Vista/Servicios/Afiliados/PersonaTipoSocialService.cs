using Coling.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Vista.Servicios.Afiliados
{
    public class PersonaTipoSocialService : IPersonaTipoSocialService
    {
        string url = "http://localhost:7102/";
        string endPoint = "";
        private readonly HttpClient clients;

        public PersonaTipoSocialService(HttpClient clients)
        {
            this.clients = clients;
            this.clients.BaseAddress = new Uri(url);
        }

        public async Task<bool> Eliminar(int id, string token)
        {
            bool sw = false;
            endPoint = url + $"api/EliminarPersonaTipoSocial/{id}";
            clients.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage respuesta = await clients.DeleteAsync(endPoint);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<bool> Insertar(PersonaTipoSocial personaTipoSocial, string token)
        {
            bool sw = false;
            endPoint = url + "api/InsertarPersonaTipoSocial";
            string jsonBody = JsonConvert.SerializeObject(personaTipoSocial);
            clients.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            HttpResponseMessage respuesta = await clients.PostAsync(endPoint, content);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<List<PersonaTipoSocial>> ListarEstado(string token)
        {
            endPoint = "api/ListarPersonaTipoSocialEstado";
            clients.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await clients.GetAsync(endPoint);
            List<PersonaTipoSocial> result = new List<PersonaTipoSocial>();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<PersonaTipoSocial>>(respuestaCuerpo);
            }
            return result;
        }

        public async Task<List<PersonaTipoSocial>> ListarPersonaTipoSocial(string token)
        {
            endPoint = "api/ListarPersonaTipoSocial";
            clients.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await clients.GetAsync(endPoint);
            List<PersonaTipoSocial> result = new List<PersonaTipoSocial>();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<PersonaTipoSocial>>(respuestaCuerpo);
            }
            return result;
        }

        public async Task<List<PersonaTipoSocial>> ListarPorNombre(string nombre, string token)
        {
            string endPoint = $"api/ListarPersonaTipoSocialPorNombre/{nombre}";
            clients.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await clients.GetAsync(endPoint);
            List<PersonaTipoSocial> result = new List<PersonaTipoSocial>();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<PersonaTipoSocial>>(respuestaCuerpo);
            }
            return result;
        }
        public async Task<bool> Modificar(PersonaTipoSocial personaTipoSocial, int id, string token)
        {
            bool sw = false;
            endPoint = url + $"api/ModificarPersonaTipoSocial/{id}";
            string jsonBody = JsonConvert.SerializeObject(personaTipoSocial);
            clients.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            HttpResponseMessage respuesta = await clients.PutAsync(endPoint, content);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<PersonaTipoSocial> ObtenerPorId(int id, string token)
        {
            endPoint = url + $"api/ObtenerPersonaTipoSocial/{id}";
            clients.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await clients.GetAsync(endPoint);
            PersonaTipoSocial result = new PersonaTipoSocial();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<PersonaTipoSocial>(respuestaCuerpo);
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

        public async Task<List<TipoSocial>> ListarTipoSocial(string token)
        {
            endPoint = "api/ListarTipoSocialEstadoActivo";
            clients.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await clients.GetAsync(endPoint);
            List<TipoSocial> result = new List<TipoSocial>();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<TipoSocial>>(respuestaCuerpo);
            }
            return result;
        }
    }
}
