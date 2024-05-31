
namespace Comun.Sipro.Dto
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class SiproRolDto
    {
      
        public string IdRol { get; set; }

        public string DescripcionRol { get; set; }

        public decimal Vigente { get; set; }

        public DateTime FechaCreacion { get; set; }

        public string UsuarioCreacion { get; set; }

        public string MaquinaCreacion { get; set; }
    }
}
