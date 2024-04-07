using Coling.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Vista.Servicios.Afiliados
{
    public class TipoSocialService : ITipoSocialService
    {
        string url = "http://localhost:7102/";
        string endPoint = "";
        private readonly HttpClient clients;

        public TipoSocialService(HttpClient clients)
        {
            this.clients = clients;
            this.clients.BaseAddress = new Uri(url);
        }

        public async Task<bool> Eliminar(int id, string token)
        {
            bool sw = false;
            endPoint = url + $"api/EliminarTipoSocial/{id}";
            clients.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage respuesta = await clients.DeleteAsync(endPoint);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<bool> Insertar(TipoSocial tipoSocial, string token)
        {
            bool sw = false;
            endPoint = url + "api/InsertarTipoSocial";
            string jsonBody = JsonConvert.SerializeObject(tipoSocial);
            clients.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            HttpResponseMessage respuesta = await clients.PostAsync(endPoint, content);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<List<TipoSocial>> ListarEstado(string token)
        {
            endPoint = "api/ListarTipoSocialEstado";
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

        public async Task<List<TipoSocial>> ListarTipoSocial(string token)
        {
            endPoint = "api/ListarTipoSocial";
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

        public async Task<List<TipoSocial>> ListarPorNombre(string nombre, string token)
        {
            string endPoint = $"api/ListarTipoSocialPorNombre/{nombre}";
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
        public async Task<bool> Modificar(TipoSocial tipoSocial, int id, string token)
        {
            bool sw = false;
            endPoint = url + $"api/ModificarTipoSocial/{id}";
            string jsonBody = JsonConvert.SerializeObject(tipoSocial);
            clients.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            HttpResponseMessage respuesta = await clients.PutAsync(endPoint, content);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<TipoSocial> ObtenerPorId(int id, string token)
        {
            endPoint = url + $"api/ObtenerTipoSocial/{id}";
            clients.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await clients.GetAsync(endPoint);
            TipoSocial result = new TipoSocial();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<TipoSocial>(respuestaCuerpo);
            }
            return result;
        }
    }
}
