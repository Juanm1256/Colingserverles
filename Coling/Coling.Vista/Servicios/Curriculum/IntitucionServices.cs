using Coling.Shared;
using Coling.Vista.Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Vista.Servicios.Curriculum
{
    public class IntitucionServices : IInstitucionService
    {
        string url = "http://localhost:7015";
        string endPoint = "";
        HttpClient client = new HttpClient();

        public async Task<List<Institucion>> ListarInstitucion(string token)
        {
            endPoint = "api/ListarInstitucion";
            client.BaseAddress = new Uri(url);

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await client.GetAsync(endPoint);
            List<Institucion> result = new List<Institucion>();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<Institucion>>(respuestaCuerpo);
            }
            return result;
        }



        public async Task<bool> InsertarInstitucion(Institucion institucion, string token)
        {
            bool sw = false;
            endPoint = url + "/api/InsertarInstitucion";
            string jsonBody = JsonConvert.SerializeObject(institucion);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            HttpResponseMessage respuesta = await client.PostAsync(endPoint, content);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<bool> ModificarInstitucion(Institucion institucion, string token)
        {
            bool sw = false;
            endPoint = url + "/api/ModificarInstitucion";
            string jsonBody = JsonConvert.SerializeObject(institucion);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            HttpResponseMessage respuesta = await client.PutAsync(endPoint, content);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<bool> EliminarInstitucion(string idInstitucion, string token)
        {
            bool sw = false;
            endPoint = url + $"/api/eliminarInstitucion/{idInstitucion}";
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage respuesta = await client.DeleteAsync(endPoint);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<Institucion> ObtenerInstitucionPorId(string rowkey, string token)
        {
            endPoint = url + $"/api/obtenerInstitucion/{rowkey}";
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await client.GetAsync(endPoint);
            Institucion institucion = new Institucion();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                institucion = JsonConvert.DeserializeObject<Institucion>(respuestaCuerpo);
            }
            return institucion;
        }
    }
}
