using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Shared
{
    public class ProfesionAfiliado
    {
        [Key]
        public int Id { get; set; }

        [Column("IDAFILIADO")]
        public int Idafiliado { get; set; }
        public int Idprofesion { get; set; }
        public DateTime fechaasignacion { get; set; }
        public string? Nrosellosib { get; set; }
        public string? Estado { get; set; }

        [ForeignKey("Idafiliado")]
        public virtual Afiliado IdAfiliadonav { get; set; }



    }
}
