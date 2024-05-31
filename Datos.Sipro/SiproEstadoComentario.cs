namespace Datos.Sipro
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SIPRO_ESTADO_COMENTARIO", Schema = "USR_SATDE")]
    public class SiproEstadoComentario
    {
        #region Propiedades
        [Column("ID_ESTADO_COMENTARIO")]
        [Key]
        public string IdEstadoComentario { get; set; }
        [Column("DESCRIPCION")]
        public string Descripcion { get; set; }
        [Column("USUARIO_CREACION")]
        public string UsuarioCreacion { get; set; }
        [Column("MAQUINA_CREACION")]
        public string MaquinaCreacion { get; set; }
        [Column("FECHA_CREACION")]
        public DateTime FechaCreacion { get; set; }
        [Column("VIGENTE")]
        public decimal Vigente { get; set; }
        [Column("ID_RESPONSABLE")]
        public string IdResponsable { get; set; }
        [Column("ID_COMENTARIO")]
        public string IdComentario { get; set; }
        [Column("ID_ESTADO")]   
        public string IdEstado { get; set; }
        [Column("ID_TIPO_RESPONSABILIDAD")]
        public string IdTipoResponsabilidad { get; set; }
        [Column("CONSECUTIVO")]
        public decimal Consecutivo { get; set; }
        [Column("UNDE_CONSECUTIVO")]
        public decimal UndeConsecutivo { get; set; }
        [Column("UNDE_FUERZA")]
        public decimal UndeFuerza { get; set; }
        [Column("IDENTIFICACION")]
        public decimal Identificacion { get; set; }
        [ForeignKey("IdResponsable")]
        public SiproResponsable ResponsableEstadoComentario { get; set; }
        [ForeignKey("IdComentario")]
        public SiproComentario ComentarioEstadoComentario { get; set; }
        [ForeignKey("IdEstado")]
        public SiproEstados EstadoComentarioEstado { get; set; }
        #endregion
    }
}
