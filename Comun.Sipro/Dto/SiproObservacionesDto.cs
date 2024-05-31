namespace Comun.Sipro.Dto
{
    using System;

    public class SiproObservacionesDto
    {
        #region Propiedades
        public string IdObservacion { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string MaquinaCreacion { get; set; }
        public decimal Vigente { get; set; }
        public string IdProyecto { get; set; }
        #endregion

    }
}
