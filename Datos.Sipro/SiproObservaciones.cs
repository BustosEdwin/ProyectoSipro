namespace Datos.Sipro
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("SIPRO_OBSERVACIONES", Schema = "USR_SATDE")]
    public class SiproObservaciones
    {
        #region Propiedades
        [Column("ID_OBSERVACION")]
        [Key]
        public string IdObservacion { get; set; }
        [Column("DESCRIPCION")]
        public string Descripcion { get; set; }
        [Column("FECHA_CREACION")]
        public DateTime FechaCreacion { get; set; }
        [Column("USUARIO_CREACION")]
        public string UsuarioCreacion { get; set; }
        [Column("MAQUINA_CREACION")]
        public string MaquinaCreacion { get; set; }
        [Column("VIGENTE")]
        public decimal Vigente { get; set; }
        [Column("ID_PROYECTO")]
        public string IdProyecto { get; set; }

        #region Propiedades de Referencias 
        [Column("IdProyecto")]
        public virtual SiproProyecto ProyectoObservaciones { get; set; }
        #endregion
        #endregion
    }
}
