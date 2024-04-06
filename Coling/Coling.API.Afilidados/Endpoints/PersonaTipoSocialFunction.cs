using Coling.API.Afilidados.Contratos;
using Coling.API.Afilidados.Implementaciones;
using Coling.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Net;

namespace Coling.API.Afilidados.Endpoints
{
    public class PersonaTipoSocialFunction
    {
        private readonly ILogger<PersonaTipoSocialFunction> _logger;
        private readonly IPersonatiposocial personatiposocialLogic;

        public PersonaTipoSocialFunction(ILogger<PersonaTipoSocialFunction> logger, IPersonatiposocial personatiposocialLogic)
        {
            _logger = logger;
            this.personatiposocialLogic = personatiposocialLogic;
        }

        [Function("ListarPersonaTipoSocial")]
        [OpenApiOperation("listarPersonaTipoSocial", "PersonaTipoSocial")]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(List<PersonaTipoSocial>))]
        public async Task<HttpResponseData> ListarPersonaTipoSocial([HttpTrigger(AuthorizationLevel.Function, "get", Route = "listarpersonatiposocials")] HttpRequestData req)
        {
            try
            {
                var listapersonatiposocials = personatiposocialLogic.ListarPersonaTipoSocialTodos();
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(listapersonatiposocials.Result);
                return respuesta;
            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }

        }

        [Function("InsertarPersonaTipoSociales")]
        [OpenApiOperation("insertarPersonaTipoSocial", "PersonaTipoSocial")]
        [OpenApiRequestBody("application/json", bodyType: typeof(PersonaTipoSocial))]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(PersonaTipoSocial))]
        public async Task<HttpResponseData> InsertarPersonaTipoSociales([HttpTrigger(AuthorizationLevel.Function, "post", Route = "insertarpersonatiposocial")] HttpRequestData req)
        {
            try
            {
                var per = await req.ReadFromJsonAsync<PersonaTipoSocial>() ?? throw new Exception("Debe ingresar una personatiposocial con todos sus datos");
                bool seGuardo = await personatiposocialLogic.InsertarPersonaTipoSocial(per);
                if (seGuardo)
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.OK);
                    return respuesta;
                }
                return req.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }

        }

        [Function("EliminarPersonaTipoSociales")]
        [OpenApiOperation("eliminarPersonaTipoSocial", "PersonaTipoSocial")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Type = typeof(int))]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(PersonaTipoSocial))]
        public async Task<HttpResponseData> EliminarPersonaTipoSociales([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "eliminarPersonaTipoSociales/{id}")] HttpRequestData req, int id)
        {
            try
            {
                var personatiposocial = await personatiposocialLogic.EliminarPersonaTipoSocial(id);
                if (personatiposocial != null)
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.OK);
                    await respuesta.WriteAsJsonAsync(personatiposocial);
                    return respuesta;
                }
                return req.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }

        }

        [Function("ObtenerPersonaTipoSociales")]
        [OpenApiOperation("obtenerPersonaTipoSocial", "PersonaTipoSocial")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Type = typeof(int))]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(PersonaTipoSocial))]
        public async Task<HttpResponseData> ObtenerPersonaTipoSociales([HttpTrigger(AuthorizationLevel.Function, "get", Route = "obtenerPersonaTipoSociales/{id}")] HttpRequestData req, int id)
        {
            try
            {
                var listapersonatiposocials = personatiposocialLogic.ObtenerPersonaTipoSocialById(id);
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(listapersonatiposocials.Result);
                return respuesta;
            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }

        }

        [Function("ModificarPersonaTipoSociales")]
        [OpenApiOperation("modificarPersonaTipoSocial", "PersonaTipoSocial")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Type = typeof(int))]
        [OpenApiRequestBody("application/json", bodyType: typeof(PersonaTipoSocial))]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", bodyType: typeof(PersonaTipoSocial))]
        public async Task<HttpResponseData> ModificarPersonaTipoSociales([HttpTrigger(AuthorizationLevel.Function, "put", Route = "modificarPersonaTipoSociales/{id}")] HttpRequestData req, int id)
        {
            try
            {
                var per = await req.ReadFromJsonAsync<PersonaTipoSocial>() ?? throw new Exception("Debe ingresar una personatiposocial con todos sus datos");

                bool seGuardo = await personatiposocialLogic.ModificarPersonaTipoSocial(per, id);
                if (seGuardo)
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.OK);
                    return respuesta;
                }
                return req.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }

        }
    }
}
