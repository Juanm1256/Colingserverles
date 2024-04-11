using Coling.Shared;
using Coling.Shared.DTOs;
using Coling.Vista.Modelos;
using Coling.Vista.Modelos.DTOs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Vista.Servicios.Curriculum
{
    public class IntitucionServices : IInstitucionService
    {
        private string url = "http://localhost:7015/";
        private string baseurl = "http://localhost:7276/";
        private readonly HttpClient client;

        public IntitucionServices( HttpClient client)
        {
            this.client = client;
            this.client.BaseAddress = new Uri(url);
        }

        public async Task<List<Institucion>> ListarInstitucion(string token)
        {
            string endPoint = "api/ListarInstitucion";
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

        public async Task<List<Institucion>> ListarEstado(string token)
        {
            string endPoint = "api/ListarInstitucionEstado";
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
        
        public async Task<List<Institucion>> ListarInstitucionEstadoActivo(string token)
        {
            string endPoint = "api/ListarInstitucionEstadoActivo";
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

        public async Task<List<Institucion>> ListarPorNombre(string nombre, string token)
        {
            string endPoint = url + $"api/ListarInstitucionPorNombre/{nombre}";

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


        public async Task<bool> InsertarInstitucion(InstituUser instituuser, string token)
        {
            bool sw = false;
            string endPoint = url + "api/InsertarInstitucion";
            string jsonBody = JsonConvert.SerializeObject(instituuser.institucion);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            HttpResponseMessage respuesta = await client.PostAsync(endPoint, content);
            if (respuesta.IsSuccessStatusCode)
            {
                var responseBody = await respuesta.Content.ReadAsStringAsync();
                var jsonObject = JObject.Parse(responseBody);
                var idInsertado = jsonObject["Idinsertado"];
                if (idInsertado != null)
                {
                    instituuser.registrarUsuario.Id = idInsertado.ToString();
                    instituuser.registrarUsuario.Rol = "Institucion";
                    instituuser.registrarUsuario.Estado = "Activo";
                    using (var clients = new HttpClient())
                    {
                        var url = $"{baseurl}{APIs.insertaruser}";

                        var serializedStr = JsonConvert.SerializeObject(instituuser.registrarUsuario);
                        var response = await clients.PostAsync(url, new StringContent(serializedStr, Encoding.UTF8, "application/json"));
                        if (response.IsSuccessStatusCode)
                        {
                            sw = true;
                        }
                    }
                }
            }
            return sw;
        }

        public async Task<bool> ModificarInstitucion(Institucion institucion, string token)
        {
            bool sw = false;
            string endPoint = url + "api/ModificarInstitucion";
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
            string endPoint = url + $"api/eliminarInstitucion/{idInstitucion}";
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
            string endPoint = url + $"api/obtenerInstitucion/{rowkey}";
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

        public async Task<RegistrarUsuario> ObteneruserPorId(string id, string token)
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var responseTask2 = client.GetAsync($"{baseurl}{APIs.obteneruser}{id}");

            await Task.WhenAll(responseTask2);

            var response2 = await responseTask2;
            RegistrarUsuario usuario = new RegistrarUsuario();
            if (response2.IsSuccessStatusCode)
            {
                using (var stream2 = await response2.Content.ReadAsStreamAsync())
                using (var reader2 = new StreamReader(stream2))
                {
                    var respuestaCuerpo2 = await reader2.ReadToEndAsync();

                    usuario = JsonConvert.DeserializeObject<RegistrarUsuario>(respuestaCuerpo2);
                    usuario.Password = "";

                }
            }

            return usuario;
        }
    }
}
