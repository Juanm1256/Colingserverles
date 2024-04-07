using Coling.Vista.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Vista.Servicios.Autentificacion
{
    public interface IAppService
    {
        Task<string> AuthenticateUser(LoginModel loginModel);
    }
}
