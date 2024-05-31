namespace Comun.Sipro
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class SiproEstadoComentarioDto
    {
        public string IdEstadoComentario { get; set; }
        public string Descripcion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string MaquinaCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public decimal Vigente { get; set; }
        public string IdResponsable { get; set; }
        public string IdComentario { get; set; }
        public string IdEstado { get; set; }
        public string IdTipoResponsabilidad { get; set; }
        public decimal Consecutivo { get; set; }
        public decimal UndeConsecutivo { get; set; }
        public decimal UndeFuerza { get; set; }
        public decimal Identificacion { get; set; }

    }
}
