using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Shared
{
    public class RegistrarUsuario
    {
        public string Id { get; set; }
        
        public string UserName { get; set; }
        
        public string Password { get; set; }
        
        public string Rol { get; set; }

        public string Estado { get; set; }
    }
}
