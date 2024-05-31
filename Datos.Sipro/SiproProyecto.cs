namespace Datos.Sipro
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SIPRO_PROYECTO", Schema = "USR_SATDE")]
    public class SiproProyecto
    {
        #region Propiedades
        [Column("ID_PROYECTO")]
        [Key]
        public string IdProyecto { get; set; }

        [Column("NOMBRE")]
        public string Nombre { get; set; }

        [Column("FECHA_INICIO")]
        public DateTime FechaInicio { get; set; }

        [Column("ID_ESTADO")]
        public string IdEstado { get; set; }

        [Column("SIGLA_UNIDAD_RESPONSABLE")]
        public string SiglaUnidadResponsable { get; set; }

        [Column("ID_UNIDAD_RESPONSABLE")]
        public Nullable<decimal> IdUnidadResponsable { get; set; }

        [Column("FECHA_CREACION")]
        public DateTime FechaCreacion { get; set; }

        [Column("USUARIO_CREACION")]
        public string UsuarioCreacion { get; set; }

        [Column("MAQUINA_CREACION")]
        public string MaquinaCreacion { get; set; }

        [Column("VIGENTE")]
        public decimal Vigente { get; set; }

        [Column("ACRONIMO")]
        public string Acronimo { get; set; }
        [Column("ID_UNIDAD_SOLICITANTE")]
        public Nullable<decimal> IdUnidadSolicitante { get; set; }
        [Column("SIGLA_UNIDAD_SOLICITANTE")]
        public string SiglaUnidadSolicitante { get; set; }
        [Column("IDENTIFICACION")]
        public decimal Identificacion { get; set; }

        #region Propiedades de Referencia

        [ForeignKey("IdEstado")]
        public virtual SiproEstados EstadosProyecto { get; set; }

        #endregion
        #endregion
    }
}
