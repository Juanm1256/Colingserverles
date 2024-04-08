using Coling.Autentificacion.Model;
using Coling.Repositorio.Contratos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Extensions;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Coling.Autentificacion
{
    public class AcountFunction
    {
        private readonly ILogger<AcountFunction> _logger;
        private readonly IUsuarioRepositorio usuarioRepositorio;

        public AcountFunction(ILogger<AcountFunction> logger, IUsuarioRepositorio usuarioRepositorio)
        {
            _logger = logger;
            this.usuarioRepositorio = usuarioRepositorio;
        }

        [Function("Login")]
        [OpenApiOperation("Accountspec", "Accont", Description = " Busca las credenciales")]
        [OpenApiRequestBody("application/json", typeof(Credenciales), Description = "Introduzca los datos de credenciales model")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ITokenData), Description = "El token es")]

        public async Task<HttpResponseData> Login([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
        {
            HttpResponseData? respuesta = null;
            var login = await req.ReadFromJsonAsync<Credenciales>() ?? throw new ValidationException("Sus credenciales deben ser completas");
            var tokenFinal = await usuarioRepositorio.VerficarCredenciales(login.UserName, login.Password);
            if (tokenFinal!=null)
            {
                respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteStringAsync(tokenFinal.Token);
            }
            else
            {
                respuesta = req.CreateResponse(HttpStatusCode.Unauthorized);
            }
            return respuesta;
        }

        [Function("Insertar")]
        [OpenApiOperation("Insertspec", "Accont", Description = " Busca las credenciales")]
        [OpenApiRequestBody("application/json", typeof(Registermodel), Description = "Introduzca los datos de credenciales model")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ITokenData), Description = "El token es")]

        public async Task<HttpResponseData> Insertar([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
        {
            HttpResponseData? respuesta = null;
            var login = await req.ReadFromJsonAsync<Registermodel>() ?? throw new ValidationException("Sus credenciales deben ser completas");
            var tokenFinal = await usuarioRepositorio.Insertar(login.Idusuario,login.UserName, login.Password, login.Rol, login.Estado);
            if (tokenFinal != null)
            {
                respuesta = req.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                respuesta = req.CreateResponse(HttpStatusCode.Unauthorized);
            }
            return respuesta;
        }
    }
}
