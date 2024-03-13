using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Shared
{
    public class PersonaTipoSocial
    {
        [Key]
        public int Id { get; set; }
        [Column("IDTIPOSOCIAL")]
        public int Idtiposocial { get; set; }
        [Column("IDPERSONA")]
        public int Idpersona { get; set; }
        public string? Descripcion { get; set; }
        public string? Estado { get; set; }

        [ForeignKey("Idtiposocial")]
        public virtual TipoSocial Idtiposocialnav { get; set; }
        [ForeignKey("Idpersona")]
        public virtual Persona IdPersonanav { get; set; }
    }
}
