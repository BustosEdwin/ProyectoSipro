namespace Datos.Sipro
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SIPRO_EVIDENCIA", Schema = "USR_SATDE")]
    public class SiproEvidencia
    {
        #region Propiedades 
        [Column("ID_EVIDENCIA")]
        [Key]
        public string IdEvidencia { get; set; }
        [Column("ID_BITACORA")]
        public string IdBitacora { get; set; }
        [Column("ID_OBSERVACION")]
        public string IdObservaciones { get; set; }

        [Column("URL_RUTA")]
        public string UrlRuta { get; set; }
        [Column("ID_RESPONSABLE")]
        public string IdResponsable { get; set; }

        [Column("FECHA_CREACION")]
        public DateTime FechaCreacion { get; set; }
        [Column("USUARIO_CREACION")]
        public string UsuarioCreacion { get; set; }
        [Column("MAQUINA_CREACION")]
        public string MaquinaCreacion { get; set; }
        [Column("VIGENTE")]
        public decimal Vigente { get; set; }

        #region Propiedades de Referencia
        //[ForeignKey("IdBitacora")]
        //public virtual SiproBitacora BitacoraEvidencias { get; set; }
        //[Column("IdObservaciones")]
        //public virtual SiproObservaciones ObservacionEvidencia { get; set; }

        //[Column("IdResponsable")]
        //public virtual SiproResponsable ResponsableEvidencia { get; set; }

        #endregion

        #endregion
    }
}
