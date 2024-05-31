namespace Datos.Sipro
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SIPRO_BITACORA", Schema = "USR_SATDE")]
    public class SiproBitacora
    {
        #region Propiedades
        [Column("ID_BITACORA")]
        [Key]
        public string IdBitacora { get; set; }
        [Column("ID_PROYECTO")]
        public string IdProyecto { get; set; }
        [Column("ID_OBSERVACION")]
        public string IdObservacion { get; set; }
        [Column("ID_FASE")]
        public string IdFase { get; set; }
        [Column("DESCRIPCION")]
        public string Descripcion { get; set; }
        [Column("FECHA_FIN")]
        public DateTime FechaFin { get; set; }
        [Column("FECHA_INICIO")]
        public DateTime FechaInicio { get; set; }
        [Column("FECHA_CREACION")]
        public DateTime FechaCreacion { get; set; }
        [Column("USUARIO_CREACION")]
        public string UsuarioCreacion { get; set; }
        [Column("MAQUINA_CREACION")]
        public string MaquinaCreacion { get; set; }
        [Column("VIGENTE")]
        public decimal Vigente { get; set; }

        #region Propiedades de Referencia
        [ForeignKey("IdProyecto")]
        public virtual SiproProyecto ProyectoBitacora { get; set; }
        [ForeignKey("IdFase")]
        public virtual SiproFases FaseBitacora { get; set; }
        [ForeignKey("IdObservacion")]
        public virtual SiproObservaciones ObservacionBitacotra { get; set; }
        #endregion
        #endregion
    }
}
