﻿using Coling.Vista.Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Vista.Servicios.Curriculum
{
    public class EstudioService : IEstudioService
    {
        string url = "http://localhost:7015/";
        string endPoint = "";
        private readonly HttpClient client;

        public EstudioService(HttpClient client)
        {
            this.client = client;
            this.client.BaseAddress = new Uri(url);
        }

        public async Task<bool> Eliminar(string id, string token)
        {
            bool sw = false;
            endPoint = url + $"api/eliminarEstudio/{id}";
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage respuesta = await client.DeleteAsync(endPoint);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<bool> Insertar(Estudio estudio, string token)
        {
            bool sw = false;
            endPoint = url + "api/InsertarEstudio";
            string jsonBody = JsonConvert.SerializeObject(estudio);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            HttpResponseMessage respuesta = await client.PostAsync(endPoint, content);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<List<Estudio>> Listar(string token)
        {
            endPoint = "api/ListarEstudio";
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await client.GetAsync(endPoint);
            List<Estudio> result = new List<Estudio>();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<Estudio>>(respuestaCuerpo);
            }
            return result;
        }

        public async Task<List<Estudio>> ListarEstado(string token)
        {
            string endPoint = "api/ListarEstudioEstado";
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await client.GetAsync(endPoint);
            List<Estudio> result = new List<Estudio>();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<Estudio>>(respuestaCuerpo);
            }
            return result;
        }

        public async Task<bool> Modificar(Estudio estudio, string token)
        {
            bool sw = false;
            endPoint = url + "api/ModificarEstudio";
            string jsonBody = JsonConvert.SerializeObject(estudio);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            HttpResponseMessage respuesta = await client.PutAsync(endPoint, content);
            if (respuesta.IsSuccessStatusCode)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<Estudio> ObtenerPorId(string id, string token)
        {
            endPoint = url + $"api/obtenerEstudio/{id}";
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await client.GetAsync(endPoint);
            Estudio estudio = new Estudio();
            if (response.IsSuccessStatusCode)
            {
                string respuestaCuerpo = await response.Content.ReadAsStringAsync();
                estudio = JsonConvert.DeserializeObject<Estudio>(respuestaCuerpo);
            }
            return estudio;
        }
    }
}
