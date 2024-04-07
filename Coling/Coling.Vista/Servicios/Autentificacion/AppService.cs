using Coling.Vista.Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Vista.Servicios.Autentificacion
{
    public class AppService : IAppService
    {
        private string baseurl = "http://localhost:7276/";
        public async Task<string> AuthenticateUser(LoginModel loginModel)
        {
            string returnStr = string.Empty;
            using (var client = new HttpClient())
            {
                var url = $"{baseurl}{APIs.Authenticateuser}";

                var serializedStr = JsonConvert.SerializeObject(loginModel);
                var response = await client.PostAsync(url, new StringContent(serializedStr, Encoding.UTF8,"application/json"));
                if (response.IsSuccessStatusCode)
                {
                    returnStr = await response.Content.ReadAsStringAsync();
                }
            }
            return returnStr;
        }
    }
}
