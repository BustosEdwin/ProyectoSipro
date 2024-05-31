namespace Datos.Sipro
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SIPRO_RESPONSABLE", Schema = "USR_SATDE")]
    public class SiproResponsable
    {
        #region Propiedades

        [Column("ID_RESPONSABLE")]
        [Key]
        public string IdResponsable { get; set; }
        [Column("ID_PROYECTO")]
        public string IdProyecto { get; set; }
        [Column("GRADO")]
        public string Grado { get; set; }
        [Column("NOMBRES")]
        public string Nombres { get; set; }
        [Column("APELLIDOS")]
        public string Apellidos { get; set; }
        [Column("CARGO")]
        public string Cargo { get; set; }
        [Column("ID_UNIDAD")]
        public Nullable<decimal> IdUnidad { get; set; }
        [Column("ID_TIPO_RESPONSABLE")]
        public string IdTipoResponsable { get; set; }
        [Column("FECHA_ASIGNACION")]
        public DateTime FechaAsignacion { get; set; }
        [Column("IDENTIFICACION")]
        public decimal Identificacion { get; set; }
        [Column("UNDE_CONSECUTIVO")]
        public Nullable<decimal> UndeConsecutivo { get; set; }
        [Column("UNDE_FUERZA")]
        public Nullable<decimal> UndeFuerza { get; set; }
        [Column("CONSECUTIVO")]
        public Nullable<decimal> Consecutivo { get; set; }

        [Column("FECHA_CREACION")]
        public DateTime FechaCreacion { get; set; }
        [Column("USUARIO_CREACION")]
        public string UsuarioCreacion { get; set; }
        [Column("MAQUINA_CREACION")]
        public string MaquinaCreacion { get; set; }
        [Column("VIGENTE")]
        public decimal Vigente { get; set; }

        [Column("FECHA_FIN")]
        public DateTime? FechaFin { get; set; }

        [Column("ACTIVO")]
        public bool? Activo { get; set; }

        [Column("OBSERVACIONES")]
        public string Observaciones { get; set; }

        #region Propiedades de Referencia
        [ForeignKey("IdProyecto")]
        public virtual SiproProyecto ProyectoResponsable { get; set; }
        [ForeignKey("IdTipoResponsable")]
        public virtual SiproTipoResponsabilidad ResponsableTipoResponsabilidad{ get; set; }
        #endregion
        #endregion
    }
}
