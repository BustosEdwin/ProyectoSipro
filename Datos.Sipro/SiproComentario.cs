namespace Datos.Sipro
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;

    [Table("SIPRO_COMENTARIOS", Schema = "USR_SATDE")]
    public class SiproComentario
    {
        [Column("ID_COMENTARIO")]
        [Key]
        public string IdCometario { get; set; }
        [Column("DESCRIPCION_COMENTARIO")]
        public string DescripcionComentario { get; set; }
        [Column("USUARIO_COMENTARIO")]
        public string UsuarioComentario { get; set; }
        [Column("CONSECUTIVO")]
        public decimal Consecutivo { get; set; }
        [Column("UNDE_CONSECUTIVO")]
        public decimal UndeConsecutivo { get; set; }
        [Column("UNDE_FUERZA")]
        public decimal UndeFuerza { get; set; }
        [Column("FECHA_CREACION")]
        public DateTime FechaCreacion { get; set; }
        [Column("USUARIO_CREACION")]
        public string UsuarioCreacion { get; set; }
        [Column("MAQUINA_CREACION")]
        public string MaquinaCreacion { get; set; }
        [Column("VIGENTE")]
        public decimal Vigente { get; set; }

        [Column("ID_EVIDENCIA")]
        public string IdEvidencia { get; set; }
        //[ForeignKey("IdEvidencia")]

        [Column("IDENTIFICACION")]
        public decimal Identificacion { get; set; }
        [Column("ID_TIPO_RESPONSABILIDAD")]
        public string IdTipoResponsabilidad { get; set; }
        [Column("ID_ESTADO")]
        public string IdEstado { get; set; }
        //[ForeignKey("IdEvidencia")]
        public virtual SiproEvidencia SiproEvidencia { get; set; }


    }
}
