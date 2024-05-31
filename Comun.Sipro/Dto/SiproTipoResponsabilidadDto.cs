using System;

namespace Comun.Sipro.Dto
{
    public class SiproTipoResponsabilidadDto
    {
        #region Propiedades
        public string IdTipoResponsabilidad { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string MaquinaCreacion { get; set; }
        public decimal Vigente { get; set; }
        #endregion
    }
}
