namespace Comun.Sipro.Dto
{
    using System;
    public class SiproUsuarioRolDto
    {
        #region Propiedades
        public string IdUsuarioRol { get; set; }
        public string IdUsuario { get; set; }
        public string IdRol { get; set; }
        public decimal Vigente { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string MaquinaCreacion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        public string DescripcionRol { get; set; }

        public decimal Identificacion { get; set; }
        public string usuario { get; set; }

        public decimal Consecutivo { get; set; }
        public decimal UndeConsecutivo { get; set; }
        public decimal UndeFuerza { get; set; }

      



        #endregion

    }
}
