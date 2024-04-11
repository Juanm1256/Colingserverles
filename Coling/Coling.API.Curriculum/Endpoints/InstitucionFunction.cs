using Coling.API.Curriculum.Contrato.Repositorios;
using Coling.API.Curriculum.Modelo;
using Coling.Shared;
using Coling.Utilitarios.Attributes;
using Coling.Utilitarios.Roles;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Net;

namespace Coling.API.Curriculum.Endpoints
{
    public class InstitucionFunction
    {
        private readonly ILogger<InstitucionFunction> _logger;
        private readonly IInstitucionRepositorio repos;

        public InstitucionFunction(ILogger<InstitucionFunction> logger, IInstitucionRepositorio repos)
        {
            _logger = logger;
            this.repos = repos;
        }

        [Function("InsertarInstitucion")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("Insertarspec", "Institucion", Description = " Sirve para listar todas las instituciones")]
        [OpenApiRequestBody("application/json", typeof(Institucion), Description = "Institucion modelo")]
        [OpenApiResponseWithBody(statusCode:HttpStatusCode.OK, contentType:"application/json", bodyType: typeof(Institucion), Description = "Insertara la institicon.")]
        public async Task<HttpResponseData> InsertarInstitucion([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            try
            {
                var registro = await req.ReadFromJsonAsync<Institucion>() ?? throw new Exception("Debe ingresar una institucion con todos sus datos");
                registro.RowKey=Guid.NewGuid().ToString();
                registro.Timestamp=DateTime.UtcNow;
                string sw = await repos.Insertar(registro);
                if (!string.IsNullOrWhiteSpace(sw))
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.OK);
                    respuesta.WriteAsJsonAsync(new { Idinsertado = sw });
                    return respuesta;
                }
                else
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.BadRequest);
                    return respuesta;
                }

            }
            catch (Exception)
            {

                var respuesta = req.CreateResponse(HttpStatusCode.InternalServerError);
                return respuesta;
            }
        }
        [Function("ListarInstitucion")]
        [ColingAuthorize(AplicacionRoles.Admin+","+AplicacionRoles.Afiliado + "," + AplicacionRoles.Secretaria)]
        [OpenApiOperation("Listarspec", "Institucion", Description = " Sirve para listar todas las instituciones")]
        [OpenApiResponseWithBody(statusCode:HttpStatusCode.OK, contentType:"application/json", bodyType: typeof(List<Institucion>), Description = "Mostrar una lista de instituciones")]
        public async Task<HttpResponseData> ListarInstitucion([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            try
            {
                var lista = repos.Getall();
                var respuest = req.CreateResponse(HttpStatusCode.OK);
                await respuest.WriteAsJsonAsync(lista.Result);
                return respuest;

            }
            catch (Exception)
            {

                var respuesta = req.CreateResponse(HttpStatusCode.InternalServerError);
                return respuesta;
            }
        }

        [Function("ListarInstitucionEstado")]
        [ColingAuthorize(AplicacionRoles.Admin + "," + AplicacionRoles.Afiliado + "," + AplicacionRoles.Secretaria)]
        [OpenApiOperation("Listarstadospec", "Institucion", Description = " Sirve para listar todas las instituciones")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<Institucion>), Description = "Mostrar una lista de instituciones")]
        public async Task<HttpResponseData> ListarInstitucionEstado([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            try
            {
                var lista = repos.Getallstatus();
                var respuest = req.CreateResponse(HttpStatusCode.OK);
                await respuest.WriteAsJsonAsync(lista.Result);
                return respuest;

            }
            catch (Exception)
            {

                var respuesta = req.CreateResponse(HttpStatusCode.InternalServerError);
                return respuesta;
            }
        }

        [Function("ListarInstitucionPorNombre")]
        [ColingAuthorize(AplicacionRoles.Admin + "," + AplicacionRoles.Afiliado + "," + AplicacionRoles.Secretaria)]
        [OpenApiParameter(name: "nombre", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "Obtener por id", Description = "Obtener")]
        [OpenApiOperation("Listarstadospec", "Institucion", Description = " Sirve para listar todas las instituciones")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<Institucion>), Description = "Mostrar una lista de instituciones")]
        public async Task<HttpResponseData> ListarInstitucionPorNombre([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ListarInstitucionPorNombre/{nombre}")] HttpRequestData req, string nombre)
        {
            try
            {
                var lista = repos.ListarPorNombre(nombre);
                var respuest = req.CreateResponse(HttpStatusCode.OK);
                await respuest.WriteAsJsonAsync(lista.Result);
                return respuest;

            }
            catch (Exception)
            {

                var respuesta = req.CreateResponse(HttpStatusCode.InternalServerError);
                return respuesta;
            }
        }


        [Function("ListarInstitucionEstadoActivo")]
        [ColingAuthorize(AplicacionRoles.Admin + "," + AplicacionRoles.Afiliado + "," + AplicacionRoles.Secretaria + "," + AplicacionRoles.Institucion)]
        [OpenApiOperation("ListarInstitucionEstadoActivo", "Institucion")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Institucion))]
        public async Task<HttpResponseData> ListarInstitucionEstadoActivo([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            try
            {
                var lista = await repos.GetallInstitucionstatus();
                var respuest = req.CreateResponse(HttpStatusCode.OK);
                await respuest.WriteAsJsonAsync(lista);
                return respuest;

            }
            catch (Exception)
            {

                var respuesta = req.CreateResponse(HttpStatusCode.InternalServerError);
                return respuesta;
            }
        }

        /*tarea para ahora*/
        [Function("EliminarInstitucion")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("eliminarInstitucion", "Institucion")]
        [OpenApiParameter("rowkey", In =ParameterLocation.Path, Type = typeof(string))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Institucion), Description = "Insertara la institicon.")]
        public async Task<HttpResponseData> EliminarInstitucion([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "eliminarInstitucion/{rowkey}")] HttpRequestData req, string rowkey)
        {
            try
            {
                var lista = repos.Delete(rowkey);
                var respuest = req.CreateResponse(HttpStatusCode.OK);
                await respuest.WriteAsJsonAsync(lista);
                return respuest;

            }
            catch (Exception)
            {

                var respuesta = req.CreateResponse(HttpStatusCode.InternalServerError);
                return respuesta;
            }
        }

        [Function("ObtenerInstitucion")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("obtenerInstitucion", "Institucion", Description = "Obtener Institucion")]
        [OpenApiParameter(name: "rowkey", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "Obtener por id", Description = "Obtener")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Institucion), Description = "Obtenecion")]
        public async Task<HttpResponseData> ObtenerInstitucion([HttpTrigger(AuthorizationLevel.Function, "get", Route = "obtenerInstitucion/{rowkey}")] HttpRequestData req, string rowkey)
        {
            try
            {
                var lista = repos.Get(rowkey);
                var respuest = req.CreateResponse(HttpStatusCode.OK);
                await respuest.WriteAsJsonAsync(lista.Result);
                return respuest;

            }
            catch (Exception)
            {

                var respuesta = req.CreateResponse(HttpStatusCode.InternalServerError);
                return respuesta;
            }
        }

        [Function("ModificarInstitucion")]
        [ColingAuthorize(AplicacionRoles.Admin)]
        [OpenApiOperation("modificarInstitucion", "Institucion")]
        [OpenApiRequestBody("application/json", typeof(Institucion), Description = "Institucion modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Institucion), Description = "Insertara la institicon.")]
        public async Task<HttpResponseData> ModificarInstitucion([HttpTrigger(AuthorizationLevel.Function, "put")] HttpRequestData req)
        {
            try
            {
                var registro = await req.ReadFromJsonAsync<Institucion>() ?? throw new Exception("Debe ingresar una institucion con todos sus datos");
                bool sw = await repos.UpdateIns(registro);
                if (sw)
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.OK);
                    return respuesta;
                }
                else
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.BadRequest);
                    return respuesta;
                }

            }
            catch (Exception)
            {

                var respuesta = req.CreateResponse(HttpStatusCode.InternalServerError);
                return respuesta;
            }
        }
    }
}
