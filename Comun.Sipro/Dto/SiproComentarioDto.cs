namespace Comun.Sipro.Dto
{
    using System;
    using System.Collections.Generic;

    public class SiproComentarioDto
    {
        #region Propiedades
        public string IdCometario { get; set; }
        public string DescripcionComentario { get; set; }
        public string UsuarioComentario { get; set; }
        public decimal Consecutivo { get; set; }
        public decimal UndeConsecutivo { get; set; }
        public decimal UndeFuerza { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string FechaCreacion2 { get; set; }
        public string UsuarioCreacion { get; set; }
        public string MaquinaCreacion { get; set; }
        public string IdEvidencia { get; set; }
        public decimal Vigente { get; set; }
        public decimal Identificacion { get; set; }
        public string IdTipoResponsabilidad { get; set; }
        public string IdFuncionarioEnvia { get; set; }

        public string IdProyecto { get; set; }

        public decimal  idUnidadEnvia { get; set; }

        public decimal Estados { get; set; }

        public string Estados2 { get; set; }
        
        public string Grado { get; set; }

        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Unidad { get; set; }

        public decimal Trazabilidad { get; set; }

        public string IdEstado { get; set; }


        #endregion
    }

    public enum EnumEstadosComentario
    {
        Revision = 2,
        Modificacion = 3,
        Aprobado = 4
    }
}
